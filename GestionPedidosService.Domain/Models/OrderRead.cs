using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionPedidosService.Domain.Models
{
    public class OrderRead
    {
        public int Id { get; set; }
        public string CodeOrder { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string AttendedBy { get; set; }
        public IEnumerable<OrderDetailMin> Details { get; set; }
        public UserClientMin Client {get;set;}
    }
}
