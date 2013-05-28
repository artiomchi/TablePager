﻿using PagedList;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FlexLabs.Web.TablePager
{
    public class TableModel
    {
        public static Int32 DefaultPageSize = 25;
        public static Int32 DefaultPageRange = 10;
        public static Int32[] DefaultPageSizes = new[] { 10, 25, 50, 100 };

        internal static IEnumerable<SelectListItem> GetPageSizes(Int32[] pageSizes = null, Int32? currentSize = null)
        {
            if (pageSizes == null)
                pageSizes = TableModel.DefaultPageSizes;
            if (currentSize.HasValue && !pageSizes.Contains(currentSize.Value))
                pageSizes = pageSizes.Union(new[] { currentSize.Value }).OrderBy(p => p).ToArray();
            return pageSizes.Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = i == TableModel.DefaultPageSize });
        }
    }

    [ModelBinder(typeof(TableModelBinder))]
    public abstract class TableModel<TSorter, TModel> : TableModel, ITableModel where TSorter : struct
    {
        public TableModel(TSorter defaultSorter, Boolean defaultAscending)
        {
            DefaultSortBy = defaultSorter;
            DefaultSortAsc = defaultAscending;
        }
        private TSorter DefaultSortBy;
        private Boolean DefaultSortAsc;

        public TSorter? ChangeSort { get; set; }
        public TSorter? SortBy { get; set; }
        public Boolean? SortAsc { get; set; }
        public Int32? PageSize { get; set; }
        public Int32? Page { get; set; }
        object ITableModel.SortBy { get { return SortBy; } set { SortBy = (TSorter?)value; } }

        public IPagedList<TModel> PageItems;

        public void SetPageItems(IEnumerable<TModel> items)
        {
            PageItems = items.ToPagedList(Page ?? 1, PageSize ?? DefaultPageSize);
        }

        public void UpdateSorter()
        {
            if (ChangeSort.HasValue)
            {
                if (ChangeSort.Value.Equals(SortBy ?? DefaultSortBy))
                {
                    SortAsc = !SortAsc.GetValueOrDefault(DefaultSortAsc);
                }
                else
                {
                    SortBy = ChangeSort;
                    SortAsc = true;
                }
                if (SortAsc.HasValue && SortAsc == DefaultSortAsc)
                    SortAsc = null;
                if (SortBy.HasValue && SortBy.Value.Equals(DefaultSortBy))
                    SortBy = null;
            }
        }

        public TSorter GetSortBy() { return SortBy ?? DefaultSortBy; }
        public Boolean GetSortAsc() { return SortAsc ?? DefaultSortAsc; }
    }
}
