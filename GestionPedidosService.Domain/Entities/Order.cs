using System;
using System.Collections.Generic;
using GestionPedidosService.Domain.Base;

namespace GestionPedidosService.Domain.Entities
{
    public class Order : Auditable
    {
        public int Id { get; set; }
        public string CodeOrder { get; set; }
        public EOrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        
        public ICollection<Garment> Garments { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        
        public int AtelierId { get; set; }
        public int UserClientId { get; set; }
        public int UserAtelierId { get; set; }
    }
}