using System;
using System.Collections.Generic;
using Webserver.Models.ViewModels.Pagination;

namespace Webserver.Services
{
    public interface IPaginationHandler<T, E> where T : class where E : struct, IConvertible
    {
        PagerViewModel<T, E> ConstructViewModel(IEnumerable<T> items, int? currentPage, E sortOrder);

        IEnumerable<T> Sort(IEnumerable<T> items, int sortOrder);
    }
}
