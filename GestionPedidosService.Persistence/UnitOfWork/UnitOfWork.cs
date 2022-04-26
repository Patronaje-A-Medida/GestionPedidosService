using System.Threading.Tasks;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using GestionPedidosService.Persistence.Repositories.Implements;
using GestionPedidosService.Persistence.Repositories.Interfaces;

namespace GestionPedidosService.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IOrderRepository orderRepository { get; private set; }
        public IOrderDetailRepository orderDetailRepository { get; private set; }
        public IGarmentRepository garmentRepository { get; private set; }
        public IDictionaryTypeRepository dictionaryTypeRepository { get; private set; }
        public IAtelierRepository atelierRepository { get; private set; }
        public IFeatureGarmentRepository featureGarmentRepository { get; private set; }

        public IPatternGarmentRepository patternGarmentRepository { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            orderDetailRepository = new OrderDetailRepository(_context);
            orderRepository = new OrderRepository(_context);
            garmentRepository = new GarmentRepository(_context);
            dictionaryTypeRepository = new DictionaryTypeRepository(_context);
            atelierRepository = new AtelierRepository(_context);
            featureGarmentRepository = new FeatureGarmentRepository(_context);
            patternGarmentRepository = new PatternGarmentRepository(_context);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}