using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentMinMobile
    {
        public int Id { get; set; }
        public string NameGarment { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string NameAtelier { get; set; }
    }
}
