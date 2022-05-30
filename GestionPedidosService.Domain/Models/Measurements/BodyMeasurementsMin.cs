using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Measurements
{
    public class BodyMeasurementsMin
    {
        public string id { get; set; }
        public int client_id { get; set; }
        public DateTime measurement_date { get; set; }
        public double height { get; set; }
        public double bust { get;  set; }
        public double waist { get; set; }
        public double hip { get; set; }
    }
}
