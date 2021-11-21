using GestionPedidosService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Interfaces
{
    public interface IPatternGarmentRepository : IRepository<PatternGarment>
    {
        Task<IEnumerable<PatternGarment>> GetAllByGarmentId(int id);
    }
}