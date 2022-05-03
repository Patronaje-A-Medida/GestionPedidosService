using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Collections
{
    public class PatternGarmentBase
    {
        [BsonId]
        public ObjectId id { get; set; }
        public int garment_id { get; set; }
        public string name_pattern { get; set; }

        public string image_pattern { get; set; }
    }
}
