using System;
using System.Collections.Generic;

namespace GestionPedidosService.Domain.Models
{
    public class OrderDetailRead
    {
        public string Code { get; set; }
        public int ClientId { get; set; }
        public string Date { get; set; }
        public string GarmentCode { get; set; }
        public int AtelierId { get; set; }
        public string GarmentName { get; set; }
        public string SelectedColor { get; set; }
        public string State { get; set; }
        public double Price { get; set; }
        public List<FeatureRead> Features { get; set; }
    }
}
