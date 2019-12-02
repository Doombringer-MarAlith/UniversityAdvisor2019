using System;

namespace Webserver.Models.ViewModels.Pagination
{
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            var totalPageCount = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage -= startPage - 1;
                startPage = 1;
            }

            if (endPage > totalPageCount)
            {
                endPage = totalPageCount;

                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPageCount = totalPageCount;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPageCount { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}