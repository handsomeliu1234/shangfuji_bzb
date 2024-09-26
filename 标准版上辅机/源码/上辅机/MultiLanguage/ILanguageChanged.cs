using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLanguage
{
    /// <summary>
    /// 语言切换接口
    /// </summary>
    public interface ILanguageChanged
    {
        /// <summary>
        /// 当切换语言时调用所有活动窗体的该方法,根据资源id更新所有控件里面的数据
        /// </summary>
        /// <param name="language"></param>
        void LanguageChanged(SupportLanguageType language);
    }

    /// <summary>
    /// 支持的语言,其他语言支持请自行添加
    /// </summary>
    public enum SupportLanguageType
    {
        Chinese,  //中文
        English,  //英语
        Vietnamese, //越南语
        Thai      //泰国语
    }
}
