using GestionPedidosService.Persistence.Interfaces;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository orderRepository { get; }
        IOrderDetailRepository orderDetailRepository { get; }
        IGarmentRepository garmentRepository { get; }
        IDictionaryTypeRepository dictionaryTypeRepository { get; }
        IAtelierRepository atelierRepository { get; }
        IFeatureGarmentRepository featureGarmentRepository { get; }
        IPatternGarmentRepository patternGarmentRepository { get; }

        Task SaveChangesAsync();
    }
}