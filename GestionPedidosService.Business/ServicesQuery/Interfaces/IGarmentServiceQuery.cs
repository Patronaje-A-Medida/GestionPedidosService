using GestionPedidosService.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IGarmentServiceQuery
    {
        Task<IEnumerable<GarmentMin>> GetAllByQuery(GarmentQuery query);
    }
}
