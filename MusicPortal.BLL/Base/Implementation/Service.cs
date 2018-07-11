using AutoMapper;
using MusicPortal.BLL.Base.Abstraction;
using MusicPortal.DAL.Interfaces;
using MusicPortal.ViewModels.Base;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Base.Implementation {
    public abstract class Service<TViewModel, TEntity, TKey> : IService<TViewModel, TEntity, TKey>
        where TViewModel : IViewModel
        where TEntity : class 
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork database;

        public Service(IMapper mapper, IUnitOfWork database) {
            this.mapper = mapper;
            this.database = database;
        }

        public IQueryable<TEntity> Query() {
            return database.Context.Set<TEntity>();
        }

        public async Task<TViewModel> GetById(TKey id) {
            var entity = await database.GetRepository<TEntity, TKey>().GetById(id);
            return mapper.Map<TEntity, TViewModel>(entity);
        }

        public async Task<TViewModel> Create(TViewModel item) {
            var entity = mapper.Map<TViewModel, TEntity>(item);
            entity = await database.GetRepository<TEntity, TKey>().Create(entity);
            return mapper.Map<TEntity, TViewModel>(entity);
        }

        public async Task<TViewModel> Update(TViewModel item) {
            var entity = mapper.Map<TViewModel, TEntity>(item);
            entity = await database.GetRepository<TEntity, TKey>().Update(entity);
            return mapper.Map<TEntity, TViewModel>(entity);
        }

        public async Task<TViewModel> Delete(TKey id) {
            var entity = await database.GetRepository<TEntity, TKey>().Remove(id);
            return mapper.Map<TEntity, TViewModel>(entity);
        }
    }
}
