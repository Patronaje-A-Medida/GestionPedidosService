using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class BodyMeasurementsCollectionRepository : IBodyMeasurementsCollectionRepository
    {
        private readonly MongoDbContext _context;

        public BodyMeasurementsCollectionRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BodyMeasurements>> GetAllByClientId(int clientId)
        {
            var collections = await _context.bodyMeasurementsCollection
                .FindAsync(Builders<BodyMeasurements>.Filter.Eq("client_id", clientId))
                .Result
                .ToListAsync();

            collections = collections.OrderByDescending(x => x.measurement_date).ToList();
            return collections;
        }

        public async Task<BodyMeasurements> GetByClientId(int clientId)
        {
            var collections = await _context.bodyMeasurementsCollection
                .FindAsync(Builders<BodyMeasurements>.Filter.Eq("client_id", clientId))
                .Result
                .ToListAsync();

            //.OrderByDescending(x => x.measurement_date);
            var collection = collections.OrderByDescending(x => x.measurement_date).First();

            return collection;
        }
    }
}
