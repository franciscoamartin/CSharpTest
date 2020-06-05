using System;
using System.Collections.Generic;
using System.Linq;
using BludataTest.Models;

namespace BludataTest.Repositories
{
    public class GenericRepository<TEntity>
        : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public readonly BludataTestDbContext _dbContext;

        public GenericRepository(BludataTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            entity.Deactivate();
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().Where(e => e.Active).ToList();
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbContext.Set<TEntity>()
              .FirstOrDefault(e => e.Id == id && e.Active);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}