using System;
using System.Collections.Generic;

namespace Webserver.Models.ViewModels.Pagination
{
    public class PagerViewModel<T, E> where T : class where E : struct, IConvertible
    {
        public IEnumerable<T> Items { get; set; }

        public Pager Pager { get; set; }

        public E SortOrder { get; set; }
    }
}