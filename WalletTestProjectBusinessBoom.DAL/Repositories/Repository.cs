using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.Core.Interfaces;

namespace WalletTestProjectBusinessBoom.DAL.Repositories
{
    public class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : class
    {
        public virtual async Task AddAsync(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);

        public virtual IQueryable<TEntity> GetAll() => context.Set<TEntity>();

        public virtual async Task<TEntity?> GetByIdAsync(Guid id) => await context.Set<TEntity>().FindAsync(id);

        public virtual IQueryable<TEntity> GetRange(Expression<Func<TEntity, bool>> expression) => context.Set<TEntity>().Where(expression);

        public virtual async Task RemoveByIdAsync(Guid id)
        {
            TEntity? entity = await GetByIdAsync(id);
            if (entity != null)
                context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities) => context.Set<TEntity>().RemoveRange(entities);

        public virtual void Update(TEntity entity) => context.Set<TEntity>().Update(entity);

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
    }
}
