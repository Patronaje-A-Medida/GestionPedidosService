using GestionPedidosService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Interfaces
{
    public interface IGarmentRepository : IRepository<Garment>
    {
        #nullable enable
        Task<IEnumerable<Garment>> GetAllByQuery(int atelierdId, string? filterString, int? category);
    }
}