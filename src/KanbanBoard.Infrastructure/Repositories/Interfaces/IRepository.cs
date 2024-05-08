using KanbanBoard.Core.Abstracts;
using System.Linq.Expressions;

namespace KanbanBoard.Infrastructure.Repositories.Interfaces;

public interface IRepository<T> where T : AbstractModel
{
    void Insert(T model);
    void Update(T model);
    void Delete(T model);
    void DeleteById(string id);


    Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default(CancellationToken), Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
    Task<T> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includes);

    Task<T> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includes);

}
