using GestionPedidosService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Interfaces
{
    public interface IOrderServiceQuery
    {
        Task<ICollection<OrderRead>> GetAll(OrderQuery query);
        Task<OrderDetailRead> GetById(int id);
    }
}
