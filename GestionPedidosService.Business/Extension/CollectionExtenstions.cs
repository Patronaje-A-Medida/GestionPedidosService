using GestionPedidosService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionPedidosService.Business.Extension
{
    public static class EnumerableExtensions
    {
        public static PagedList<T> ToPagedList<T>(this ICollection<T> entities, int? pageNumber, int? pageSize) where T : class
        {
            int count = entities.Count();

            int page = pageNumber == null ? 0 : pageNumber.Value;
            int size = pageSize == null ? count : pageSize.Value;
            int maxPage = (int) Math.Ceiling(count * 1.0 / size);


            var items = entities.Skip((page - 1) * size).Take(size).ToList();

            return new PagedList<T>
            {
                Items = items,
                Total = count,
                MaxPage = maxPage,
                PageNumber = page,
                ItemsPerPage = size,
            };
        }
    }
}
