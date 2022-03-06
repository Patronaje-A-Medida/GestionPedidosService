namespace GestionPedidosService.Domain.Models
{
    public class GarmentMin
    {
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public double AveragePrice { get; set; }
        public bool Available { get; set; }
    }
}
