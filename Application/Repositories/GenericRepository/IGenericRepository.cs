using System;
using System.Collections.Generic;
using BludataTest.Models;

namespace BludataTest.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : BaseEntity
    {
        List<TEntity> GetAll();
        TEntity Read(Guid id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
