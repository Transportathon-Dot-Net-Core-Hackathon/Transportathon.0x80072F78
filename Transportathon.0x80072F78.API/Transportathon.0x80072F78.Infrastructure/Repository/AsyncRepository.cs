using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class AsyncRepository<T> : IAsyncRepository<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly IFilter _filter;


    public AsyncRepository(AppDbContext dbContext, IFilter filter)
    {
        _dbContext = dbContext;
        _filter = filter;
    }


    public async Task<T> GetByIdAsync(Guid entityId)
    {
        var keyValues = new object[] { entityId };
        return await _dbContext.Set<T>().FindAsync(keyValues);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(bool asNoTracking = false)
    {
        return asNoTracking ? await _dbContext.Set<T>().AsNoTracking().ToListAsync() :
                              await _dbContext.Set<T>().ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        if (predicate != null)
            return await _dbContext.Set<T>().CountAsync(predicate);

        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, string filter = "", string search = "")
    {
        var entityType = _dbContext.Model.FindEntityType(typeof(T));
        IQueryable<T> query = _dbContext.Set<T>();

        query = _filter.PrepareFilterQuery(entityType, query, filter, search);
        if (predicate != null)
            return await query.CountAsync(predicate);

        return await query.CountAsync();
    }

    public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", bool asNoTracking = false)
    {
        IQueryable<T> query = GetAllQueryable(predicate, orderBy, include);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.FirstAsync();
    }

    public async Task<TRes> FirstAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", Expression<Func<T, TRes>> select = null)
    {
        IQueryable<TRes> query = GetAllQueryable(predicate, orderBy, include, select);
        return await query.FirstAsync();
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", bool asNoTracking = false)
    {
        IQueryable<T> query = GetAllQueryable(predicate, orderBy, include);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync();
    }

    public async Task<TRes> FirstOrDefaultAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", Expression<Func<T, TRes>> select = null)
    {
        IQueryable<TRes> query = GetAllQueryable(predicate, orderBy, include, select);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", bool asNoTracking = false)
    {
        IQueryable<T> query = GetAllQueryable(predicate, orderBy, include);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.SingleOrDefaultAsync();
    }

    public async Task<TRes> SingleOrDefaultAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", Expression<Func<T, TRes>> select = null)
    {
        IQueryable<TRes> query = GetAllQueryable(predicate, orderBy, include, select);
        return await query.SingleOrDefaultAsync();
    }

    public async Task<T> LastAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>> predicate = null,
        string include = "", bool asNoTracking = false)
    {
        IQueryable<T> query = GetAllQueryable(predicate, orderBy, include);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.LastAsync();
    }

    public async Task<TRes> LastAsync<TRes>(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>> predicate = null,
        string include = "", Expression<Func<T, TRes>> select = null)
    {
        IQueryable<TRes> query = GetAllQueryable(predicate, orderBy, include, select);
        return await query.LastAsync();
    }

    /// <summary>
    ///     Verilen filtre ve koşullara göre oluşturulan sorguyu çalıştırarak liste döner.
    /// </summary>
    /// <param name="filter">Where() içine yazılacak ifadedir.</param>
    /// <param name="orderBy">OrderBy() içine yazılacak ifadedir.</param>
    /// <param name="include">Include için yazılacak ifadedir. Aralarına virgül ',' konularak birden çok eklenebilir.</param>
    /// <returns></returns>    
    public async Task<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", bool asNoTracking = false)
    {
        var query = GetAllQueryable(predicate, orderBy, include);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<TRes>> GetAllByFilterAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
       string include = "", Expression<Func<T, TRes>> select = null)
    {
        var query = GetAllQueryable(predicate, orderBy, include, select);
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllByPageAsync(string search = "", int skip = 0, int take = 0,
        string include = "", string filter = "", string sort = "",
        bool asNoTracking = false)
    {
        IQueryable<T> query = GetAllByPageQuery(search, skip, take, include, filter, sort);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync();
    }


    #region Private methods
    private IQueryable<T> GetAllByPageQuery(string search = "", int skip = 0, int take = 0,
        string include = "", string filter = "", string sort = "")
    {
        IQueryable<T> query = _dbContext.Set<T>();
        var entityType = _dbContext.Model.FindEntityType(typeof(T));

        query = _filter.PrepareFilterQuery(entityType, query, filter, search, sort);

        if (string.IsNullOrWhiteSpace(include) == false)
        {
            foreach (var item in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
        }

        //If source contains fewer than count elements, an empty IEnumerable<T> is returned.
        //If count is less than or equal to zero, all elements of source are yielded.
        if (skip > 0)
            query = query.Skip(skip);

        //If count is less than or equal to zero, source is not enumerated and an empty IEnumerable<T> is returned.
        if (take > 0)
            query = query.Take(take);

        return query;
    }

    /// <summary>
    ///     IQueryable tipi geri dönüş değeri olarak public olarak açılmamalıdır.
    ///     IQueryable değişken olarak alan ve IEnumerable tipi geri dönmelidir.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    private IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var item in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(item);

        if (orderBy != null)
            query = orderBy(query);

        return query;
    }

    private IQueryable<TRes> GetAllQueryable<TRes>(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null,
        Expression<Func<T, TRes>> select = null)
    {
        IQueryable<T> query = GetAllQueryable(filter, orderBy, includeProperties);

        return query.Select(select);
    }
    #endregion
}