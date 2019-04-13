using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Model;

namespace Data.Repository
{
    public class GenericInMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IList<TEntity> entities = new List<TEntity>();

        public void Create(TEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            this.entities.Remove(entity);
        }

        public void Delete(Guid id)
        {
            var entity = entities.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                this.entities.Remove(entity);
            }
        }

        public void Edit(TEntity entity)
        {
            var existingEntity = entities.FirstOrDefault(x => x.Id == entity.Id);

            if (existingEntity != null)
            {
                existingEntity = entity;
            }
        }

        public List<TEntity> GetAll()
        {
            return this.entities.ToList();
        }

        public TEntity GetById(Guid id)
        {
            return entities.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
        }
    }
}
