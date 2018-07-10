using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class {
        protected readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationContext context) {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query() {
            return _dbSet;
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<TEntity> GetById(TKey id) {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity item) {
            TEntity entity = (await _dbSet.AddAsync(item)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity item) {
            TEntity entity = _dbSet.Update(item).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Remove(TKey id) {
            TEntity entity = _dbSet.Remove(_dbSet.Find(id)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<TEntity> GetRange(int startIndex, int numberOfItems) {
            return _dbSet.Skip(startIndex).Take(numberOfItems);
        }

        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> items) {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> items) {
            _dbSet.UpdateRange(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<IEnumerable<TEntity>> RemoveRange(IEnumerable<TKey> ids) {
            var entitiesToRemove = ids.Select(id => Task.Run(() => _dbSet.FindAsync(id)).Result);
            _dbSet.RemoveRange(entitiesToRemove);
            await _context.SaveChangesAsync();
            return entitiesToRemove;
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties) {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties) {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties) {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
