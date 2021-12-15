using System.Linq.Expressions;

namespace DLL.Interfaces {
    public interface IRepository<TEntity> {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task                               InsertAsync(TEntity data);
    }
}