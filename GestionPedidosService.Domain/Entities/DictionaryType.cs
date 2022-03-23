using GestionPedidosService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Entities
{
    public class DictionaryType : Auditable
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? ParentType { get; set; }
        public int? AtelierId { get; set; }
    }
}
