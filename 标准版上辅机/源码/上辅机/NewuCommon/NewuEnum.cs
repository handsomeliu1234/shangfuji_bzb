using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Newu
{
    /// <summary>
    /// 配方部分
    /// </summary>
    public enum FormulaPartEnum
    {
        /// <summary>
        /// 称量部分
        /// </summary>
        Raw,

        /// <summary>
        /// 密炼部分
        /// </summary>
        Mix
    }

    public enum ShowMode
    {
        Dialog,
        MidForm
    }

    public enum FormMode
    {
        Edit,
        Add
    }

    public enum NodeLevel
    {
        Top,
        Child
    }

    public enum ColumnType
    {
        txt,
        cmb,
        chk,
        btn,
        img
    }

    public static class NewuColor
    {
        public static Color PanelBg = SystemColors.ControlLight;
        public static Color ControlInit = SystemColors.Control;
    }

    public enum DeviceType
    {
        T上辅机,
        T小药
    }

    public enum EventType
    {
        DeviceStop,
        DeviceRun
    }

    public enum PmMode
    {
        Manual,
        Automatic
    }

    public enum DevicePartType
    {
        Zno,
        Carbon,
        WhiteCarbon,//用作炭黑2
        Oil,
        DrugMixer,
        MixUp,
        MixDown,
        Rubber,
        Silane,
        Plasticizer,
        DrugXf,  //小药系统专属 不属于上辅机
        DrugMixer2,
        Oil2,
        Zon2,
        Carbon2
    }

    public enum AppEventType
    {
        UserLogin,
        SystemLogOut,
        AppRun,
        AppStop,
        WorkShiftChange,
    }

    public enum AppLogType
    {
        FormulaLog,
        SendOrderLog,
        Add,
        Update,
        Delete,
        Send,
        BackUp
    }
}