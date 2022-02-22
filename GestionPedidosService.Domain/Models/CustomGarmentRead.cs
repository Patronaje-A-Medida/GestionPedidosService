using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models
{
    public class CustomGarmentRead
    {
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public double EstimatedPrice { get; set; }
        public IEnumerable<FeatureRead> Features { get; set; }

    }
}
