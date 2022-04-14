using GestionPedidosService.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesCommand.Interfaces
{
    public interface IPatternGarmentBaseCollectionServiceCommand
    {
        Task Add(PatternGarmentBase patternGarmentBase);
        Task Add(IEnumerable<PatternGarmentBase> patternGarmentBases);
    }
}
