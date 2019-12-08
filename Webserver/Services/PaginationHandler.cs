using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Webserver.Enums;
using Webserver.Models.ViewModels.Pagination;

namespace Webserver.Services
{
	public class PaginationHandler<T, E> : IPaginationHandler<T, E> where T : class where E : struct, IConvertible
    {
		public PagerViewModel<T, E> ConstructViewModel(IEnumerable<T> items, int? currentPage, E sortOrder)
        {
			if (!typeof(E).IsEnum)
            {
                throw new InvalidCastException("PaginationHandler::ConstructViewModel(): Not an enum!");
            }

			try
            {
                dynamic value = sortOrder;
                var convertedSortOrder = (Int32)value;
                items = Sort(items, convertedSortOrder);
            }
			catch
            {
                throw;
            }

            var pager = new Pager(items.Count(), currentPage);

            var viewModel = new PagerViewModel<T, E>
            {
                Items = items.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
                SortOrder = sortOrder
            };

            return viewModel;
        }

		public IEnumerable<T> Sort(IEnumerable<T> items, int sortOrder)
        {
			if (items.GetType().GetGenericArguments()[0].Name == typeof(University).Name)
            {
                IEnumerable<University> convertedItems = items.Cast<University>();

				switch (sortOrder)
                {
                    case (Int32)UniversitySortOrder.CITY_ASC:
                        return convertedItems.OrderBy(university => university.City).Cast<T>();
                    case (Int32)UniversitySortOrder.CITY_DESC:
                        return convertedItems.OrderByDescending(university => university.City).Cast<T>();
                    case (Int32)UniversitySortOrder.NAME_DESC:
                        return convertedItems.OrderByDescending(university => university.Name).Cast<T>();
                    default:
                        return convertedItems.OrderBy(university => university.Name).Cast<T>();
                }
            }
			else if (items.GetType().GetGenericArguments()[0].Name == typeof(Review).Name)
            {
                IEnumerable<Review> convertedItems = items.Cast<Review>();

				switch (sortOrder)
                {
                    case (Int32)ReviewSortOrder.VALUE_ASC:
                        return convertedItems.OrderBy(review => review.Value).Cast<T>();
                    case (Int32)ReviewSortOrder.VALUE_DESC:
                        return convertedItems.OrderByDescending(review => review.Value).Cast<T>();
                    case (Int32)ReviewSortOrder.DATE_ASC:
                        return null;
                    case (Int32)ReviewSortOrder.DATE_DESC:
                    default:
                        return null;
                }
            }

            return null;
        }
	}
}
