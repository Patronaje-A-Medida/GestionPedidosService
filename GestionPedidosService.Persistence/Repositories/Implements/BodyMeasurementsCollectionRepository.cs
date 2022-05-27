using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class BodyMeasurementsCollectionRepository : IBodyMeasurementsCollectionRepository
    {
        private readonly MongoDbContext _context;

        public BodyMeasurementsCollectionRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<BodyMeasurements> GetByClientId(int clientId)
        {
            var collection = await _context.bodyMeasurementsCollection
                .FindAsync(Builders<BodyMeasurements>.Filter.Eq("client_id", clientId))
                .Result
                .FirstOrDefaultAsync();

            return collection;
        }
    }
}
