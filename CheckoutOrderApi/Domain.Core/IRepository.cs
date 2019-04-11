using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(Guid id);

        Task EditAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        Task SaveChangesAsync();
    }
}
