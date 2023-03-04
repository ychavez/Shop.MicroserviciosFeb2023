using Ordering.Domain.Common;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();


        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);


        Task<IReadOnlyList<T>> GetAsync(int offset, int limit,
             Expression<Func<T, bool>>? predicate,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
             params string[] includeStrings);


        Task<T> GetByIdAsync(int Id);


        Task<T> AddAsync(T entity);


        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

    }
}
