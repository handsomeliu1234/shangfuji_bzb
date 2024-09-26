using NewuCommon;
using Repository.GlobalConfig;
using System;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_MLData : Form
    {
        private CSharedString memRead = NewuGlobal.MemDB;
        int carbon = 0;
        int power = 0;
        int oil = 0;
        public FM_MLData()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveRawMix rptBll = new SaveRawMix();
        }

        private void btnCarFlag848_Click(object sender, EventArgs e)
        {
            int CarbonBatch = int.Parse(Carbon1120.Text);
            memRead.SetStr(1120, CarbonBatch.ToString("0000"));
            memRead.SetStr(30000, string.Format(Carbon30000.Text, "00000000"));
            memRead.SetStr(848, txt848.Text);
        }

        private void btnZnOFlag852_Click(object sender, EventArgs e)
        {
            int ZnOBatch = int.Parse(ZnO1136.Text);
            memRead.SetStr(1136, ZnOBatch.ToString("0000"));
            memRead.SetStr(30400, string.Format(ZnO30400.Text, "00000000"));
            memRead.SetStr(849, txt852.Text);
        }

        private void btnOilFlag850_Click(object sender, EventArgs e)
        {
            int OilBatch = int.Parse(Oil1128.Text);
            memRead.SetStr(1128, OilBatch.ToString("0000"));
            memRead.SetStr(30200, string.Format(Oil30200.Text, "00000000"));
            memRead.SetStr(850, txt850.Text);
        }

        private void btnRubFlag856_Click(object sender, EventArgs e)
        {
            int rubberBatch = int.Parse(Rubber1144.Text);
            memRead.SetStr(1144, rubberBatch.ToString("0000"));
            memRead.SetStr(30600, string.Format(Rubber30600.Text, "00000000"));
            memRead.SetStr(856, txt856.Text);
        }

        private void btnDrugFlag857_Click(object sender, EventArgs e)
        {
            int DrugBatch = int.Parse(Drug1148.Text);
            memRead.SetStr(1148, DrugBatch.ToString("0000"));
            memRead.SetStr(30700, string.Format(Drug30700.Text, "00000000"));
            memRead.SetStr(857, txt857.Text);
        }

        private void btnMixFlag859_Click(object sender, EventArgs e)
        {
            int MixerBatch = int.Parse(Mixer1156.Text);
            memRead.SetStr(1156, MixerBatch.ToString("0000"));
            memRead.SetStr(32000, string.Format(Mixer32000.Text, "0000000000000000000000000000"));
            memRead.SetStr(859, txt859.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(600, 1) == "1")
            {
                memRead.SetStr(600, "0");
            }
            else
            {
                memRead.SetStr(600, "1");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(601, 1) == "1")
            {
                memRead.SetStr(601, "0");
            }
            else
            {
                memRead.SetStr(601, "1");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(602, 1) == "1")
            {
                memRead.SetStr(602, "0");
            }
            else
            {
                memRead.SetStr(602, "1");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(603, 1) == "1")
            {
                memRead.SetStr(603, "0");
            }
            else
            {
                memRead.SetStr(603, "1");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(604, 1) == "1")
            {
                memRead.SetStr(604, "0");
            }
            else
            {
                memRead.SetStr(604, "1");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(605, 1) == "1")
            {
                memRead.SetStr(605, "0");
            }
            else
            {
                memRead.SetStr(605, "1");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(606, 1) == "1")
            {
                memRead.SetStr(606, "0");
            }
            else
            {
                memRead.SetStr(606, "1");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (txt848.Text == null || txt848.Text == "")
            {
                txt848.Text = "1";
            }
            if (txt848.Text == "0")
            {
                memRead.SetStr(848, "1");
                txt848.Text = memRead.GetStr(848, 1);
            }
            else
            {
                memRead.SetStr(848, "0");
                txt848.Text = memRead.GetStr(848, 1);
            }
        }

        private void Carbon_Click(object sender, EventArgs e)
        {
            if (Carbon1120.Text == null || Carbon1120.Text == "")
            {
                Carbon1120.Text = "0000";
            }
            if (Carbon1120.Text == "0000")
            {
                memRead.SetStr(1120, string.Format(Carbon1120.Text, "0000"));
                Carbon1120.Text = memRead.GetStr(1120, 4);
            }
            else
            {
                memRead.SetStr(1120, string.Format(Carbon1120.Text, "0000"));
                Carbon1120.Text = memRead.GetStr(1120, 4);
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {
            if (Carbon30000.Text == null || Carbon30000.Text == "")
            {
                Carbon30000.Text = "00000000";
            }
            if (Carbon30000.Text == "00000000")
            {
                memRead.SetStr(30000, string.Format(Carbon30000.Text, "00000000"));
                Carbon30000.Text = memRead.GetStr(30000, 8);
            }
            else
            {
                memRead.SetStr(30000, string.Format(Carbon30000.Text, "00000000"));
                Carbon30000.Text = memRead.GetStr(30000, 8);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (txt852.Text == null || txt852.Text == "")
            {
                txt852.Text = "1";
            }
            if (txt852.Text == "0")
            {
                memRead.SetStr(852, "1");
                txt852.Text = memRead.GetStr(852, 1);
            }
            else
            {
                memRead.SetStr(852, "0");
                txt852.Text = memRead.GetStr(852, 1);
            }
        }

        private void ZnO_Click(object sender, EventArgs e)
        {
            if (ZnO1136.Text == null || ZnO1136.Text == "")
            {
                ZnO1136.Text = "0000";
            }
            if (ZnO1136.Text == "0000")
            {
                memRead.SetStr(1136, string.Format(ZnO1136.Text, "0000"));
                ZnO1136.Text = memRead.GetStr(1136, 4);
            }
            else
            {
                memRead.SetStr(1136, string.Format(ZnO1136.Text, "0000"));
                ZnO1136.Text = memRead.GetStr(1136, 4);
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {
            if (ZnO30400.Text == null || ZnO30400.Text == "")
            {
                ZnO30400.Text = "00000000";
            }
            if (ZnO30400.Text == "00000000")
            {
                memRead.SetStr(30400, string.Format(ZnO30400.Text, "00000000"));
                ZnO30400.Text = memRead.GetStr(30400, 8);
            }
            else
            {
                memRead.SetStr(30400, string.Format(ZnO30400.Text, "00000000"));
                ZnO30400.Text = memRead.GetStr(30400, 8);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            if (txt850.Text == null || txt850.Text == "")
            {
                txt850.Text = "1";
            }
            if (txt850.Text == "0")
            {
                memRead.SetStr(850, "1");
                txt850.Text = memRead.GetStr(850, 1);
            }
            else
            {
                memRead.SetStr(850, "0");
                txt850.Text = memRead.GetStr(850, 1);
            }
        }

        private void Oil_Click(object sender, EventArgs e)
        {
            if (Oil1128.Text == null || Oil1128.Text == "")
            {
                Oil1128.Text = "0000";
            }
            if (Oil1128.Text == "0000")
            {
                memRead.SetStr(1128, string.Format(Oil1128.Text, "0000"));
                Oil1128.Text = memRead.GetStr(1128, 4);
            }
            else
            {
                memRead.SetStr(1128, string.Format(Oil1128.Text, "0000"));
                Oil1128.Text = memRead.GetStr(1128, 4);
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
            if (Oil30200.Text == null || Oil30200.Text == "")
            {
                Oil30200.Text = "00000000";
            }
            if (Oil30200.Text == "00000000")
            {
                memRead.SetStr(30200, string.Format(Oil30200.Text, "00000000"));
                Oil30200.Text = memRead.GetStr(30200, 8);
            }
            else
            {
                memRead.SetStr(30200, string.Format(Oil30200.Text, "00000000"));
                Oil30200.Text = memRead.GetStr(30200, 8);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (txt856.Text == null || txt856.Text == "")
            {
                txt856.Text = "1";
            }
            if (txt856.Text == "0")
            {
                memRead.SetStr(856, "1");
                txt856.Text = memRead.GetStr(856, 1);
            }
            else
            {
                memRead.SetStr(856, "0");
                txt856.Text = memRead.GetStr(856, 1);
            }
        }

        private void Rubber_Click(object sender, EventArgs e)
        {
            if (Rubber1144.Text == null || Rubber1144.Text == "")
            {
                Rubber1144.Text = "0000";
            }
            if (Rubber1144.Text == "0000")
            {
                memRead.SetStr(1144, string.Format(Rubber1144.Text, "0000"));
                Rubber1144.Text = memRead.GetStr(1144, 4);
            }
            else
            {
                memRead.SetStr(1144, string.Format(Rubber1144.Text, "0000"));
                Rubber1144.Text = memRead.GetStr(1144, 4);
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {
            if (Rubber30600.Text == null || Rubber30600.Text == "")
            {
                Rubber30600.Text = "0000000000000000";
            }
            if (Rubber30600.Text == "0000000000000000")
            {
                memRead.SetStr(30600, string.Format(Rubber30600.Text, "0000000000000000"));
                Rubber30600.Text = memRead.GetStr(30600, 12);
            }
            else
            {
                memRead.SetStr(30600, string.Format(Rubber30600.Text, "0000000000000000"));
                Rubber30600.Text = memRead.GetStr(30600, 12);
            }
        }

        private void Drug_Click(object sender, EventArgs e)
        {
            if (Drug1148.Text == null || Drug1148.Text == "")
            {
                Drug1148.Text = "0000";
            }
            if (Drug1148.Text == "0000")
            {
                memRead.SetStr(1148, string.Format(Drug1148.Text, "0000"));
                Drug1148.Text = memRead.GetStr(1148, 4);
            }
            else
            {
                memRead.SetStr(1148, string.Format(Drug1148.Text, "0000"));
                Drug1148.Text = memRead.GetStr(1148, 4);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (txt857.Text == null || txt857.Text == "")
            {
                txt857.Text = "1";
            }
            if (txt857.Text == "0")
            {
                memRead.SetStr(857, "1");
                txt857.Text = memRead.GetStr(857, 1);
            }
            else
            {
                memRead.SetStr(857, "0");
                txt857.Text = memRead.GetStr(857, 1);
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {
            if (Drug30700.Text == null || Drug30700.Text == "")
            {
                Drug30700.Text = "00000000";
            }
            if (Drug30700.Text == "00000000")
            {
                memRead.SetStr(30700, string.Format(Drug30700.Text, "00000000"));
                Drug30700.Text = memRead.GetStr(30200, 8);
            }
            else
            {
                memRead.SetStr(30700, string.Format(Drug30700.Text, "00000000"));
                Drug30700.Text = memRead.GetStr(30700, 8);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (txt859.Text == null || txt859.Text == "")
            {
                txt859.Text = "1";
            }
            if (txt859.Text == "0")
            {
                memRead.SetStr(859, "1");
                txt859.Text = memRead.GetStr(859, 1);
            }
            else
            {
                memRead.SetStr(859, "0");
                txt859.Text = memRead.GetStr(859, 1);
            }
        }

        private void Mixer_Click(object sender, EventArgs e)
        {
            if (Mixer1156.Text == null || Mixer1156.Text == "")
            {
                Mixer1156.Text = "0000";
            }
            if (Mixer1156.Text == "0000")
            {
                memRead.SetStr(1156, string.Format(Mixer1156.Text, "0000"));
                Mixer1156.Text = memRead.GetStr(1156, 4);
            }
            else
            {
                memRead.SetStr(1156, string.Format(Mixer1156.Text, "0000"));
                Mixer1156.Text = memRead.GetStr(1156, 4);
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (Mixer32000.Text == null || Mixer32000.Text == "")
            {
                Mixer32000.Text = "0000000000000000000000000000";
            }
            if (Mixer32000.Text == "0000000000000000000000000000")
            {
                memRead.SetStr(32000, string.Format(Mixer32000.Text, "0000000000000000000000000000"));
                Mixer32000.Text = memRead.GetStr(32000, 28);
            }
            else
            {
                memRead.SetStr(32000, string.Format(Mixer32000.Text, "0000000000000000000000000000"));
                Mixer32000.Text = memRead.GetStr(32000, 28);
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(720, 1) == "1")
            {
                memRead.SetStr(720, "0");
            }
            else
            {
                memRead.SetStr(720, "1");
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(721, 1) == "1")
            {
                memRead.SetStr(721, "0");
            }
            else
            {
                memRead.SetStr(721, "1");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(648, 1) == "1")
            {
                memRead.SetStr(648, "0");
            }
            else
            {
                memRead.SetStr(648, "1");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(649, 1) == "1")
            {
                memRead.SetStr(649, "0");
            }
            else
            {
                memRead.SetStr(649, "1");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(650, 1) == "1")
            {
                memRead.SetStr(650, "0");
            }
            else
            {
                memRead.SetStr(650, "1");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(651, 1) == "1")
            {
                memRead.SetStr(651, "0");
            }
            else
            {
                memRead.SetStr(651, "1");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(652, 1) == "1")
            {
                memRead.SetStr(652, "0");
            }
            else
            {
                memRead.SetStr(652, "1");
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(702, 1) == "1")
            {
                memRead.SetStr(702, "0");
            }
            else
            {
                memRead.SetStr(702, "1");
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(696, 1) == "1")
            {
                memRead.SetStr(696, "0");
            }
            else
            {
                memRead.SetStr(696, "1");
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(697, 1) == "1")
            {
                memRead.SetStr(697, "0");
            }
            else
            {
                memRead.SetStr(697, "1");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(656, 1) == "1")
            {
                memRead.SetStr(656, "0");
            }
            else
            {
                memRead.SetStr(656, "1");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(657, 1) == "1")
            {
                memRead.SetStr(657, "0");
            }
            else
            {
                memRead.SetStr(657, "1");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(658, 1) == "1")
            {
                memRead.SetStr(658, "0");
            }
            else
            {
                memRead.SetStr(658, "1");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(659, 1) == "1")
            {
                memRead.SetStr(659, "0");
            }
            else
            {
                memRead.SetStr(659, "1");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(660, 1) == "1")
            {
                memRead.SetStr(660, "0");
            }
            else
            {
                memRead.SetStr(660, "1");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(616, 1) == "1")
            {
                memRead.SetStr(616, "0");
            }
            else
            {
                memRead.SetStr(616, "1");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(617, 1) == "1")
            {
                memRead.SetStr(617, "0");
            }
            else
            {
                memRead.SetStr(617, "1");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(618, 1) == "1")
            {
                memRead.SetStr(618, "0");
            }
            else
            {
                memRead.SetStr(618, "1");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(619, 1) == "1")
            {
                memRead.SetStr(619, "0");
            }
            else
            {
                memRead.SetStr(619, "1");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(620, 1) == "1")
            {
                memRead.SetStr(620, "0");
            }
            else
            {
                memRead.SetStr(620, "1");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(621, 1) == "1")
            {
                memRead.SetStr(621, "0");
            }
            else
            {
                memRead.SetStr(621, "1");
            };
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(792, 1) == "1")
            {
                memRead.SetStr(792, "0");
            }
            else
            {
                memRead.SetStr(792, "1");
            };
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(793, 1) == "1")
            {
                memRead.SetStr(793, "0");
            }
            else
            {
                memRead.SetStr(793, "1");
            };
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(632, 1) == "1")
            {
                memRead.SetStr(632, "0");
            }
            else
            {
                memRead.SetStr(632, "1");
            };
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(633, 1) == "1")
            {
                memRead.SetStr(633, "0");
            }
            else
            {
                memRead.SetStr(633, "1");
            };
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(634, 1) == "1")
            {
                memRead.SetStr(634, "0");
            }
            else
            {
                memRead.SetStr(634, "1");
            };
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(635, 1) == "1")
            {
                memRead.SetStr(635, "0");
            }
            else
            {
                memRead.SetStr(635, "1");
            };
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(636, 1) == "1")
            {
                memRead.SetStr(636, "0");
            }
            else
            {
                memRead.SetStr(636, "1");
            };
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(637, 1) == "1")
            {
                memRead.SetStr(637, "0");
            }
            else
            {
                memRead.SetStr(637, "1");
            };
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(638, 1) == "1")
            {
                memRead.SetStr(638, "0");
            }
            else
            {
                memRead.SetStr(638, "1");
            };
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(760, 1) == "1")
            {
                memRead.SetStr(760, "0");
            }
            else
            {
                memRead.SetStr(760, "1");
            };
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(761, 1) == "1")
            {
                memRead.SetStr(761, "0");
            }
            else
            {
                memRead.SetStr(761, "1");
            };
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(681, 1) == "1")
            {
                memRead.SetStr(681, "0");
            }
            else
            {
                memRead.SetStr(681, "1");
            };
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(680, 1) == "1")
            {
                memRead.SetStr(680, "0");
            }
            else
            {
                memRead.SetStr(680, "1");
            };
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(682, 1) == "1")
            {
                memRead.SetStr(682, "0");
            }
            else
            {
                memRead.SetStr(682, "1");
            };
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(672, 1) == "1")
            {
                memRead.SetStr(672, "0");
            }
            else
            {
                memRead.SetStr(672, "1");
            };
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(673, 1) == "1")
            {
                memRead.SetStr(673, "0");
            }
            else
            {
                memRead.SetStr(673, "1");
            };
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(674, 1) == "1")
            {
                memRead.SetStr(674, "0");
            }
            else
            {
                memRead.SetStr(674, "1");
            };
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(675, 1) == "1")
            {
                memRead.SetStr(675, "0");
            }
            else
            {
                memRead.SetStr(675, "1");
            };
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(676, 1) == "1")
            {
                memRead.SetStr(676, "0");
            }
            else
            {
                memRead.SetStr(676, "1");
            };
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(678, 1) == "1")
            {
                memRead.SetStr(678, "0");
            }
            else
            {
                memRead.SetStr(678, "1");
            };
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(679, 1) == "1")
            {
                memRead.SetStr(679, "0");
            }
            else
            {
                memRead.SetStr(679, "1");
            };
        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(838, 1) == "1")
            {
                memRead.SetStr(838, "0");
            }
            else
            {
                memRead.SetStr(838, "1");
            };
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(839, 1) == "1")
            {
                memRead.SetStr(839, "0");
            }
            else
            {
                memRead.SetStr(839, "1");
            };
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35000, 1) == "1")
            {
                memRead.SetStr(35000, "0");
            }
            else
            {
                memRead.SetStr(35000, "1");
            };
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35001, 1) == "1")
            {
                memRead.SetStr(35001, "0");
            }
            else
            {
                memRead.SetStr(35001, "1");
            };
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35002, 1) == "1")
            {
                memRead.SetStr(35002, "0");
            }
            else
            {
                memRead.SetStr(35002, "1");
            };
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35003, 1) == "1")
            {
                memRead.SetStr(35003, "0");
            }
            else
            {
                memRead.SetStr(35003, "1");
            };
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35004, 1) == "1")
            {
                memRead.SetStr(35004, "0");
            }
            else
            {
                memRead.SetStr(35004, "1");
            };
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35005, 1) == "1")
            {
                memRead.SetStr(35005, "0");
            }
            else
            {
                memRead.SetStr(35005, "1");
            };
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35006, 1) == "1")
            {
                memRead.SetStr(35006, "0");
            }
            else
            {
                memRead.SetStr(35006, "1");
            };
        }

        private void button59_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35007, 1) == "1")
            {
                memRead.SetStr(35007, "0");
            }
            else
            {
                memRead.SetStr(35007, "1");
            };
        }

        private void button67_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35030, 1) == "1")
            {
                memRead.SetStr(35030, "0");
            }
            else
            {
                memRead.SetStr(35030, "1");
            };
        }

        private void button66_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35031, 1) == "1")
            {
                memRead.SetStr(35031, "0");
            }
            else
            {
                memRead.SetStr(35031, "1");
            };
        }

        private void button65_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35032, 1) == "1")
            {
                memRead.SetStr(35032, "0");
            }
            else
            {
                memRead.SetStr(35032, "1");
            };
        }

        private void button64_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35033, 1) == "1")
            {
                memRead.SetStr(35033, "0");
            }
            else
            {
                memRead.SetStr(35033, "1");
            };
        }

        private void button63_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35034, 1) == "1")
            {
                memRead.SetStr(35034, "0");
            }
            else
            {
                memRead.SetStr(35034, "1");
            };
        }

        private void button62_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35035, 1) == "1")
            {
                memRead.SetStr(35035, "0");
            }
            else
            {
                memRead.SetStr(35035, "1");
            };
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35036, 1) == "1")
            {
                memRead.SetStr(35036, "0");
            }
            else
            {
                memRead.SetStr(35036, "1");
            };
        }

        private void button60_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(35037, 1) == "1")
            {
                memRead.SetStr(35037, "0");
            }
            else
            {
                memRead.SetStr(35037, "1");
            };
        }

        private void button68_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(823, 1) == "1")
            {
                memRead.SetStr(823, "0");
            }
            else
            {
                memRead.SetStr(823, "1");
            };
        }

        private void button69_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(1068, 1) == "1")
            {
                memRead.SetStr(1068, "0");
            }
            else
            {
                memRead.SetStr(1068, "1");
            };
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                textBox1.Text = "0000";
            }
            if (textBox1.Text == "0000")
            {
                memRead.SetStr(1004, string.Format(textBox1.Text, "0000"));
                textBox1.Text = memRead.GetStr(1004, 4);
            }
            else
            {
                memRead.SetStr(1004, string.Format(textBox1.Text, "0000"));
                textBox1.Text = memRead.GetStr(1004, 4);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == null || textBox2.Text == "")
            {
                textBox2.Text = "0000";
            }
            if (textBox2.Text == "0000")
            {
                memRead.SetStr(1000, string.Format(textBox2.Text, "0000"));
                textBox2.Text = memRead.GetStr(1000, 4);
            }
            else
            {
                memRead.SetStr(1000, string.Format(textBox2.Text, "0000"));
                textBox2.Text = memRead.GetStr(1000, 4);
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == null || textBox5.Text == "")
            {
                textBox5.Text = "0000";
            }
            if (textBox5.Text == "0000")
            {
                memRead.SetStr(1032, string.Format(textBox5.Text, "0000"));
                textBox5.Text = memRead.GetStr(1032, 4);
            }
            else
            {
                memRead.SetStr(1032, string.Format(textBox5.Text, "0000"));
                textBox5.Text = memRead.GetStr(1032, 4);
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == null || textBox4.Text == "")
            {
                textBox4.Text = "0000";
            }
            if (textBox4.Text == "0000")
            {
                memRead.SetStr(1016, string.Format(textBox4.Text, "0000"));
                textBox4.Text = memRead.GetStr(1016, 4);
            }
            else
            {
                memRead.SetStr(1016, string.Format(textBox4.Text, "0000"));
                textBox4.Text = memRead.GetStr(1016, 4);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == null || textBox6.Text == "")
            {
                textBox6.Text = "0000";
            }
            if (textBox6.Text == "0000")
            {
                memRead.SetStr(1040, string.Format(textBox6.Text, "0000"));
                textBox6.Text = memRead.GetStr(1040, 4);
            }
            else
            {
                memRead.SetStr(1040, string.Format(textBox6.Text, "0000"));
                textBox6.Text = memRead.GetStr(1040, 4);
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == null || textBox3.Text == "")
            {
                textBox3.Text = "0000";
            }
            if (textBox3.Text == "0000")
            {
                memRead.SetStr(1044, string.Format(textBox3.Text, "0000"));
                textBox3.Text = memRead.GetStr(1044, 4);
            }
            else
            {
                memRead.SetStr(1044, string.Format(textBox3.Text, "0000"));
                textBox3.Text = memRead.GetStr(1044, 4);
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == null || textBox8.Text == "")
            {
                textBox8.Text = "0000";
            }
            if (textBox8.Text == "0000")
            {
                memRead.SetStr(1176, string.Format(textBox8.Text, "0000"));
                textBox8.Text = memRead.GetStr(1176, 4);
            }
            else
            {
                memRead.SetStr(1176, string.Format(textBox8.Text, "0000"));
                textBox8.Text = memRead.GetStr(1176, 4);
            }
        }

        private void label34_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == null || textBox11.Text == "")
            {
                textBox11.Text = "0000";
            }
            if (textBox11.Text == "0000")
            {
                memRead.SetStr(1180, string.Format(textBox11.Text, "0000"));
                textBox11.Text = memRead.GetStr(1180, 4);
            }
            else
            {
                memRead.SetStr(1180, string.Format(textBox11.Text, "0000"));
                textBox11.Text = memRead.GetStr(1180, 4);
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == null || textBox10.Text == "")
            {
                textBox10.Text = "0000";
            }
            if (textBox10.Text == "0000")
            {
                memRead.SetStr(1184, string.Format(textBox10.Text, "0000"));
                textBox10.Text = memRead.GetStr(1184, 4);
            }
            else
            {
                memRead.SetStr(1184, string.Format(textBox10.Text, "0000"));
                textBox10.Text = memRead.GetStr(1184, 4);
            }
        }

        private void label33_Click(object sender, EventArgs e)
        {
            if (textBox12.Text == null || textBox12.Text == "")
            {
                textBox12.Text = "0000";
            }
            if (textBox12.Text == "0000")
            {
                memRead.SetStr(1188, string.Format(textBox12.Text, "0000"));
                textBox12.Text = memRead.GetStr(1188, 4);
            }
            else
            {
                memRead.SetStr(1188, string.Format(textBox12.Text, "0000"));
                textBox12.Text = memRead.GetStr(1188, 4);
            }
        }

        private void label32_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == null || textBox9.Text == "")
            {
                textBox9.Text = "0000";
            }
            if (textBox9.Text == "0000")
            {
                memRead.SetStr(1192, string.Format(textBox9.Text, "0000"));
                textBox9.Text = memRead.GetStr(1192, 4);
            }
            else
            {
                memRead.SetStr(1192, string.Format(textBox9.Text, "0000"));
                textBox9.Text = memRead.GetStr(1192, 4);
            }
        }

        private void label29_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == null || textBox7.Text == "")
            {
                textBox7.Text = "0000";
            }
            if (textBox7.Text == "0000")
            {
                memRead.SetStr(1196, string.Format(textBox7.Text, "0000"));
                textBox7.Text = memRead.GetStr(1196, 4);
            }
            else
            {
                memRead.SetStr(1196, string.Format(textBox7.Text, "0000"));
                textBox7.Text = memRead.GetStr(1196, 4);
            }
        }

        private void label36_Click(object sender, EventArgs e)
        {
            if (textBox14.Text == null || textBox14.Text == "")
            {
                textBox14.Text = "0000";
            }
            if (textBox14.Text == "0000")
            {
                memRead.SetStr(1200, string.Format(textBox14.Text, "0000"));
                textBox14.Text = memRead.GetStr(1200, 4);
            }
            else
            {
                memRead.SetStr(1200, string.Format(textBox14.Text, "0000"));
                textBox14.Text = memRead.GetStr(1200, 4);
            }
        }

        private void label42_Click(object sender, EventArgs e)
        {
            if (textBox20.Text == null || textBox20.Text == "")
            {
                textBox20.Text = "0000";
            }
            if (textBox20.Text == "0000")
            {
                memRead.SetStr(1224, string.Format(textBox20.Text, "0000"));
                textBox20.Text = memRead.GetStr(1224, 4);
            }
            else
            {
                memRead.SetStr(1224, string.Format(textBox20.Text, "0000"));
                textBox20.Text = memRead.GetStr(1224, 4);
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {
            if (textBox17.Text == null || textBox17.Text == "")
            {
                textBox17.Text = "0000";
            }
            if (textBox17.Text == "0000")
            {
                memRead.SetStr(452, string.Format(textBox17.Text, "0000"));
                textBox17.Text = memRead.GetStr(452, 4);
            }
            else
            {
                memRead.SetStr(452, string.Format(textBox17.Text, "0000"));
                textBox17.Text = memRead.GetStr(452, 4);
            }
        }

        private void label37_Click(object sender, EventArgs e)
        {
            if (textBox16.Text == null || textBox16.Text == "")
            {
                textBox16.Text = "0000";
            }
            if (textBox16.Text == "0000")
            {
                memRead.SetStr(404, string.Format(textBox16.Text, "0000"));
                textBox16.Text = memRead.GetStr(404, 4);
            }
            else
            {
                memRead.SetStr(404, string.Format(textBox16.Text, "0000"));
                textBox16.Text = memRead.GetStr(404, 4);
            }
        }

        private void label39_Click(object sender, EventArgs e)
        {
            if (textBox18.Text == null || textBox18.Text == "")
            {
                textBox18.Text = "0000";
            }
            if (textBox18.Text == "0000")
            {
                memRead.SetStr(460, string.Format(textBox18.Text, "0000"));
                textBox18.Text = memRead.GetStr(460, 4);
            }
            else
            {
                memRead.SetStr(460, string.Format(textBox18.Text, "0000"));
                textBox18.Text = memRead.GetStr(460, 4);
            }
        }

        private void label38_Click(object sender, EventArgs e)
        {
            if (textBox15.Text == null || textBox15.Text == "")
            {
                textBox15.Text = "0000";
            }
            if (textBox15.Text == "0000")
            {
                memRead.SetStr(420, string.Format(textBox15.Text, "0000"));
                textBox15.Text = memRead.GetStr(420, 4);
            }
            else
            {
                memRead.SetStr(420, string.Format(textBox15.Text, "0000"));
                textBox15.Text = memRead.GetStr(420, 4);
            }
        }

        private void label35_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == null || textBox13.Text == "")
            {
                textBox13.Text = "0000";
            }
            if (textBox13.Text == "0000")
            {
                memRead.SetStr(436, string.Format(textBox13.Text, "0000"));
                textBox13.Text = memRead.GetStr(436, 4);
            }
            else
            {
                memRead.SetStr(436, string.Format(textBox13.Text, "0000"));
                textBox13.Text = memRead.GetStr(436, 4);
            }
        }

        private void label46_Click(object sender, EventArgs e)
        {
            if (textBox23.Text == null || textBox23.Text == "")
            {
                textBox23.Text = "0000";
            }
            if (textBox23.Text == "0000")
            {
                memRead.SetStr(448, string.Format(textBox23.Text, "0000"));
                textBox23.Text = memRead.GetStr(448, 4);
            }
            else
            {
                memRead.SetStr(448, string.Format(textBox23.Text, "0000"));
                textBox23.Text = memRead.GetStr(448, 4);
            }
        }

        private void label43_Click(object sender, EventArgs e)
        {
            if (textBox22.Text == null || textBox22.Text == "")
            {
                textBox22.Text = "0000";
            }
            if (textBox22.Text == "0000")
            {
                memRead.SetStr(400, string.Format(textBox22.Text, "0000"));
                textBox22.Text = memRead.GetStr(400, 4);
            }
            else
            {
                memRead.SetStr(400, string.Format(textBox22.Text, "0000"));
                textBox22.Text = memRead.GetStr(400, 4);
            }
        }

        private void label45_Click(object sender, EventArgs e)
        {
            if (textBox24.Text == null || textBox24.Text == "")
            {
                textBox24.Text = "0000";
            }
            if (textBox24.Text == "0000")
            {
                memRead.SetStr(456, string.Format(textBox24.Text, "0000"));
                textBox24.Text = memRead.GetStr(456, 4);
            }
            else
            {
                memRead.SetStr(456, string.Format(textBox24.Text, "0000"));
                textBox24.Text = memRead.GetStr(456, 4);
            }
        }

        private void label44_Click(object sender, EventArgs e)
        {
            if (textBox21.Text == null || textBox21.Text == "")
            {
                textBox21.Text = "0000";
            }
            if (textBox21.Text == "0000")
            {
                memRead.SetStr(416, string.Format(textBox21.Text, "0000"));
                textBox21.Text = memRead.GetStr(416, 4);
            }
            else
            {
                memRead.SetStr(416, string.Format(textBox21.Text, "0000"));
                textBox21.Text = memRead.GetStr(416, 4);
            }
        }

        private void label41_Click(object sender, EventArgs e)
        {
            if (textBox19.Text == null || textBox19.Text == "")
            {
                textBox19.Text = "0000";
            }
            if (textBox19.Text == "0000")
            {
                memRead.SetStr(432, string.Format(textBox19.Text, "0000"));
                textBox19.Text = memRead.GetStr(432, 4);
            }
            else
            {
                memRead.SetStr(432, string.Format(textBox19.Text, "0000"));
                textBox19.Text = memRead.GetStr(432, 4);
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            memRead.SetStr(35000, "000000000000000000000000000000");
            memRead.SetStr(35030, "000000000000000000000000000000");
        }

        private void button71_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37000, 1) == "1")
            {
                memRead.SetStr(37000, "0");
            }
            else
            {
                memRead.SetStr(37000, "1");
            };
        }

        private void button114_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37145, 1) == "1")
            {
                memRead.SetStr(37145, "0");
            }
            else
            {
                memRead.SetStr(37145, "1");
            };
        }

        private void button112_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37147, 1) == "1")
            {
                memRead.SetStr(37147, "0");
            }
            else
            {
                memRead.SetStr(37147, "1");
            };
        }

        private void button113_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37146, 1) == "1")
            {
                memRead.SetStr(37146, "0");
            }
            else
            {
                memRead.SetStr(37146, "1");
            };
        }

        private void button111_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37148, 1) == "1")
            {
                memRead.SetStr(37148, "0");
            }
            else
            {
                memRead.SetStr(37148, "1");
            };
        }

        private void button72_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37001, 1) == "1")
            {
                memRead.SetStr(37001, "0");
            }
            else
            {
                memRead.SetStr(37001, "1");
            };
        }

        private void button73_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37002, 1) == "1")
            {
                memRead.SetStr(37002, "0");
            }
            else
            {
                memRead.SetStr(37002, "1");
            };
        }

        private void button74_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37003, 1) == "1")
            {
                memRead.SetStr(37003, "0");
            }
            else
            {
                memRead.SetStr(37003, "1");
            };
        }

        private void button78_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37007, 1) == "1")
            {
                memRead.SetStr(37007, "0");
            }
            else
            {
                memRead.SetStr(37007, "1");
            };
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button75_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37010, 1) == "1")
            {
                memRead.SetStr(37010, "0");
            }
            else
            {
                memRead.SetStr(37010, "1");
            };
        }

        private void button77_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37008, 1) == "1")
            {
                memRead.SetStr(37008, "0");
            }
            else
            {
                memRead.SetStr(37008, "1");
            };
        }

        private void button76_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37009, 1) == "1")
            {
                memRead.SetStr(37009, "0");
            }
            else
            {
                memRead.SetStr(37009, "1");
            };
        }

        private void button82_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37014, 1) == "1")
            {
                memRead.SetStr(37014, "0");
            }
            else
            {
                memRead.SetStr(37014, "1");
            };
        }

        private void button81_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37015, 1) == "1")
            {
                memRead.SetStr(37015, "0");
            }
            else
            {
                memRead.SetStr(37015, "1");
            };
        }

        private void button80_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37016, 1) == "1")
            {
                memRead.SetStr(37016, "0");
            }
            else
            {
                memRead.SetStr(37016, "1");
            };
        }

        private void button79_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37017, 1) == "1")
            {
                memRead.SetStr(37017, "0");
            }
            else
            {
                memRead.SetStr(37017, "1");
            };
        }

        private void button86_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37021, 1) == "1")
            {
                memRead.SetStr(37021, "0");
            }
            else
            {
                memRead.SetStr(37021, "1");
            };
        }

        private void button85_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37022, 1) == "1")
            {
                memRead.SetStr(37022, "0");
            }
            else
            {
                memRead.SetStr(37022, "1");
            };
        }

        private void button84_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37023, 1) == "1")
            {
                memRead.SetStr(37023, "0");
            }
            else
            {
                memRead.SetStr(37023, "1");
            };
        }

        private void button83_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37024, 1) == "1")
            {
                memRead.SetStr(37024, "0");
            }
            else
            {
                memRead.SetStr(37024, "1");
            };
        }

        private void button90_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37028, 1) == "1")
            {
                memRead.SetStr(37028, "0");
            }
            else
            {
                memRead.SetStr(37028, "1");
            };
        }

        private void button94_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37035, 1) == "1")
            {
                memRead.SetStr(37035, "0");
            }
            else
            {
                memRead.SetStr(37035, "1");
            };
        }

        private void button87_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37031, 1) == "1")
            {
                memRead.SetStr(37031, "0");
            }
            else
            {
                memRead.SetStr(37031, "1");
            };
        }

        private void button89_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37029, 1) == "1")
            {
                memRead.SetStr(37029, "0");
            }
            else
            {
                memRead.SetStr(37029, "1");
            };
        }

        private void button88_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37030, 1) == "1")
            {
                memRead.SetStr(37030, "0");
            }
            else
            {
                memRead.SetStr(37030, "1");
            };
        }

        private void button93_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37036, 1) == "1")
            {
                memRead.SetStr(37036, "0");
            }
            else
            {
                memRead.SetStr(37036, "1");
            };
        }

        private void button92_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37037, 1) == "1")
            {
                memRead.SetStr(37037, "0");
            }
            else
            {
                memRead.SetStr(37037, "1");
            };
        }

        private void button91_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37038, 1) == "1")
            {
                memRead.SetStr(37038, "0");
            }
            else
            {
                memRead.SetStr(37038, "1");
            };
        }

        private void button98_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37042, 1) == "1")
            {
                memRead.SetStr(37042, "0");
            }
            else
            {
                memRead.SetStr(37042, "1");
            };
        }

        private void button97_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37043, 1) == "1")
            {
                memRead.SetStr(37043, "0");
            }
            else
            {
                memRead.SetStr(37043, "1");
            };
        }

        private void button96_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37044, 1) == "1")
            {
                memRead.SetStr(37044, "0");
            }
            else
            {
                memRead.SetStr(37044, "1");
            };
        }

        private void button95_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37045, 1) == "1")
            {
                memRead.SetStr(37045, "0");
            }
            else
            {
                memRead.SetStr(37045, "1");
            };
        }

        private void button102_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37049, 1) == "1")
            {
                memRead.SetStr(37049, "0");
            }
            else
            {
                memRead.SetStr(37049, "1");
            };
        }

        private void button101_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37050, 1) == "1")
            {
                memRead.SetStr(37050, "0");
            }
            else
            {
                memRead.SetStr(37050, "1");
            };
        }

        private void button100_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37051, 1) == "1")
            {
                memRead.SetStr(37051, "0");
            }
            else
            {
                memRead.SetStr(37051, "1");
            };
        }

        private void button99_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37052, 1) == "1")
            {
                memRead.SetStr(37052, "0");
            }
            else
            {
                memRead.SetStr(37052, "1");
            };
        }

        private void button106_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37056, 1) == "1")
            {
                memRead.SetStr(37056, "0");
            }
            else
            {
                memRead.SetStr(37056, "1");
            };
        }

        private void button105_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37057, 1) == "1")
            {
                memRead.SetStr(37057, "0");
            }
            else
            {
                memRead.SetStr(37057, "1");
            };
        }

        private void button104_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37058, 1) == "1")
            {
                memRead.SetStr(37058, "0");
            }
            else
            {
                memRead.SetStr(37058, "1");
            };
        }

        private void button103_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37059, 1) == "1")
            {
                memRead.SetStr(37059, "0");
            }
            else
            {
                memRead.SetStr(37059, "1");
            };
        }

        private void button110_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37063, 1) == "1")
            {
                memRead.SetStr(37063, "0");
            }
            else
            {
                memRead.SetStr(37063, "1");
            };
        }

        private void button109_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37064, 1) == "1")
            {
                memRead.SetStr(37064, "0");
            }
            else
            {
                memRead.SetStr(37064, "1");
            };
        }

        private void button108_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37065, 1) == "1")
            {
                memRead.SetStr(37065, "0");
            }
            else
            {
                memRead.SetStr(37065, "1");
            };
        }

        private void button107_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37066, 1) == "1")
            {
                memRead.SetStr(37066, "0");
            }
            else
            {
                memRead.SetStr(37066, "1");
            };
        }

        private void button117_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37095, 1) == "1")
            {
                memRead.SetStr(37095, "0");
            }
            else
            {
                memRead.SetStr(37095, "1");
            };
        }

        private void button116_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37096, 1) == "1")
            {
                memRead.SetStr(37096, "0");
            }
            else
            {
                memRead.SetStr(37096, "1");
            };
        }

        private void button115_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37097, 1) == "1")
            {
                memRead.SetStr(37097, "0");
            }
            else
            {
                memRead.SetStr(37097, "1");
            };
        }

        private void button120_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37100, 1) == "1")
            {
                memRead.SetStr(37100, "0");
            }
            else
            {
                memRead.SetStr(37100, "1");
            };
        }

        private void button119_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37101, 1) == "1")
            {
                memRead.SetStr(37101, "0");
            }
            else
            {
                memRead.SetStr(37101, "1");
            };
        }

        private void button118_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37102, 1) == "1")
            {
                memRead.SetStr(37102, "0");
            }
            else
            {
                memRead.SetStr(37102, "1");
            };
        }

        private void button123_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37105, 1) == "1")
            {
                memRead.SetStr(37105, "0");
            }
            else
            {
                memRead.SetStr(37105, "1");
            };
        }

        private void button122_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37106, 1) == "1")
            {
                memRead.SetStr(37106, "0");
            }
            else
            {
                memRead.SetStr(37106, "1");
            };
        }

        private void button121_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37107, 1) == "1")
            {
                memRead.SetStr(37107, "0");
            }
            else
            {
                memRead.SetStr(37107, "1");
            };
        }

        private void button126_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37111, 1) == "1")
            {
                memRead.SetStr(37111, "0");
            }
            else
            {
                memRead.SetStr(37111, "1");
            };
        }

        private void button125_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37112, 1) == "1")
            {
                memRead.SetStr(37112, "0");
            }
            else
            {
                memRead.SetStr(37112, "1");
            };
        }

        private void button124_Click(object sender, EventArgs e)
        {
            if (memRead.GetStr(37113, 1) == "1")
            {
                memRead.SetStr(37113, "0");
            }
            else
            {
                memRead.SetStr(37113, "1");
            };
        }

        private void button127_Click(object sender, EventArgs e)
        {
            
            carbon++;
            label1.Text = carbon.ToString();
            if (carbon > 11)
            {
                memRead.SetStr(704, "0000000000");
                carbon = 0;
                memRead.SetStr(716, "0");
                label1.Text = "";
            }
            else
            {
                if (carbon == 1)
                {
                    memRead.SetStr(704, "1");
                }
                else if (carbon == 2)
                {
                    memRead.SetStr(705, "1");
                }
                else if (carbon == 3)
                {
                    memRead.SetStr(706, "1");
                }
                else if (carbon == 4)
                {
                    memRead.SetStr(707, "1");
                }
                else if (carbon == 5)
                {
                    memRead.SetStr(708, "1");
                }
                else if (carbon == 6)
                {
                    memRead.SetStr(709, "1");
                }
                else if (carbon == 7)
                {
                    memRead.SetStr(710, "1");
                }
                else if (carbon == 8)
                {
                    memRead.SetStr(711, "1");
                }
                else if (carbon == 9)
                {
                    memRead.SetStr(712, "1");
                }
                else if (carbon == 10)
                {
                    memRead.SetStr(713, "1");
                }
                else if (carbon == 11)
                {
                    memRead.SetStr(716, "1");
                }
            }
        }

        private void button128_Click(object sender, EventArgs e)
        {
            power++;
            label2.Text = power.ToString();
            if (power > 8)
            {
                memRead.SetStr(752, "00000000");
                power = 0;
                label2.Text = "";
            }
            else
            {
                if (power == 1)
                {
                    memRead.SetStr(752, "1");
                }
                else if (power == 2)
                {
                    memRead.SetStr(753, "1");
                }
                else if (power == 3)
                {
                    memRead.SetStr(754, "1");
                }
                else if (power == 4)
                {
                    memRead.SetStr(755, "1");
                }
                else if (power == 5)
                {
                    memRead.SetStr(756, "1");
                }
                else if (power == 6)
                {
                    memRead.SetStr(757, "1");
                }
                else if (power == 7)
                {
                    memRead.SetStr(758, "1");
                }
                else if (power == 8)
                {
                    memRead.SetStr(759, "1");
                }
            }
        }

        private void button129_Click(object sender, EventArgs e)
        {
            oil++;
            label3.Text = oil.ToString();
            if (oil > 8)
            {
                memRead.SetStr(784, "0000000000");
                oil = 0;
                label3.Text = "";
            }
            else
            {
                if (oil == 1)
                {
                    memRead.SetStr(784, "1");
                }
                else if (oil == 2)
                {
                    memRead.SetStr(785, "1");
                }
                else if (oil == 3)
                {
                    memRead.SetStr(786, "1");
                }
                else if (oil == 4)
                {
                    memRead.SetStr(787, "1");
                }
                else if (oil == 5)
                {
                    memRead.SetStr(788, "1");
                }
                else if (oil == 6)
                {
                    memRead.SetStr(789, "1");
                }
                else if (oil == 7)
                {
                    memRead.SetStr(790, "1");
                }
                else if (oil == 8)
                {
                    memRead.SetStr(791, "1");
                }
            }
        }
    }
}