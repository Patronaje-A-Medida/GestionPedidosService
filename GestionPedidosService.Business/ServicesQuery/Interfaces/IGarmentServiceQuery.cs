using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.Garments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IGarmentServiceQuery
    {
        Task<PagedList<GarmentMinWeb>> GetAllByQueryToWeb(GarmentQuery query);
        Task<IEnumerable<GarmentMinMobile>> GetAllByQueryToMobile(GarmentQuery query);
        Task<GarmentRead> GetByCodeGarment_AtelierId(string codeGarment, int atelierId);
    }
}
