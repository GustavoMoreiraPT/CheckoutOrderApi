using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);

        void Delete(TEntity entity);

        void Delete(Guid id);

        void Edit(TEntity entity);

        TEntity GetById(Guid id);

        List<TEntity> GetAll();

        void SaveChanges();
    }
}
