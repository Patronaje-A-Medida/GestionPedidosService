using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentUpdateImages
    {
        public string CodeGarment { get; set; }
        public int AtelierId { get; set; }
        public ICollection<GarmentUpdateFile> Images { get; set; }
    }
}
