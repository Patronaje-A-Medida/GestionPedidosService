using System;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}