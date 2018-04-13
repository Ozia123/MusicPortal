using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    // TODO: TKey тут не нужен, я думаю. PK обычно одного типа во всех таблицах
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

        public Task<TEntity> GetById(TKey id) {
            return _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity item) {
            TEntity entity = _dbSet.Add(item).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(TKey id) {
            TEntity entity = _dbSet.Remove(_dbSet.Find(id)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity item) {
            TEntity entity = _dbSet.Update(item).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> items) {
            _dbSet.UpdateRange(items);
            await _context.SaveChangesAsync();
            return items;
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
