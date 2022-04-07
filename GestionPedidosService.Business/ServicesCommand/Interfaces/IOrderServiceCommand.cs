using GestionPedidosService.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesCommand.Interfaces
{
    public interface IOrderServiceCommand
    {
        Task<bool> Create(OrderCreate orderCreate);
    }
}
