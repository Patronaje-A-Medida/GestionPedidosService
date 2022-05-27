using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class BodyMeasurementsCollectionServiceQuery : IBodyMeasurementsCollectionServiceQuery
    {
        private readonly IBodyMeasurementsCollectionRepository _repository;

        public BodyMeasurementsCollectionServiceQuery(IBodyMeasurementsCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<BodyMeasurements> GetByClientId(int clientId)
        {
            var collection = await _repository.GetByClientId(clientId);
            return collection;
        }
    }
}
