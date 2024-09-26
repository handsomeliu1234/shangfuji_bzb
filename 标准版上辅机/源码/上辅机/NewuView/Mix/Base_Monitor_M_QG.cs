using NewuBLL;
using NewuControl;
using System;
using System.Data;
using System.Drawing;

namespace NewuView.Mix
{
    public partial class Base_Monitor_M_QG : Base_Monitor_M
    {

        public Base_Monitor_M_QG()
        {
            InitializeComponent();
        }
        private void FM_LL_M_QG_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadZnOName();
                sonHandleEvent += new SonHandle(MonitorRefresh);
            }
        }
        void MonitorRefresh()
        {
            DisplayYiBiao();
            DisPlayZonScale();
            // 回收斗
            ReCycBin2.NewuSet料位(SS.getInt(37056, 4));
            DisPlayReCycCarbonBin();
        }
        void DisPlayReCycCarbonBin()
        {
            bool temp = SS.getbool(717);
            ReCycPipe21.setImageTag(temp);
            ReCycPipe22.setImageTag(temp);
            ReCycPipe23.setImageTag(temp);
        }

        void DisplayYiBiao()
        {
            //----------------------------------------------粉料磅秤值
            ScaleDisPlay.Scale_Z(YiBiaoZon1);
            //----------------------------------------------粉料秤中间斗
            ScaleDisPlay.Scale_Z_MID(YiBiaoZonMid);
        }

        // 加载粉料罐的名称 
        void LoadZnOName()
        {
            string _typeCodeName = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T粉料);

            string _typeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName);
            DataSet ds = new NewuBLL.TB_BinSetingBLL().GetListJoinMaterialCode("", _typeCodeID);
            int cnt = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //if (cnt > 3) break;
                //CarbonBin cb = this.Controls["zonBin0" + cnt++] as CarbonBin;
                //cb.NewuLabText = (cnt - 1) + "#" + "\n" + row["MaterialCode"].ToString();
                if (cnt <=2 )
                {
                    CarbonBin cb = this.Controls["zonBin0" + cnt++] as CarbonBin;
                    if (cb == null) break;
                    cb.NewuLabText = (cnt - 1) + "#" + "\n" + row["MaterialCode"].ToString();
                }
            }
        }

        /// <summary>
        /// 粉料秤
        /// </summary>
        void DisPlayZonScale()
        {
            zonBin01.NewuSet料位(false, SS.getbool(37095), SS.getbool(37096), SS.getbool(37097));
            zonBin02.NewuSet料位(false, SS.getbool(37100), SS.getbool(37101), SS.getbool(37102));
            //加粉料
            int StartPos = 752;
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    NewuPicAngle pc = this.Controls["PipeZon" + i + j] as NewuPicAngle;
                    if (pc == null) break;
                    pc.setImageTag(SS.getbool(StartPos + i - 1));
                }
            }
            //粉料称好
            ZonScaleBin.setImageTag(SS.getbool(635));
            //粉料中间斗有料
            ZonScaleMidBin.setImageTag(SS.getbool(637));
            //卸粉料
            FaZonScale.setImageTag(SS.getbool(760));
            PipeZonScale.setImageTag(SS.getbool(760));
            //粉料排错位
            if (SS.getbool(766))
            {
                lblZonTroubleshooting.BackColor = Color.Lime; //粉料排错位光电
                lblZonHost.BackColor = SystemColors.ControlLightLight;
            }
            else
            {
                lblZonTroubleshooting.BackColor = SystemColors.ControlLightLight; //粉料排错位光电
                lblZonHost.BackColor = Color.Lime; //主机位
            }
            //投粉料
            bool To = SS.getbool(761); //投粉料
            bool Ro = SS.getbool(766);  //粉料排错位
            FaZonScaleMid.setImageTag(To);
            PipeZonScaleMid01.setImageTag(To);
            PipeZonScaleMid02.setImageTag(To && !Ro);
            PipeZonScaleMid023.setImageTag(To && !Ro);
            PipeZonScaleMid03.setImageTag(To && !Ro);
            PipeZonScaleMid04.setImageTag(To && Ro);
            PipeZonScaleMid05.setImageTag(To && Ro);
            PipeZonScaleMid06.setImageTag(To && Ro);
        }
    }
}
