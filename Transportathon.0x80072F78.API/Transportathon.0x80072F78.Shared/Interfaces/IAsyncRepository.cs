using System.Linq.Expressions;

namespace Transportathon._0x80072F78.Shared.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid entityId);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> GetAllAsync(bool asNoTracking = false);

    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

    Task<int> CountAsync(Expression<Func<T, bool>> predicate, string filter = "", string search = "");

    Task<T> FirstAsync(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string include = "", bool asNoTracking = false);

    Task<TRes> FirstAsync<TRes>(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string include = "", Expression<Func<T, TRes>> select = null);

    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string include = "", bool asNoTracking = false);

    Task<TRes> FirstOrDefaultAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", Expression<Func<T, TRes>> select = null);

    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", bool asNoTracking = false);

    Task<TRes> SingleOrDefaultAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", Expression<Func<T, TRes>> select = null);

    //LastAsync => orderby islemi zorunlu oldugu icin non-optional parametre olarak basa alindi.
    Task<T> LastAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>> predicate = null,
        string include = "", bool asNoTracking = false);

    Task<TRes> LastAsync<TRes>(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>> predicate = null,
        string include = "", Expression<Func<T, TRes>> select = null);

    Task<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", bool asNoTracking = false);

    Task<IReadOnlyList<TRes>> GetAllByFilterAsync<TRes>(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string include = "", Expression<Func<T, TRes>> select = null);

    Task<IReadOnlyList<T>> GetAllByPageAsync(string search = "", int skip = 0, int take = 0,
        string include = "", string filter = "", string sort = "", bool asNoTracking = false);
}