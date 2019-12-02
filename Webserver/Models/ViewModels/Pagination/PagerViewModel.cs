using System.Collections.Generic;

namespace Webserver.Models.ViewModels.Pagination
{
    public class PagerViewModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public Pager Pager { get; set; }
    }
}