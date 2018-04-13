using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {

    // TODO: Не хватает Generic-реализации этого класса
    public interface IService<T, TK> {
        Task<T> GetById(TK id);
        Task<T> Create(T item);
        Task<T> Update(T item);
        Task<T> Delete(TK id);
    }
}