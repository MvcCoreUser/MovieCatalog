using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieCatalog.DataAccess.Entities;

namespace MovieCatalog.DataAccess.Interfaces
{
    public interface IRepository<TEntity> 
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        void Remove(TEntity item);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity item);
        int SaveChanges();

    }
}
