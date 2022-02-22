using GestionPedidosService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<OrderDetail> GetByCodeOrder_CodeGarment(string codeOrder, string codeGarment);
    }
}