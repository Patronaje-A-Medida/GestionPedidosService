using System.ComponentModel;

namespace GestionPedidosService.Domain.Models
{
    public class OrderQuery
    {
#nullable enable
        [DefaultValue(null)]
        public int? OrderStatus { get; set; }
        [DefaultValue(null)]
        public string? GarmentCode { get; set; }
        [DefaultValue(null)]
        public int? PageNumber { get; set; }
        [DefaultValue(null)]
        public int? PageSize { get; set; }
    }
}
