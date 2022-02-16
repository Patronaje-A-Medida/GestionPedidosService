﻿using System.Threading.Tasks;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using GestionPedidosService.Persistence.Repositories.Implements;

namespace GestionPedidosService.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IOrderRepository orderRepository { get; private set; }
        public IOrderDetailRepository orderDetailRepository { get; private set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            orderDetailRepository = new OrderDetailRepository(_context);
            orderRepository = new OrderRepository(_context);
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