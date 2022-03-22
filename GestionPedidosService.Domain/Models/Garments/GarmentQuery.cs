using System.Collections.Generic;
using System.ComponentModel;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentQuery
    {
        public int AtelierId { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Occasions { get; set; }

        #nullable enable
        [DefaultValue(null)]
        public string? FilterString { get; set; }

        [DefaultValue(null)]
        public int? Category { get; set; }

        [DefaultValue(null)]
        public int? PageNumber { get; set; }

        [DefaultValue(null)]
        public int? PageSize { get; set; }

    }
}
