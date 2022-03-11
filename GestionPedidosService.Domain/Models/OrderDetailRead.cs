using GestionPedidosService.Domain.Models.Garments;
using System;

namespace GestionPedidosService.Domain.Models
{
    public class OrderDetailRead
    {
        public string CodeOrder { get; set; }
        public string OrderDetailStatus { get; set; }
        public string AttendedBy { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomGarmentRead Garment { get; set; }
        public UserClientMin Client { get; set; }
    }
}
