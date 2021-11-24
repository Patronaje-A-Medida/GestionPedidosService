using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models
{
    public class OrderQuery
    {
        public int OrderStatus { get; set; }
        public string GarmentCode { get; set; }
    }
}
