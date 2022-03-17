using System.Collections.Generic;

namespace GestionPedidosService.Domain.Models
{
    public class PatternGarmentRead
    {
        public string Type { get; set; }
        public string Image { get; set; }
        public int ScaledStatus { get; set; }
        public List<PatternDimensionRead> Dimensions { get; set; }
    }
}
