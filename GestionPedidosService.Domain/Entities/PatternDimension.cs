namespace GestionPedidosService.Domain.Entities
{
    public class PatternDimension
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public double Value { get; set; }
        public string Units { get; set; }
        
        public int PatternGarmentId { get; set; }
        public PatternGarment PatternGarment { get; set; }
    }
}