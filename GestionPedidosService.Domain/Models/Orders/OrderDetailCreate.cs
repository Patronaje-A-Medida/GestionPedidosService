using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Orders
{
    public class OrderDetailCreate
    {
        public int GarmentId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}
