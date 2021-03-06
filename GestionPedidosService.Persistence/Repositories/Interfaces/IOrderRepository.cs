using GestionPedidosService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllByQuery(int atelierId, int? orderStatus, string filterString);
        Task<IEnumerable<Order>> GetByClientId(int userId);
        Task<string> GetLastCodeOrderByAtelier();
    }
}