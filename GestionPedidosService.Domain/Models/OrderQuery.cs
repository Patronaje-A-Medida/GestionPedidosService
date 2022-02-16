using GestionPedidosService.Domain.Entities;
using System.ComponentModel;

namespace GestionPedidosService.Domain.Models
{
    public class OrderQuery
    {
        public int AtelierId { get; set; }
#nullable enable
        [DefaultValue(null)]
        public string? OrderStatus { get; set; }
        [DefaultValue(null)]
        public string? CodeGarment { get; set; }
        [DefaultValue(null)]
        public int? PageNumber { get; set; }
        [DefaultValue(null)]
        public int? PageSize { get; set; }
    }
}
