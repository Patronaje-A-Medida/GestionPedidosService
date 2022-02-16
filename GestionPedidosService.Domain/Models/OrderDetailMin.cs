using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models
{
    public class OrderDetailMin
    {
        public int Id { get; set; }
        public string CodeGarment { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string OrderDetailStatus { get; set; }
    }
}
