using GestionPedidosService.Domain.Models.Garments;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Orders
{
    public class OrderReadMobile
    {
        public int Id { get; set; }
        public int UserClientId { get; set; }
        public string NameAtelier { get; set; }
        public string AtelierAddress { get; set; }
        public string CodeOrder { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public IEnumerable<OrderDetailReadMobile> Details {get;set;}
    }
}
