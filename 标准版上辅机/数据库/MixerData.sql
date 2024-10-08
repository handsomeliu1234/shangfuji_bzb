USE [master]
GO
/****** Object:  Database [StandardMixer_Data]    Script Date: 2024/1/22 09:53:36 ******/
CREATE DATABASE [StandardMixer_Data]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StandardMixer_Data', FILENAME = N'D:\Database\StandardMixer_Data.mdf' , SIZE = 5567040KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StandardMixer_Data_log', FILENAME = N'D:\Database\StandardMixer_Data_log.ldf' , SIZE = 69568KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StandardMixer_Data].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StandardMixer_Data] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET ARITHABORT OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StandardMixer_Data] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StandardMixer_Data] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StandardMixer_Data] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StandardMixer_Data] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET RECOVERY FULL 
GO
ALTER DATABASE [StandardMixer_Data] SET  MULTI_USER 
GO
ALTER DATABASE [StandardMixer_Data] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StandardMixer_Data] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StandardMixer_Data] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StandardMixer_Data] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [StandardMixer_Data]
GO
/****** Object:  Table [dbo].[_temp]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_temp](
	[DevicePartCode] [varchar](50) NULL,
	[SetMaterialCode] [nvarchar](50) NULL,
	[DropOrder] [int] NULL,
	[SetBinNo] [int] NULL,
	[FactOrder] [int] NULL,
	[SetWeight] [decimal](18, 3) NULL,
	[ActWeight] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2023_RPT_CurveF]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2023_RPT_CurveF](
	[CurveID] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
	[UpdateTotal] [int] NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[DeviceCode] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[RealTime] [nvarchar](max) NULL,
	[Temp] [nvarchar](max) NULL,
	[Power] [nvarchar](max) NULL,
	[Press] [nvarchar](max) NULL,
	[Energy] [nvarchar](max) NULL,
	[Speed] [nvarchar](max) NULL,
	[Reserve1] [nvarchar](max) NULL,
	[Reserve2] [nvarchar](max) NULL,
	[Reserve3] [nvarchar](max) NULL,
	[Reserve4] [nvarchar](max) NULL,
	[Reserve5] [nvarchar](max) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2023_RPT_DeviceEventF]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2023_RPT_DeviceEventF](
	[DeviceEventID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[EventType] [nvarchar](50) NOT NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[OrderID] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[UseTime] [int] NULL,
	[InitTime] [datetime] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[Temp] [decimal](18, 3) NULL,
	[Power] [decimal](18, 3) NULL,
	[Energy] [decimal](18, 3) NULL,
	[Speed] [decimal](18, 3) NULL,
	[Press] [decimal](18, 3) NULL,
	[PmMode] [nvarchar](50) NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2023_RPT_MixStepF]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2023_RPT_MixStepF](
	[MixStepID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[DevicePartCode] [nvarchar](50) NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[StepOrder] [int] NULL,
	[StepName] [nvarchar](50) NULL,
	[ActionControlName] [nvarchar](50) NULL,
	[SetTime] [decimal](18, 3) NULL,
	[ActTime] [decimal](18, 3) NULL,
	[SetTemp] [decimal](18, 3) NULL,
	[ActTemp] [decimal](18, 3) NULL,
	[SetPower] [decimal](18, 3) NULL,
	[ActPower] [decimal](18, 3) NULL,
	[SetEnergy] [decimal](18, 3) NULL,
	[ActEnergy] [decimal](18, 3) NULL,
	[SetPress] [decimal](18, 3) NULL,
	[ActPress] [decimal](18, 3) NULL,
	[SetSpeed] [decimal](18, 3) NULL,
	[ActSpeed] [decimal](18, 3) NULL,
	[KeepTime] [decimal](18, 3) NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[SaveTime] [datetime] NOT NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2023_RPT_WeightF]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2023_RPT_WeightF](
	[WeightID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[DevicePartCode] [nvarchar](50) NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[TypeCodeName] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[SetMaterialCode] [nvarchar](50) NULL,
	[SetBinNo] [int] NULL,
	[SetWeight] [decimal](18, 3) NULL,
	[AllowError] [decimal](18, 3) NULL,
	[ActWeight] [decimal](18, 3) NULL,
	[ActError] [decimal](18, 3) NULL,
	[WeightOrder] [int] NULL,
	[DropOrder] [int] NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[SaveTime] [datetime] NOT NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2024_RPT_Curve]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2024_RPT_Curve](
	[CurveID] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
	[UpdateTotal] [int] NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[DeviceCode] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[RealTime] [nvarchar](max) NULL,
	[Temp] [nvarchar](max) NULL,
	[Power] [nvarchar](max) NULL,
	[Press] [nvarchar](max) NULL,
	[Energy] [nvarchar](max) NULL,
	[Speed] [nvarchar](max) NULL,
	[Reserve1] [nvarchar](max) NULL,
	[Reserve2] [nvarchar](max) NULL,
	[Reserve3] [nvarchar](max) NULL,
	[Reserve4] [nvarchar](max) NULL,
	[Reserve5] [nvarchar](max) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2024_RPT_DeviceEvent]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2024_RPT_DeviceEvent](
	[DeviceEventID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[EventType] [nvarchar](50) NOT NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[OrderID] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[UseTime] [int] NULL,
	[InitTime] [datetime] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[Temp] [decimal](18, 3) NULL,
	[Power] [decimal](18, 3) NULL,
	[Energy] [decimal](18, 3) NULL,
	[Speed] [decimal](18, 3) NULL,
	[Press] [decimal](18, 3) NULL,
	[PmMode] [nvarchar](50) NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2024_RPT_MixStep]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2024_RPT_MixStep](
	[MixStepID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[DevicePartCode] [nvarchar](50) NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[StepOrder] [int] NULL,
	[StepName] [nvarchar](50) NULL,
	[ActionControlName] [nvarchar](50) NULL,
	[SetTime] [decimal](18, 3) NULL,
	[ActTime] [decimal](18, 3) NULL,
	[SetTemp] [decimal](18, 3) NULL,
	[ActTemp] [decimal](18, 3) NULL,
	[SetPower] [decimal](18, 3) NULL,
	[ActPower] [decimal](18, 3) NULL,
	[SetEnergy] [decimal](18, 3) NULL,
	[ActEnergy] [decimal](18, 3) NULL,
	[SetPress] [decimal](18, 3) NULL,
	[ActPress] [decimal](18, 3) NULL,
	[SetSpeed] [decimal](18, 3) NULL,
	[ActSpeed] [decimal](18, 3) NULL,
	[KeepTime] [decimal](18, 3) NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[SaveTime] [datetime] NOT NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[2024_RPT_Weight]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[2024_RPT_Weight](
	[WeightID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[DevicePartCode] [nvarchar](50) NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[TypeCodeName] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[SetMaterialCode] [nvarchar](50) NULL,
	[SetBinNo] [int] NULL,
	[SetWeight] [decimal](18, 3) NULL,
	[AllowError] [decimal](18, 3) NULL,
	[ActWeight] [decimal](18, 3) NULL,
	[ActError] [decimal](18, 3) NULL,
	[WeightOrder] [int] NULL,
	[DropOrder] [int] NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[SaveTime] [datetime] NOT NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RPT_Weight]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RPT_Weight](
	[WeightID] [nvarchar](50) NOT NULL,
	[DeviceCode] [nvarchar](50) NOT NULL,
	[DevicePartCode] [nvarchar](50) NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[MaterialCode] [nvarchar](50) NULL,
	[TypeCodeName] [nvarchar](50) NULL,
	[VersionNo] [nvarchar](50) NULL,
	[Lot] [nvarchar](50) NULL,
	[PlanQty] [int] NULL,
	[FactOrder] [int] NULL,
	[SetMaterialCode] [nvarchar](50) NULL,
	[SetBinNo] [int] NULL,
	[SetWeight] [decimal](18, 3) NULL,
	[AllowError] [decimal](18, 3) NULL,
	[ActWeight] [decimal](18, 3) NULL,
	[ActError] [decimal](18, 3) NULL,
	[WeightOrder] [int] NULL,
	[DropOrder] [int] NULL,
	[WorkGroup] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[WorkerUserCode] [nvarchar](50) NULL,
	[SaveTime] [datetime] NOT NULL,
	[Reserve1] [nvarchar](50) NULL,
	[Reserve2] [nvarchar](50) NULL,
	[Reserve3] [nvarchar](50) NULL,
	[Reserve4] [nvarchar](50) NULL,
	[Reserve5] [nvarchar](50) NULL,
	[VersionID] [int] NULL,
	[Is_Read] [int] NULL,
	[ReadTime] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2023_RPT_CurveF]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2023_RPT_DeviceEventF]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2023_RPT_MixStepF]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2023_RPT_WeightF]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2024_RPT_Curve]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2024_RPT_DeviceEvent]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2024_RPT_MixStep]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_index]    Script Date: 2024/1/22 09:53:36 ******/
CREATE NONCLUSTERED INDEX [IX_index] ON [dbo].[2024_RPT_Weight]
(
	[OrderID] ASC,
	[FactOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[getBatchReport]    Script Date: 2024/1/22 09:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[getBatchReport]
	@OrderID varchar(50),
	@DateYear varchar(4)
AS

BEGIN
	---判断表是否存在
	if exists (select name from sysobjects where name = '_temp')
	Begin
		drop table _temp
	End
	
	--创建临时表
	if object_id('tempdb..#temp_weight') is not null 
	Begin
		drop table #temp_weight
	End

	CREATE TABLE #temp_weight
	(
		DevicePartCode varchar(50),
		SetMaterialCode nvarchar(50),
		DropOrder int,
		SetBinNo int,
		FactOrder int,
		SetWeight decimal(18,3),
		ActWeight nvarchar(50)
	)

	DECLARE @TableName varchar(50)
	DECLARE @sql varchar(2000)
	set @TableName ='[' + @DateYear +'_RPT_Weight]'
	----将存储过程数据保存到临时表
	print @TableName
	set @sql ='insert into #temp_weight SELECT DevicePartCode,SetMaterialCode,DropOrder,SetBinNo,FactOrder,Max(SetWeight) as SetWeight,Max(ActWeight) as ActWeight FROM '+ @TableName + ' WHERE OrderID = '''+ @OrderID  + '''  GROUP BY DevicePartCode,SetMaterialCode,DropOrder,SetBinNo,FactOrder'
	print @sql
	exec(@sql)

	--获取排胶信息
	DECLARE @factorder NVARCHAR(50),@Temp decimal(18,0),@Power decimal(18,0),@Energy decimal(18,1),@Speed decimal(18,0),@Press decimal(18,1),@useTime int,@pmMode nvarchar(50),@spareTime nvarchar(50)
	DECLARE @starttime datetime,@endtime datetime

	set @TableName ='[' + @DateYear +'_RPT_DeviceEvent]'
	set @sql ='SELECT FactOrder,Temp,[Power],Energy,Speed,Press,UseTime,PmMode,WorkGroup,StartTime,EndTime FROM ' + @TableName + ' WHERE  OrderID = '''+ @OrderID  + ''' and EventType=''作业'' ORDER BY FactOrder '
	set @sql ='SELECT FactOrder,Temp,[Power],Energy,Speed,Press,UseTime,PmMode,WorkGroup,StartTime,EndTime FROM ' + @TableName + ' WHERE  OrderID = '''+ @OrderID  + ''' and EventType=''作业'' ORDER BY FactOrder '

	--创建临时表存储deviceEvent数据
	create table #tempEvent
	(
		FactOrder int,
		Temp decimal(18,1),
		[Power] decimal(18,0),
		Energy decimal(18,1),
		Speed decimal(18,0),
		Press decimal(18,1),
		UseTime decimal(18,1),
		PmMode nvarchar(50),
		Reserve4 nvarchar(50),
		startTime datetime,
		endTime datetime
	)

	insert #tempEvent exec(@sql)

	DECLARE rs CURSOR LOCAL SCROLL FOR
		select * from #tempEvent
	OPEN rs

		DECLARE @Mcount int
		set @Mcount =0

		FETCH NEXT FROM rs INTO @factorder,@Temp,@Power,@Energy,@Speed,@Press,@useTime,@pmMode,@spareTime,@starttime,@endtime
		While ( @@Fetch_Status=0 )
			BEGIN
				set @Mcount =@Mcount+1
				print ''''+@factorder+'''' +',';---处理结果 CONVERT(varchar(100), @startTime, 120) 120这个数值显示不同的日期格式，查询百度查看（24,代表显示日期，120显示年月日）
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','开始时间',CONVERT(varchar(100), @startTime, 24),@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','结束时间',CONVERT(varchar(100), @endTime, 24),@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','配方时间',@useTime,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','间隔时间',@spareTime,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','温度',@Temp,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','功率',@Power,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','能量',@Energy,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','转速',@Speed,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','压力',@Press,@Mcount,0)
				INSERT INTO #temp_weight(DevicePartCode,SetMaterialCode,ActWeight,FactOrder,DropOrder) Values('0','模式',@pmMode,@Mcount,0)
				FETCH NEXT FROM rs INTO @factorder,@Temp,@Power,@Energy,@Speed,@Press,@useTime,@pmMode,@spareTime,@starttime,@endtime
			END
		

		CLOSE rs
		Deallocate rs

	SELECT * INTO _temp FROM #temp_weight

	SELECT * FROM _temp ORDER BY FactOrder,DevicePartCode,DropOrder,SetBinNo --最终查询的数据 --最终查询的数据

	drop table #temp_weight   ---删除临时表
	drop table #tempEvent

END
GO
USE [master]
GO
ALTER DATABASE [StandardMixer_Data] SET  READ_WRITE 
GO
