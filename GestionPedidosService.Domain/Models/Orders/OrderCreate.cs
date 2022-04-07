using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Orders
{
    public class OrderCreate
    {
        public DateTime OrderDate { get; set; }
        public int UserClientId { get; set; }
        public ICollection<OrderDetailCreate> Details { get; set; }
    }
}
