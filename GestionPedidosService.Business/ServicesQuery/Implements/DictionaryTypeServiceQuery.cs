using AutoMapper;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Persistence.UnitOfWork;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class DictionaryTypeServiceQuery : IDictionaryTypeServiceQuery
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public DictionaryTypeServiceQuery(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<ConfigurationTypeRead> GetAll(int atelierId)
        {
            var dict = await _uof.dictionaryTypeRepository.Get(
                filter: d => 
                    (d.AtelierId == 0 || d.AtelierId == atelierId) &&
                    d.Status
            );

            var dto = _mapper.Map<ConfigurationTypeRead>(dict);
            return dto;
        }
    }
}
