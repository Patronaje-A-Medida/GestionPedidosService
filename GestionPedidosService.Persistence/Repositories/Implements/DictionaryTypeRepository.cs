using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class DictionaryTypeRepository : Repository<DictionaryType>, IDictionaryTypeRepository
    {
        public DictionaryTypeRepository(AppDbContext context) : base(context) { }
    }
}
