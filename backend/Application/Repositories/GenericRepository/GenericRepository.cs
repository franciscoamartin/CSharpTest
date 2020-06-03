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

        public void Delete(Guid id)
        {
            var entity = Read(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity Read(Guid id)
        {
            return _dbContext.Set<TEntity>()
              .FirstOrDefault(e => e.Id == id);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}