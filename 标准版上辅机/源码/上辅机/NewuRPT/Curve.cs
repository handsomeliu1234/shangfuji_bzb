using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NewuRPT
{
    #region 曲线


    public struct Serise属性
    {



        public Serise属性(int _no, Axis _axis, string _field, Color _color)
        {
            no = _no;
            color = _color;
            axis = _axis;
            field = _field;
        }

        public enum Axis
        {
            Left,
            Right
        }

        public int no;
        public Color color;
        public Axis axis;
        public string field;

    }

    /// <summary>
    /// 硫化曲线
    /// </summary>
    public class TTSerise
    {
    

        public TTSerise()
        {
        }

        

        public Serise属性 T背景 = new Serise属性(0, Serise属性.Axis.Right, "", Color.Black);
        public Serise属性 T工艺步骤 = new Serise属性(1, Serise属性.Axis.Left, "val1", Color.Black);

        public Serise属性 L左内温 = new Serise属性(2, Serise属性.Axis.Left, "val2", Color.Red);
        public Serise属性 L左上热板温度 = new Serise属性(3, Serise属性.Axis.Left, "val3", Color.Black);
        public Serise属性 L左下热板温度 = new Serise属性(4, Serise属性.Axis.Left, "val4", Color.Black);
        public Serise属性 L左模套温度 = new Serise属性(5, Serise属性.Axis.Left, "val5", Color.Black);
        public Serise属性 L左内压 = new Serise属性(6, Serise属性.Axis.Right, "val6", Color.DarkOrchid);
        public Serise属性 L左外压 = new Serise属性(7, Serise属性.Axis.Right, "val7", Color.Black);

        public Serise属性 R右内温 = new Serise属性(8, Serise属性.Axis.Left, "val10", Color.OrangeRed);
        public Serise属性 R右上热板温度 = new Serise属性(9, Serise属性.Axis.Left, "val11", Color.Black);
        public Serise属性 R右下热板温度 = new Serise属性(10, Serise属性.Axis.Left, "val12", Color.Black);
        public Serise属性 R右模套温度 = new Serise属性(11, Serise属性.Axis.Left, "val13", Color.Black);
        public Serise属性 R右内压 = new Serise属性(12, Serise属性.Axis.Right, "val14", Color.DarkMagenta);
        public Serise属性 R右外压 = new Serise属性(13, Serise属性.Axis.Right, "val15", Color.Black);
        public Serise属性 M备注 = new Serise属性(14, Serise属性.Axis.Right, "", Color.Black);
    }



    /// <summary>
    /// 密炼曲线
    /// </summary>
    public class TTMixSerise
    {

        public  int Pre提前量 = 0;

        public TTMixSerise()
        {
        }

        public Serise属性 T背景 = new Serise属性(0, Serise属性.Axis.Left , "", Color.Black);

        public Serise属性 T温度 = new Serise属性(1, Serise属性.Axis.Left, "Temp", Color.Red);
        public Serise属性 L功率 = new Serise属性(2, Serise属性.Axis.Right, "Power", Color.Blue);
        public Serise属性 L压力 = new Serise属性(3, Serise属性.Axis.Left, "Press", Color.Green);
        public Serise属性 L转速 = new Serise属性(4, Serise属性.Axis.Left, "Speed", Color.LightGray);
        public Serise属性 L能量 = new Serise属性(5, Serise属性.Axis.Right, "Energy", Color.Yellow);
        public Serise属性 M备注 = new Serise属性(6, Serise属性.Axis.Right, "", Color.Black);
        public Serise属性 L栓位 = new Serise属性(6, Serise属性.Axis.Right, "Resever1", Color.Pink);
        public Serise属性 L电压 = new Serise属性(7, Serise属性.Axis.Right, "Resever2", Color.Orange);

    }


    public class Arr
    {
        private double[] _Arr;


        public Arr(int xLen)
        {
            _Arr = new double[xLen];

        }

        public int Len
        {
            get { return _Arr.Length; }
        }

        public double[] Data
        {
            get { return _Arr; }
        }


        public double MaxValue
        {
            get
            {
                if (_Arr.Length > 0)
                {
                    return _Arr[_Arr.Length - 1];
                }
                else
                {
                    return 0;
                }
            }
        }


        public void Redim(int desiredSize)
        {
            _Arr = (double[])Redim(_Arr, desiredSize);
        }

        public Array Redim(Array origArray, int desiredSize)
        {
            try
            {
                //determine the type of element
                Type t = origArray.GetType().GetElementType();
                //create a number of elements with a new array of expectations
                //new array type must match the type of the original array
                Array newArray = Array.CreateInstance(t, desiredSize);
                //copy the original elements of the array to the new array
                Array.Copy(origArray, 0, newArray, 0, Math.Min(origArray.Length, desiredSize));
                //return new array
                return newArray;

            }
            catch (Exception)
            {
                return origArray;
            }

        }

    }

    #endregion
}
