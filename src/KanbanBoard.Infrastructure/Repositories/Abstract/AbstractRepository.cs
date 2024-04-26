using KanbanBoard.Core.Abstracts;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace KanbanBoard.Infrastructure.Repositories.Abstract;

public abstract class AbstractRepository<T> : IRepository<T>
    where T : AbstractModel
{
    protected readonly EfDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected AbstractRepository(EfDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    private int GetNextOrder(string statusId)
    {
        // Veritabanından kolondaki son order'ı alır
        var lastOrder = _dbContext.Issues.Where(t => t.StatusId == statusId).Max(x => x.Order);
        return lastOrder + 1; // Sonraki order'ı hesaplar
    }

    public virtual void Insers(T model) => _dbSet.Add(model);

    public virtual void Update(T model) => _dbContext.Entry<T>(model).State = EntityState.Modified;

    public virtual void Delete(T model) => _dbSet.Remove(model);

    public void DeleteById(string id)
    {
        T? entity = _dbSet.SingleOrDefault(x => x.Id == id);
        _dbSet.Remove(entity!);
    }


    public virtual async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        query = filter is not null ? query.Where(filter) : query;

        if (includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(filter).AsNoTracking();

        if (includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T> FindByIdAsync(string id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(x => x.Id == id).AsNoTracking();

        if (includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }


}
