using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Repositories.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class AtelierRepository : Repository<Atelier>, IAtelierRepository
    {
        public AtelierRepository(AppDbContext context) : base(context) { }
    }
}
