using Repository.Model;

namespace Repository.GlobalConfig
{
    public interface IReportForm
    {
        void QueryReport(PM_OrderTran model);
    }

    public interface IRefresh
    {
        void RefreshData();

        void RefreshData(bool isWeight);
    }

    public interface ScanRefresh
    {
        void ScanRefresh(WorkType work, string barCode, string type, string scaninfo);
    }
}