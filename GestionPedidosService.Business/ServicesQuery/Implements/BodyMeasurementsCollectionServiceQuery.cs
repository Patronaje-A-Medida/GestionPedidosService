using AutoMapper;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Domain.Models.Measurements;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class BodyMeasurementsCollectionServiceQuery : IBodyMeasurementsCollectionServiceQuery
    {
        private readonly IBodyMeasurementsCollectionRepository _repository;
        private readonly IMapper _mapper;

        public BodyMeasurementsCollectionServiceQuery(IBodyMeasurementsCollectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BodyMeasurementsMin>> GetAllByClientId(int clientId)
        {
            var collections = await _repository.GetAllByClientId(clientId);
            var dto = _mapper.Map<IEnumerable<BodyMeasurementsMin>>(collections);
            return dto;
        }

        public async Task<BodyMeasurements> GetByClientId(int clientId)
        {
            var collection = await _repository.GetByClientId(clientId);
            return collection;
        }
    }
}
