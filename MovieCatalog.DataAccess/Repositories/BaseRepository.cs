using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.DataAccess.Entities;
using MovieCatalog.DataAccess.Interfaces;

namespace MovieCatalog.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        DbContext _db;
        DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext db)
        {
            this._db = db;
            this._dbSet = _db.Set<TEntity>();
        }

        public void Create(TEntity item)
        => _dbSet.Add(item);

        public TEntity FindById(int id)
        => _dbSet.Find(id);

        public IQueryable<TEntity> GetAll()
        => _dbSet.AsQueryable<TEntity>();
        public IQueryable<TEntity> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        => Include(includeProperties);

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        => GetAll().Where(predicate);
        
        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        => Include(includeProperties).Where(predicate);

        public void Remove(TEntity item)
        => _dbSet.Remove(item);

        public int SaveChanges()
        => _db.SaveChanges();

        public void Update(TEntity item)
        => _dbSet.Update(item);

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            _dbSet.Where(predicate).ToList().ForEach(item =>
            {
                this.Remove(item);
            });
        }
    }
}
