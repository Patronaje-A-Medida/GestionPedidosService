using GestionPedidosService.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Interfaces
{
    public interface IBodyMeasurementsCollectionRepository
    {
        Task<BodyMeasurements> GetByClientId(int clientId);
    }
}
