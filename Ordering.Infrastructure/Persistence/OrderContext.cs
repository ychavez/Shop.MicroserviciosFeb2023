using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {

        public OrderContext(DbContextOptions<OrderContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Order> Orders { get; set; }
        private Guid TenantId { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "User";
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "User";
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            foreach (var item in ChangeTracker.Entries().Where(e=> e.State == EntityState.Added 
            && e.Entity is IMultiTenant))
            {
                var entity = item.Entity as IMultiTenant;
                entity!.TenantId = Guid.Empty;
            }


            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entity.ClrType;

                if (!typeof(IMultiTenant).IsAssignableFrom(entityType)) continue;

                var method = typeof(OrderContext).GetMethod(nameof(MultiTenantExpression),
                    BindingFlags.NonPublic | BindingFlags.Static)?.MakeGenericMethod(entityType);

                var filter = method?.Invoke(null, new object[] { this });

                entity.SetQueryFilter((LambdaExpression)filter!);

                entity.AddIndex(entity.FindProperty(nameof(IMultiTenant.TenantId))!);

            }
        }

        private static LambdaExpression MultiTenantExpression<T>(OrderContext context)
            where T : EntityBase, IMultiTenant
        {
            Expression<Func<T, bool>> tenantFilter = x => x.TenantId == context.TenantId;
            return tenantFilter;
        }
    }
}
