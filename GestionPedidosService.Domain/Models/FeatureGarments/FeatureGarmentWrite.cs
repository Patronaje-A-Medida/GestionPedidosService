using GestionPedidosService.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.FeatureGarments
{
    public class FeatureGarmentWrite
    {
        public string Value { get; set; }
        public string TypeFeature { get; set; }
        public int TypeFeatureValue { get; set; }
    }
}
