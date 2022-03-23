using System.ComponentModel;

namespace GestionPedidosService.Domain.Models
{
    public class OrderQuery
    {
        public int AtelierId { get; set; }

        #nullable enable
        [DefaultValue(null)]
        public int? OrderStatus { get; set; }
        [DefaultValue(null)]
        public string? FilterString { get; set; }
        [DefaultValue(null)]
        public int? PageNumber { get; set; }
        [DefaultValue(null)]
        public int? PageSize { get; set; }
    }
}
