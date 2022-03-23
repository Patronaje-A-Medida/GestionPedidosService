using System.Collections.Generic;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentRead
    {
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public string Description { get; set; }
        public double FirstRangePrice { get; set; }
        public double SecondRangePrice { get; set; }
        public bool Available { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public IEnumerable<string> Fabrics { get; set; }
        public IEnumerable<string> Occasions { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<string> Patterns { get; set; }
    }
}
