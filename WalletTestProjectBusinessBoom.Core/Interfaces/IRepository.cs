using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WalletTestProjectBusinessBoom.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        TEntity GetByIdAsync(int id);   
        IQueryable<TEntity> GetAll();
        void Update(TEntity entity);
        void DeleteById(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetRange(Expression<Func<TEntity, bool>> expression);
    }
}
