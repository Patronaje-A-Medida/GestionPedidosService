﻿using System.Collections.Generic;
using GestionPedidosService.Domain.Base;

namespace GestionPedidosService.Domain.Entities
{
    public class Garment : Auditable
    {
        public int Id { get; set; }
        public string CodeGarment { get; set; }
        public string NameGarment { get; set; }
        public double FirstRangePrice { get; set; }
        public double SecondRangePrice { get; set; }
        public byte Available { get; set; }
        
        public ICollection<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        
        public List<FeatureGarment> FeatureGarments { get; set; }
        public List<PatternGarment> PatternGarments { get; set; }
        
        public int AtelierId { get; set; }
    }
}