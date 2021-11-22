using System;
using System.ComponentModel.DataAnnotations;

namespace GestionPedidosService.Domain.Models
{
    public class OrderRead
    {
        public int Id { get; set; }
        public int GarmentId { get; set; }
        public string GarmentCode { get; set; }
        public int Client { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
    }
}
