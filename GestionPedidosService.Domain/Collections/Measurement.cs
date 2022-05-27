using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Collections
{
    public class Measurement
    {
        public string name_measurement { get; set; }
        public double value { get; set; }
        public string acronym { get; set; }
        public string units { get; set; }
    }
}
