using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Orders
{
    public class OrderDetailReadMobile
    {
        public int Id { get; set; }
        public string NameGarment { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string OrderDetailStatus { get; set; }
    }
}
