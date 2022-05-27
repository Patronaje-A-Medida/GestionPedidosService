using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GestionPedidosService.Domain.Collections
{
    public class PatternGarmentBase
    {
        [BsonId]
        public ObjectId id { get; set; }
        public int garment_id { get; set; }
        public string image_pattern { get; set; }
    }
}
