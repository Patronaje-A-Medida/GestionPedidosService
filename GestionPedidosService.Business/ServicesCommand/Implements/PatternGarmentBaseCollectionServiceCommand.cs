using GestionPedidosService.Business.ServicesCommand.Interfaces;
using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesCommand.Implements
{
    public class PatternGarmentBaseCollectionServiceCommand : IPatternGarmentBaseCollectionServiceCommand
    {
        private readonly IPatternGarmentBaseCollectionRepository _repository;

        public PatternGarmentBaseCollectionServiceCommand(IPatternGarmentBaseCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(PatternGarmentBase patternGarmentBase)
        {
            await _repository.Add(patternGarmentBase);
        }

        public async Task Add(IEnumerable<PatternGarmentBase> patternGarmentBases)
        {
            await _repository.Add(patternGarmentBases);
        }
    }
}
