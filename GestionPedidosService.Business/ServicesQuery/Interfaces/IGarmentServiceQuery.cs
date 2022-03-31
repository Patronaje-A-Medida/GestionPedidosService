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
        Task<GarmentReadWeb> GetByCodeGarment_AtelierId(string codeGarment, int atelierId);
        Task<GarmentReadMobile> GetById(int id);
    }
}
