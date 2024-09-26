using System.Collections.Generic;

namespace NewuSoft
{
    class BUG_FIX_LOG
    {
        /**
         * ——------BUG---------
         * BUG.Add("未解决","");
         */
        BUG_FIX_LOG()
        {
            Dictionary<string, string> BUG = new Dictionary<string, string>();
            BUG.Add("OJBK", "投入时  工艺匹配流程算法修改   投料顺序");   
            BUG.Add("OJBK", "添加投料顺序时，根据当前步数递增 ");
            BUG.Add("OJBK！！！", "配方所有操作有事物回滚功能");
            BUG.Add("OJBK", "刚进入配方库的时候，并未完全刷新");
            BUG.Add("OJBK", "配方中  第二步骤 中 温度写入大数值时 为0  查看数据库存储   ans:数据format 中有分号,转Int 捕获异常返回0 ");
            BUG.Add("OJBK", "投入时  工艺匹配流程算法修改   投料顺序  修改bug");
            BUG.Add("OJBK", "配方中 工艺参数 插入设备ID为null 再次读出来 则为空  导致无法保存   ans:插入时 没有将三个参数传入 已添加");
            BUG.Add("OJBK", " 胶料秤值 逻辑错误，显示错乱");
            BUG.Add("OJBK", "下定栓开，清空蜜炼工艺参数部分，等待下一车次的数据更新 ");
            BUG.Add("OJBK", "秤数 显示格式化    添加发送粉料的标志位 和 设定车数");
            BUG.Add("OJBK", "编辑配方时，必须输入快秤和提前量，胶料则无需输入  默认为  零");
            BUG.Add("OJBK", "格式化参数显示   标准化  模块化配置秤值的精度   Bll.ScaleAccuracy.cs");
            BUG.Add("OJBK", "删除配方时   会有其他配方使用该配方   且发送订单列表中有引用  导致无法删除   已经删除主键依赖  但应在业务层提醒其order_tran中删除");
            BUG.Add("OJBK", "配方库中  被其他配方持有引用 导致该配方无法删除 且数据无法回滚");
            BUG.Add("未解决", "储斗  储罐编号 和  材料类型 加约束  不允许加入重复储罐编号 和 材料");
            //严重  一脸懵B BUG
            BUG.Add("未解决", "ViewDisPlay  表，先后打开   先打开的不更新问题      虽不影响正常使用 但应找出原因");
        }

    }

}
