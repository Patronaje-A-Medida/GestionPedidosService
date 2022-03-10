using GestionPedidosService.Domain.Models;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IGarmentServiceQuery
    {
        Task<PagedList<GarmentMin>> GetAllByQuery(GarmentQuery query);
    }
}
