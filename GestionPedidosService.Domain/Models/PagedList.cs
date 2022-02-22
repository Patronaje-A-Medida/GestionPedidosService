using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models
{
    public class PagedList<T>
    {
        public ICollection<T> Items { get; set; }
        public int Total{ get; set; }
        public int MaxPage { get; set; }
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
