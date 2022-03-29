using System.Collections.Generic;
using System.ComponentModel;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentQuery
    {
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Occasions { get; set; }
        public IEnumerable<bool> Availabilities { get; set; }

        #nullable enable
        [DefaultValue(null)]
        public int? AtelierId { get; set; }

        [DefaultValue(null)]
        public string? FilterString { get; set; }

        [DefaultValue(null)]
        public int? PageNumber { get; set; }

        [DefaultValue(null)]
        public int? PageSize { get; set; }

    }
}
