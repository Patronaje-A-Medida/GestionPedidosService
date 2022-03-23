using GestionPedidosService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Entities
{
    public class DictionaryType : Auditable
    {
        public int Id { get; set; }
        public string KeyType { get; set; }
        public int ValueType { get; set; }
        public string Description { get; set; }
        public string GroupType { get; set; }
        public int? ParentTypeId { get; set; }
        public int? AtelierId { get; set; }
    }
}
