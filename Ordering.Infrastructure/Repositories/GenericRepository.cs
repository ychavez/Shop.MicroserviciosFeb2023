using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : EntityBase
    {
        private readonly OrderContext orderContext;

        public GenericRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await orderContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
            => await orderContext.Set<T>().Where(predicate).ToListAsync();


        public async Task<IReadOnlyList<T>> GetAsync(int offset, int limit,
            Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            params string[] includeStrings)
        {

            IQueryable<T> query = orderContext.Set<T>();

            query = query.Skip(offset).Take(limit);

            query = includeStrings.Aggregate(query, (current, itemInclude) => current.Include(itemInclude));

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
            => await orderContext.Set<T>().SingleAsync(x => x.Id == Id);

        public async Task<T> AddAsync(T entity)
        {
            await orderContext.Set<T>().AddAsync(entity);
            await orderContext.SaveChangesAsync();

            return entity;

        }

        public async Task UpdateAsync(T entity)
        {
            orderContext.Entry(entity).State = EntityState.Modified;
            await orderContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity) 
        {
            orderContext.Set<T>().Remove(entity);
            await orderContext.SaveChangesAsync();     
        }
    }
}
