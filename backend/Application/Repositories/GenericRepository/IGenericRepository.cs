using BludataTest.Models;
using System;
using System.Collections.Generic;

namespace BludataTest.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        List<TEntity> GetAll();
        TEntity GetById(Guid id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
