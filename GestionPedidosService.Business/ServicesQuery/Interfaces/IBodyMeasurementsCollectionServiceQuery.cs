using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Domain.Models.Measurements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IBodyMeasurementsCollectionServiceQuery
    {
        Task<IEnumerable<BodyMeasurementsMin>> GetAllByClientId(int clientId);
        Task<BodyMeasurements> GetByClientId(int clientId);
    }
}
