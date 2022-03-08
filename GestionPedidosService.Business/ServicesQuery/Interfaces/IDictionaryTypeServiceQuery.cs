using GestionPedidosService.Domain.Models;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IDictionaryTypeServiceQuery
    {
        Task<ConfigurationTypeRead> GetAll(int atelierId);
    }
}
