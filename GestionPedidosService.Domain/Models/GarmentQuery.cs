﻿using System.ComponentModel;

namespace GestionPedidosService.Domain.Models
{
    public class GarmentQuery
    {
        public int AtelierId { get; set; }

        #nullable enable
        [DefaultValue(null)]
        public string? FilterString { get; set; }

        [DefaultValue(null)]
        public string? Category { get; set; }
    }
}