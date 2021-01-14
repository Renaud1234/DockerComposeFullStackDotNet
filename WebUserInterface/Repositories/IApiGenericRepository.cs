using System.Collections.Generic;
using System.Threading.Tasks;
using WebUserInterface.Models;

namespace WebUserInterface.Repositories
{
    public interface IApiGenericRepository<TEntity>
        where TEntity : IBaseEntity
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<TEntity> PostAsync(TEntity entity);

        Task PutAsync(TEntity entity);
    }
}