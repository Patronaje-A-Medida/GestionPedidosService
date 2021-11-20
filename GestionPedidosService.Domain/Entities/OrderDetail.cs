using GestionPedidosService.Domain.Base;

namespace GestionPedidosService.Domain.Entities
{
    public class OrderDetail : Auditable
    {
        public int Id { get; set; }
        public string Color { get; set; }
        
        public int GarmentId { get; set; }
        public Garment Garment { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}