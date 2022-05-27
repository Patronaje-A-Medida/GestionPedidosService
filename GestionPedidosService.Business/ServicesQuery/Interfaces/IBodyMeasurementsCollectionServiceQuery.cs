using GestionPedidosService.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IBodyMeasurementsCollectionServiceQuery
    {
        Task<BodyMeasurements> GetByClientId(int clientId);
    }
}
