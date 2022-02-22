using System;
using System.Collections.Generic;

namespace GestionPedidosService.Domain.Models
{
    public class OrderDetailRead
    {
        /*public int Id { get; set; }
        public string Code { get; set; }
        public int ClientId { get; set; }
        public string Date { get; set; }
        public string GarmentCode { get; set; }
        public int AtelierId { get; set; }
        public string GarmentName { get; set; }
        public string SelectedColor { get; set; }
        public string State { get; set; }
        public double Price { get; set; }
        public List<FeatureRead> Features { get; set; }*/
        public CustomGarmentRead Garment { get; set; }
        public UserClientMin Client { get; set; }
        public string CodeOrder { get; set; }
        public string OrderDetailStatus { get; set; }
        public string AttendedBy { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
