using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IOrderServiceQuery
    {
        Task<PagedList<OrderRead>> GetAllByQuery(OrderQuery query);
        //Task<PagedList<OrderRead>> GetAll(OrderQuery query);
        Task<OrderDetailRead> GetById(int id);
        Task<OrderDetailRead> GetByCodeOrder_CodeGarment(string codeOrder, string codeGarment);
        Task<IEnumerable<OrderReadMobile>> GetByClientId(int userId);
    }
}
