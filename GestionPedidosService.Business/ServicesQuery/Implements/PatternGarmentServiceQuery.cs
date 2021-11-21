using AutoMapper;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class PatternGarmentServiceQuery : IPatternGarmentServiceQuery
    {
        private readonly IMapper _mapper;
        private readonly IPatternGarmentRepository _patternGarmentRepository;

        public PatternGarmentServiceQuery(IMapper mapper, IPatternGarmentRepository patternGarmentRepository)
        {
            _mapper = mapper;
            _patternGarmentRepository = patternGarmentRepository;
        }

        public async Task<ICollection<PatternGarmentRead>> GetAllByGarmentId(int id)
        {
            var results = await _patternGarmentRepository.GetAllByGarmentId(id);
            return _mapper.Map<ICollection<PatternGarmentRead>>(results);
        }
    }
}
