namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentMinWeb
    {
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public string ImageUrl { get; set; }
        //public string Category { get; set; }
        public double AveragePrice { get; set; }
        public bool Available { get; set; }
    }
}
