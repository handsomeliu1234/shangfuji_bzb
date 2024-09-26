namespace Repository.GlobalConfig
{
    public class AddressConst
    {
        public const int MixerSystemParams = 29000;  //密炼系统参数的首地址
        public const int MixerDownSystemParams = 29500;  //下密炼系统参数的首地址
        public const int MixerTechStart = 35000; //密炼工艺开始首地址
        public const int MixerTechEnd = 35030;   //密炼工艺结束首地址
        public const int MixerTechDownStart = 35100; //下密炼工艺开始首地址
        public const int MixerTechDownEnd = 35110;   //下密炼工艺结束首地址
        public const int DischargeRubberTime = 27200;            //排胶时间首地址
        public const int DischargeRubberDownTime = 28400;            //排胶时间首地址
        public const int MixerTime = 86;                     //炼胶料时间
        public const int MixerFTime = 87;                     //下密炼炼胶时间
        public const int UpDateAlarm = 85;                   //更新报警列表
        public const int PLCCommunicationStatus = 1324; //PLC通讯状态
        public const int ConstantTempChar = 27208;    //恒温炼胶字符地址
        public const int SendFeedingParam = 630;      //断电发送加料参数
    }

    /// <summary>
    /// MixerWeight类存放所有物料称量以及密炼的首地址
    /// </summary>
    public enum MixerWeight
    {
        Carbon = 11000,  //炭黑
        Carbon2 = 1200,    //炭黑2
        Oil = 13000,     //油料
        Oil2 = 14000,      //油料2
        Zno = 15000,      //粉料
        Plasticizer = 16000, // 塑解剂
        Rubber = 17000,   //胶料
        DrugMixer = 18000,  //小药
        DrugMixerDown = 18280, //下密炼小药
        Silane = 19000,       //硅烷
        Mixer = 26000,      //密炼机
        MixerDown = 28000    //下密炼机
    }

    /// <summary>
    /// MixerTransFlag类存放所有物料的发送标志位的首地址
    /// </summary>
    public enum MixerTransFlag
    {
        Carbon = 29900,  //炭黑
        Carbon2 = 29904, //炭黑2
        Oil = 29908,   //油料
        Oil2 = 29912,   //油料2
        Zno = 29916,   //粉料
        Plasticizer = 29920,//塑解剂
        Rubber = 29924,   //胶料
        DrugMixer = 29928,  //药品
        Silane = 29932,    //硅烷
        Mixer = 29936,   //密炼
        DrugMixerDown = 29940   //下密炼
    }

    /// <summary>
    /// MixerNetWeight类存放所有物料的净重的首地址
    /// </summary>
    public enum MixerNetWeight
    {
        Carbon = 400,           //重量
        CarbonSerialNum = 404,  //序号
        Carbon2 = 408,
        CarbonSerialNum2 = 412,
        Oil = 416,
        OilSerialNum = 420,
        Oil2 = 416,
        OilSerialNum2 = 420,
        Zno = 432,
        ZnoSerialNum = 436,
        Plasticizer = 440,
        PlasticizerSerialNum = 444,
        Rubber = 448,
        RubberSerialNum = 452,
        DrugMixer = 456,
        DrugMixerSerialNum = 460,
        Silane = 464,
        SilaneSerialNum = 468,
        DrugMixer2 = 472,
        DrugMixer2SerialNum = 476
    }

    /// <summary>
    /// MixerReportWeightandMixer类存放报表回采各种物料重量和密炼数据的首地址
    /// </summary>
    public enum MixerReportWeightandMixer
    {
        Carbon = 30000,
        Carbon2 = 30100,
        Oil = 30200,
        Oil2 = 30300,
        Zno = 30400,
        Plasticizer = 30500,
        Rubber = 30600,
        DrugMixer = 30700,
        Silane = 30800,
        DrugMixer2 = 30900,
        MixerTime = 32000,
        MixerTemp = 32004,
        MixerEnergy = 32008,
        MixerPress = 32012,
        MixerSpeed = 32016,
        MixerPower = 32020,
        MixerReflectQty = 32024,
        MixerDownTime = 33000,
        MixerDownTemp = 33004,
        MixerDownEnergy = 33008,
        MixerDownPress = 33012,
        MixerDownSpeed = 33016,
        MixerDownPower = 33020,
        MixerDownReflectQty = 33024,
    }

    /// <summary>
    /// MixerAnalogMiningWeight类存放模拟量重量回采的首地址
    /// </summary>
    public enum MixerAnalogMiningWeight
    {
        Carbon = 1000,
        CarbonMid = 1004,
        Carbon2 = 1008,
        CarbonMid2 = 1012,
        Oil = 1016,
        OilMid = 1020,
        Oil2 = 1024,
        Oil2Mid = 1028,
        Zno = 1032,
        ZnoMid = 1036,
        Rubber = 1040,
        DrugMixer = 1044,
        Silane = 1048,
        Plasticizer = 1052,
        DrugMixer2 = 1308
    }

    /// <summary>
    /// MixerAnalogMiningSetBatch类存放模拟量设定车数回采的首地址
    /// </summary>
    public enum MixerAnalogMiningSetBatch
    {
        Carbon = 1080,
        Carbon2 = 1084,
        Oil = 1088,
        Oil2 = 1092,
        Zno = 1096,
        Plasticizer = 1100,
        Rubber = 1104,
        DrugMixer = 1108,
        Silane = 1112,
        Mixer = 1116,
        DrugMixer2 = 1312
    }

    /// <summary>
    /// MixerAnalogMiningActBatch类存放模拟量实际车数回采的首地址
    /// </summary>
    public enum MixerAnalogMiningActBatch
    {
        Carbon = 1120,
        Carbon2 = 1124,
        Oil = 1128,
        Oil2 = 1132,
        Zno = 1136,
        Plasticizer = 1140,
        Rubber = 1144,
        DrugMixer = 1148,
        Silane = 1152,
        Mixer = 1156,
        DrugMixer2 = 1316,
        DownMixer = 1320
    }

    /// <summary>
    /// MixerAnalogMiningMixer类存放模拟量上密炼机数据回采的地址
    /// </summary>
    public enum MixerAnalogMiningMixer
    {
        WorkOrder = 1160,     //班次
        RecipeNo = 1164,      //配方号
        SetBatch = 1168,      //设定车数
        Ramdom = 1172,        //随机数
        Temp = 1176,         //温度
        Power = 1180,        //功率
        Press = 1184,        //压力
        Speed = 1188,        //转速
        Energy = 1192,      //能量
        Time = 1196,       //时间(分步)
        DualScreenWeight = 1200,   //胶双屏重量
        RubberScaleWeightNo = 1204,  //胶料称量顺序
        Voltage = 1208,             //电压
        Reserve = 1212,            //备用
        ReflectQty = 1216,        //反应量
        Current = 1220,           //电流
        DischargeTemp = 1224,    //排胶温度
        DischargePower = 1228,   //排胶功率
        DischargePress = 1232,   //排胶压力
        DischargeEnergy = 1236,  //排胶能量
        TempControl1 = 1288,     //三区温度1
        TempControl2 = 1292,   //三区温度2
        TempControl3 = 1296,   //三区温度3
        MixerTemp2 = 1300,       //密炼机温度2
        MixerTemp3 = 1304,      //密炼机温度3
        RealTimeRam = 1072,     //实时栓位(16进制)
        WeightContinueRecipe = 1064,   //连续配方(称量)
        MixerContinueRecipe = 1068,    //连续配方(密炼)
    }

    /// <summary>
    /// MixerAnalogMiningMixer类存放模拟量下密炼机数据回采的地址
    /// </summary>
    public enum MixerAnalogMiningMixerDown
    {
        WorkOrder = 1380,
        RecipeNo = 1384,
        SetBatch = 1388,
        Ramdom = 1392,
        Temp = 1396,
        Power = 1400,
        Press = 1404,
        Speed = 1408,
        Energy = 1412,
        Time = 1416,
        Voltage = 1420,
        ReflectQty = 1424,
        Current = 1428,
        DischargeTemp = 1432,
        DischargePower = 1436,
        DischargePress = 1440,
        DischargeEnergy = 1444,
        MixerTemp2 = 1448,
        MixerTemp3 = 1452,
    }

    /// <summary>
    /// MixerAnalogMiningMixer类存放模拟量物料称量时间数据回采的地址
    /// </summary>
    public enum MixerAnalogMiningWeightTime
    {
        Carbon1 = 1240,
        Carbon2 = 1244,
        Carbon3 = 1248,
        Oil1 = 1252,
        Oil2 = 1256,
        Oil3 = 1260,
        Silane1 = 1264,
        Silane2 = 1268,
        Silane3 = 1272,
        Oil21 = 1276,   //菜籽油1
        Oil22 = 1280,  //菜籽油2
        Oil23 = 1284, //菜籽油3
    }

    /// <summary>
    /// MixerWeighOKSignal类存放物料称号信号
    /// </summary>
    public enum MixerWeighOKSignal
    {
        Carbon = 848,
        Carbon2 = 849,
        Oil = 850,
        Oil2 = 851,
        Zno = 852,
        Plasticizer = 853,
        Rubber = 856,
        DrugMixer = 857,
        Silane = 858,
        Mixer = 859,
        MixerDown = 860,
        DrugMixer2 = 861
    }

    /// <summary>
    /// MixerDigitalMiningCarbon类存放炭黑数字量回采
    /// </summary>
    public enum MixerDigitalMiningCarbon
    {
        Auto = 600,           //自动
        OverTolerance = 601,   //超差
        Sign = 602,           //正负号
        WeightingOK = 603,    //称好
        WeightingStart = 604,    //开始称量
        MidHaveMat = 605,        //中间斗有料
        MidSign = 606,          //中间斗正负号
        Add1 = 704,              //加1号炭黑
        Add2 = 705,              //加2号炭黑
        Add3 = 706,           //加3号炭黑
        Add4 = 707,           //加4号炭黑
        Add5 = 708,           //加5号炭黑
        Add6 = 709,           //加6号炭黑
        Add7 = 710,           //加7号炭黑
        Add8 = 711,           //加8号炭黑
        Add9 = 712,           //加9号炭黑
        Add10 = 713,          //加10号炭黑
        Drop = 720,             //卸炭黑
        Feeding = 721,          //投炭黑
        DischargeMisalignment = 726,  //炭黑排错位
        Recovery1 = 716,              //回收螺旋1
        Recovery2 = 717,           //回收螺旋2
    }

    /// <summary>
    /// MixerDigitalMiningCarbon2类存放炭黑2数字量回采
    /// </summary>
    public enum MixerDigitalMiningCarbon2
    {
        Auto = 608,           //自动
        OverTolerance = 609,   //超差
        Sign = 610,           //正负号
        WeightingOK = 611,    //称好
        WeightingStart = 612,    //开始称量
        MidHaveMat = 613,        //中间斗有料
        MidSign = 614,          //中间斗正负号
        Add1 = 728,              //加1号炭黑
        Add2 = 729,              //加2号炭黑
        Add3 = 730,           //加3号炭黑
        Add4 = 731,           //加4号炭黑
        Add5 = 732,           //加5号炭黑
        Add6 = 733,           //加6号炭黑
        Add7 = 734,           //加7号炭黑
        Add8 = 735,           //加8号炭黑
        Add9 = 736,           //加9号炭黑
        Add10 = 737,          //加10号炭黑
        Drop = 744,             //卸炭黑
        Feeding = 745,          //投炭黑
        DischargeMisalignment = 750,  //炭黑排错位
        Recovery1 = 742,              //回收螺旋1
        Recovery2 = 743,           //回收螺旋2
    }

    /// <summary>
    /// MixerDigitalMiningOil类存放油料数字量回采
    /// </summary>
    public enum MixerDigitalMiningOil
    {
        Auto = 616,           //自动
        OverTolerance = 617,   //超差
        Sign = 618,           //正负号
        WeightingOK = 619,    //称好
        WeightingStart = 620,    //开始称量
        MidHaveMat = 621,        //中间斗有料
        Add1 = 784,              //加1号油料
        Add2 = 785,              //加2号油料
        Add3 = 786,           //加3号油料
        Add4 = 787,           //加4号油料
        Add5 = 788,           //加5号油料
        Add6 = 789,           //加6号油料
        Add7 = 790,           //加7号油料
        Add8 = 791,           //加8号油料
        Drop = 792,             //卸油料
        Feeding = 793,          //注油料
        Clean = 794,              //清扫
    }

    /// <summary>
    /// MixerDigitalMiningOil类存放油料2数字量回采
    /// </summary>
    public enum MixerDigitalMiningOil2
    {
        Auto = 624,           //自动
        OverTolerance = 625,   //超差
        Sign = 626,           //正负号
        WeightingOK = 627,    //称好
        WeightingStart = 628,    //开始称量
        MidHaveMat = 629,        //中间斗有料
        Add1 = 800,              //加1号油料
        Add2 = 801,              //加2号油料
        Add3 = 802,           //加3号油料
        Add4 = 803,           //加4号油料
        Add5 = 804,           //加5号油料
        Add6 = 805,           //加6号油料
        Add7 = 806,           //加7号油料
        Add8 = 807,           //加8号油料
        Drop = 808,             //卸油料
        Feeding = 809,          //注油料
        Clean = 810,              //清扫
    }

    /// <summary>
    ///MixerDigitalMiningZno类存放粉料数字量回采
    ///料数字量回采
    /// </summary>
    public enum MixerDigitalMiningZno
    {
        Auto = 632,           //自动
        OverTolerance = 633,   //超差
        Sign = 634,           //正负号
        WeightingOK = 635,    //称好
        WeightingStart = 636,    //开始称量
        MidHaveMat = 637,        //中间斗有料
        MidSign = 638,          //中间斗正负号
        Add1 = 752,              //加1号粉料
        Add2 = 753,              //加2号粉料
        Add3 = 754,           //加3号粉料
        Add4 = 755,           //加4号粉料
        Add5 = 756,           //加5号粉料
        Add6 = 757,           //加6号粉料
        Add7 = 758,           //加7号粉料
        Add8 = 759,           //加8号粉料
        Drop = 760,             //卸粉料
        Feeding = 761,          //投粉料
        DischargeMisalignment = 766,  //排错位
    }

    /// <summary>
    /// MixerDigitalMiningZno2类存放粉料2数字量回采
    /// </summary>
    public enum MixerDigitalMiningZno2
    {
        Auto = 640,           //自动
        OverTolerance = 641,   //超差
        Sign = 642,           //正负号
        WeightingOK = 643,    //称好
        WeightingStart = 644,    //开始称量
        MidHaveMat = 645,        //中间斗有料
        MidSign = 646,          //中间斗正负号
        Add1 = 768,              //加1号粉料
        Add2 = 769,              //加2号粉料
        Add3 = 770,           //加3号粉料
        Add4 = 771,           //加4号粉料
        Add5 = 772,           //加5号粉料
        Add6 = 773,           //加6号粉料
        Add7 = 774,           //加7号粉料
        Add8 = 775,           //加8号粉料
        Drop = 776,             //卸粉料
        Feeding = 777,          //投粉料
        DischargeMisalignment = 781,  //排错位
    }

    /// <summary>
    /// MixerDigitalMiningRubber类存放胶料数字量回采
    /// </summary>
    public enum MixerDigitalMiningRubber
    {
        Auto = 648,           //自动
        OverTolerance = 649,   //超差
        Sign = 650,           //正负号
        WeightingOK = 651,    //称好
        WeightingStart = 652,    //开始称量
        DropBeltHaveMat = 653,     //投料带有料
        MidSign = 654,          //胶料秤正负号
        ScreenExchange = 655,     //胶料屏幕切换
        FeedingBeltMotor = 696,   //投料带电机
        RubberScaleMotor = 697,   //胶料秤电机
        FeederMotor1 = 698,      //供胶机电机1
        FeederMotor2 = 699,   //供胶机电机2
        FeedingBeltPE = 700,   //投料带光电
        RubberScalePE = 701,  //胶料秤光电
        RubberDualScreenSign = 702, //胶料双屏正负号
        RubberFilpScreenSignal = 703, //胶料翻屏信号
    }

    /// <summary>
    /// MixerDigitalMiningDrug类存放小药数字量回采
    /// </summary>
    public enum MixerDigitalMiningDrug
    {
        Auto = 656,           //自动
        OverTolerance = 657,   //超差
        Sign = 658,           //正负号
        WeightingOK = 659,    //称好
        WeightingStart = 660,    //开始称量
        WeightingOKDualScreen = 823,   //称好分屏专用信号
        Photoelectricity = 715,        //光电
        Motor = 719,                   //电机
    }

    /// <summary>
    /// MixerDigitalMiningSilane类存放硅烷数字量回采
    /// </summary>
    public enum MixerDigitalMiningSilane
    {
        Auto = 664,           //自动
        OverTolerance = 665,   //超差
        Sign = 666,           //正负号
        WeightingOK = 667,    //称好
        WeightingStart = 668,    //开始称量
        MidHaveMat = 669,        //中间斗有料
        MidSign = 670,          //中间斗正负号
        Add = 824,                //加硅烷
        Drop = 825,               //卸硅烷
        Feeding = 826,            //注硅烷
        Clean = 827,              //硅烷清扫
    }

    /// <summary>
    /// MixerDigitalMiningPlasticizer类存放塑解剂数字量回采
    /// </summary>
    public enum MixerDigitalMiningPlasticizer
    {
        WeightingOKDualScreen = 822,   //称好分屏专用信号
        Photoelectricity = 714,        //光电
        Motor = 718                    //电机
    }

    /// <summary>
    /// MixerDigitalMiningMixer类存放密炼数字量回采
    /// </summary>
    public enum MixerDigitalMiningMixer
    {
        Manual = 680,           //手动
        Auto = 681,           //自动
        Running = 682,        //运行
        MixerAlarm = 683,     //密炼机报警
        ExtruderRunning = 684,  //挤出机运行
        UpperDeviceAlarm = 685,  //上辅机报警
        ExtruderAlarm = 686,  //挤出机报警
        RamHigh = 672,         //上顶栓高位
        RamMid = 673,         //上顶栓中位
        RamLow = 674,         //上顶栓低位
        FeedingDoorOpen = 675, //加料门开
        FeedingDoorClose = 676, //加料门关
        RequestDischargeRubber = 677,  //请求排胶
        BelowRamOpen = 678,           //下顶栓开(卸料门开)
        BelowRamClose = 679,         //下顶栓关(卸料门关)
        CurveStart = 838,           //曲线开始标志位
        CurveEnd = 839,           //曲线结束标志位
    }

    /// <summary>
    /// MixerDigitalMiningMixerDown类存放下密炼数字量回采
    /// </summary>
    public enum MixerDigitalMiningMixerDown
    {
        Manual = 688,           //手动
        Auto = 689,           //自动
        Running = 690,        //运行
        MixerAlarm = 691,     //密炼机报警
        BelowRamOpen = 692,    //下顶栓开(卸料门开)
        BelowRamClose = 693,   //下顶栓关(卸料门关)
        CurveStart = 843,      //曲线开始标志位
        CurveEnd = 844,      //曲线结束标志位
    }

    /// <summary>
    /// MixerDigitalMiningScanner类存放扫描枪数字量回采
    /// </summary>
    public enum MixerDigitalMiningScanner
    {
        CarbonEnable = 39020,      //炭黑是否启用
        ZnoEnable = 39024,        //粉料是否启用
        OilEnable = 39028,       //油料是否启用
        RubberEnable = 39032,    //胶料是否启用
        DrugEnable = 39036,      //小药是否启用
        CarbonBin = 39000,      //炭黑扫描斗号
        ZnoBin = 39004,        //粉料扫描斗号
        OilBin = 39008,       //油料扫描斗号
        RubberScanOK = 39012, //胶料扫描正确信号
        DrugScanOK = 39016,   //小药扫描正确信号
    }

    /// <summary>
    /// MixerTransducerParamsCarbon类存放炭黑变频器参数
    /// </summary>
    public enum MixerTransducerParamsCarbon
    {
        HighSpeed1 = 9000,
        LowSpeed1 = 9004,
        HighSpeed2 = 9008,
        LowSpeed2 = 9012,
        HighSpeed3 = 9016,
        LowSpeed3 = 9020,
        HighSpeed4 = 9024,
        LowSpeed14 = 9028,
        HighSpeed5 = 9032,
        LowSpeed5 = 9036,
        HighSpeed6 = 9040,
        LowSpeed6 = 9044,
        HighSpeed7 = 9048,
        LowSpeed7 = 9052,
        HighSpeed8 = 9056,
        LowSpeed8 = 9060,
        HighSpeed9 = 9064,
        LowSpeed9 = 9068,
        HighSpeed10 = 9072,
        LowSpeed10 = 9076,
    }

    /// <summary>
    /// MixerTransducerParamsCarbon2类存放炭黑2变频器参数
    /// </summary>
    public enum MixerTransducerParamsCarbon2
    {
        HighSpeed1 = 9200,
        LowSpeed1 = 9204,
        HighSpeed2 = 9208,
        LowSpeed2 = 9212,
        HighSpeed3 = 9216,
        LowSpeed3 = 9220,
        HighSpeed4 = 9224,
        LowSpeed14 = 9228,
        HighSpeed5 = 9232,
        LowSpeed5 = 9236,
        HighSpeed6 = 9240,
        LowSpeed6 = 9244,
        HighSpeed7 = 9248,
        LowSpeed7 = 9252,
        HighSpeed8 = 9256,
        LowSpeed8 = 9260,
        HighSpeed9 = 9264,
        LowSpeed9 = 9268,
        HighSpeed10 = 9272,
        LowSpeed10 = 9276,
    }

    /// <summary>
    /// MixerTransducerParamsZon类存放粉料变频器参数
    /// </summary>
    public enum MixerTransducerParamsZno
    {
        HighSpeed1 = 9100,
        LowSpeed1 = 9104,
        HighSpeed2 = 9108,
        LowSpeed2 = 9112,
        HighSpeed3 = 9116,
        LowSpeed3 = 9120,
        HighSpeed4 = 9124,
        LowSpeed14 = 9128,
        HighSpeed5 = 9132,
        LowSpeed5 = 9136,
        HighSpeed6 = 9140,
        LowSpeed6 = 9144,
        HighSpeed7 = 9148,
        LowSpeed7 = 9152,
        HighSpeed8 = 9156,
        LowSpeed8 = 9160,
        HighSpeed9 = 9164,
        LowSpeed9 = 9168,
        HighSpeed10 = 9172,
        LowSpeed10 = 9176,
    }

    /// <summary>
    /// MixerTransportDisplay类存放输送显示各个斗的首地址
    /// </summary>
    public enum MixerTransDisplay
    {
        Carbon = 37000,
        Zno = 37095,
        Oil = 37115,
        Carbon2 = 37131,
        CarbonRecovery = 37145,
    }
}