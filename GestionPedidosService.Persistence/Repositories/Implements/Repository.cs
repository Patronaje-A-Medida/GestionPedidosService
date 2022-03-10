using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<bool> Add(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return true;
        }

        public virtual async Task<bool> Delete(int id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                return true;
            }
            else
                return false;
        }

        public virtual bool Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return true;
        }

        public bool Delete(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperties);
            }

            if (orderBy != null)
            {
                var result = await orderBy(query).ToListAsync();
                return result;
            }

            var finalResult = await query.ToListAsync();
            return finalResult;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return result;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            var result = await _context.Set<TEntity>().FindAsync(id);
            return result;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public virtual bool Update(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return true;
        }

    }
}