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
    public class PatternGarmentBaseCollectionRepository : IPatternGarmentBaseCollectionRepository
    {
        private readonly MongoDbContext _context;

        public PatternGarmentBaseCollectionRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task Add(PatternGarmentBase patternGarmentBase)
        {
            await _context.patternGarmentBaseCollection.InsertOneAsync(patternGarmentBase);
        }

        public async Task Add(IEnumerable<PatternGarmentBase> patternGarmentBases)
        {
            await _context.patternGarmentBaseCollection.InsertManyAsync(patternGarmentBases);
        }

        public async Task<IEnumerable<PatternGarmentBase>> GetAll()
        {
            var collections = await _context.patternGarmentBaseCollection
                .FindAsync(new BsonDocument())
                .Result
                .ToListAsync();

            return collections;
        }
    }
}
