using GestionPedidosService.Domain.Models.FeatureGarments;
using System.Collections.Generic;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentWrite
    {
        public int Id { get; set; }
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public double FirstRangePrice { get; set; }
        public double SecondRangePrice { get; set; }
        public bool Available { get; set; }
        public int AtelierId { get; set; }
        public ICollection<FeatureGarmentWrite> Features { get; set; }
        public IEnumerable<GarmentImageString> Images { get; set; }
        //public IEnumerable<GarmentImageFile> Images { get; set; }
        public IEnumerable<GarmentImageString> Patterns { get; set; }

    }
}
