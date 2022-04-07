using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Interfaces;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Interfaces
{
    public interface IAtelierRepository : IRepository<Atelier>
    {
        Task<Atelier> GetByGarmentId(int garmentId);
    }
}
