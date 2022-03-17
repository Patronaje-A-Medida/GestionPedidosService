using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.Garments;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IGarmentServiceQuery
    {
        Task<PagedList<GarmentMin>> GetAllByQuery(GarmentQuery query);
    }
}
