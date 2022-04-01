using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentReadMobile
    {
        public int Id { get; set; }
        public string NameGarment { get; set; }
        public string Description { get; set; }
        public double FirstRangePrice { get; set; }
        public double SecondRangePrice { get; set; }
        public string Category { get; set; }
        public string NameAtelier { get; set; }
        public string AtelierAddress { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public IEnumerable<string> Fabrics { get; set; }
        public IEnumerable<string> Occasions { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
