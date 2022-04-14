using GestionPedidosService.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Interfaces
{
    public interface IPatternGarmentBaseCollectionRepository
    {
        Task Add(PatternGarmentBase patternGarmentBase);
        Task Add(IEnumerable<PatternGarmentBase> patternGarmentBases);
        Task<IEnumerable<PatternGarmentBase>> GetAll();
    }
}
