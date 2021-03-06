using GestionPedidosService.Domain.Base;
using GestionPedidosService.Domain.Utils;

namespace GestionPedidosService.Domain.Entities
{
    public class FeatureGarment : Auditable
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string TypeFeature { get; set; }
        public int TypeFeatureValue { get; set; }
        
        public int GarmentId { get; set; }
        public Garment Garment { get; set; }
    }
}