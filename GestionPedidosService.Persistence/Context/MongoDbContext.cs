using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Persistence.Managers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Persistence.Context
{
    public class MongoDbContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IConfigurationManager _config;

        public MongoDbContext(IConfigurationManager config)
        {
            _config = config;
            _client = new MongoClient(_config.MongoDbConnectionString);
            _db = _client.GetDatabase(_config.MongoDbName);
            patternGarmentBaseCollection = _db.GetCollection<PatternGarmentBase>("PatternGarmentBase");
            bodyMeasurementsCollection = _db.GetCollection<BodyMeasurements>("BodyMeasurements");
        }

        public IMongoCollection<PatternGarmentBase> patternGarmentBaseCollection;
        public IMongoCollection<BodyMeasurements> bodyMeasurementsCollection;
    }
}
