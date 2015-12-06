using System;

namespace FlexLabs.Web.TablePager
{
    public interface ITableModel
    {
        void UpdateSorter();
        Object SortBy { get; set; }
        Boolean? SortAsc { get; set; }
    }
}
