using GestionPedidosService.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Interfaces
{
    public interface IBodyMeasurementsCollectionRepository
    {
        Task<IEnumerable<BodyMeasurements>> GetAllByClientId(int clientId);
        Task<BodyMeasurements> GetByClientId(int clientId);
    }
}
