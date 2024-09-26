using System.Windows.Forms;

namespace NewuCommon
{
    public interface IShellForm
    {
        TabControl GetTabControl();

        Form GetFormByClassName(string name);
    }
}