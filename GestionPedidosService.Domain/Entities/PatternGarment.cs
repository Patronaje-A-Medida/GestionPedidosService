using System.Collections.Generic;

namespace GestionPedidosService.Domain.Entities
{
    public class PatternGarment
    {
        public int Id { get; set; }
        public string TypePattern { get; set; }
        public string ImagePattern { get; set; }
        public int ResizedStatus { get; set; }
        
        public int GarmentId { get; set; }
        public string NamePattern { get; set; }
        public Garment Garment { get; set; }
        
        public List<PatternDimension> PatternDimensions { get; set; }
    }
}