using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlexRule.Models.Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> Get(int id);
        Task Add(TEntity entity);
        Task Update(TEntity dbEntity, TEntity entity);
        Task Delete(TEntity entity);
    }
}
