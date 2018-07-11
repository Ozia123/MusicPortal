using MusicPortal.ViewModels.Base;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Base.Abstraction {
    public interface IService<TViewModel, TEntity, TKey>
        where TViewModel : IViewModel
        where TEntity : class
    {
        IQueryable<TEntity> Query();

        Task<TViewModel> GetById(TKey id);

        Task<TViewModel> Create(TViewModel item);

        Task<TViewModel> Update(TViewModel item);

        Task<TViewModel> Delete(TKey id);
    }
}