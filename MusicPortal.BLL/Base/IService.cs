using System.Threading.Tasks;

namespace MusicPortal.BLL.Base {
    public interface IService<T, TK> {
        Task<T> GetById(TK id);

        Task<T> Create(T item);

        Task<T> Update(T item);

        Task<T> Delete(TK id);
    }
}