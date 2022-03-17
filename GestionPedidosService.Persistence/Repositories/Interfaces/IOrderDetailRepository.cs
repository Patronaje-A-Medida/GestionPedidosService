using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<OrderDetail> GetByCodeOrder_CodeGarment(string codeOrder, string codeGarment);
        Task<OrderDetailRead> GetByCodeOrder_CodeGarment2(string codeOrder, string codeGarment);
    }
}