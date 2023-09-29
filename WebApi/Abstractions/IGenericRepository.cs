using System.Threading.Tasks;

namespace WebApi.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> GetByIdAsync(long id);
        /// <summary>
        /// содержится ли объект с таким же id в бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistByIdAsync(long id);
    }
}
