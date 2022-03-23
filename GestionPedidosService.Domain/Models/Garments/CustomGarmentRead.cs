using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class CustomGarmentRead
    {
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public double EstimatedPrice { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<string> Fabrics { get; set; }
        public IEnumerable<string> Occasions { get; set; }
        public IEnumerable<string> Images { get; set; }

    }
}
