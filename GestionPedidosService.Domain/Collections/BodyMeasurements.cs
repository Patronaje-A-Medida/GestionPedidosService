using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Collections
{
    public class BodyMeasurements
    {
        [BsonId]
        public ObjectId id { get; set; }
        public int client_id { get; set; }
        public DateTime measurement_date { get; set; }
        public IEnumerable<Measurement> measurements { get; set; }
    }
}
