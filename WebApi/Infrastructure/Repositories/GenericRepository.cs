using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApi.Abstractions;
using WebApi.Exceptions;

namespace WebApi.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class,IEntity
    {
        private readonly EntityFrameworkCoreDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(EntityFrameworkCoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            await using var tran = await _context.Database.BeginTransactionAsync();
            try
            {
                _dbSet.Add(item);
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return item;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new DataOperationException("Ошибка при создании записи", ex);
            }
        }
        public async Task<TEntity> GetByIdAsync(long id)
        {
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                return entity;
            }
            catch (Exception ex)
            {
                throw new DataOperationException("Ошибка при получении записи", ex);
            }
        }
        public async Task<bool> IsExistByIdAsync(long id)
        {
            try
            {
                var isExist = await _dbSet.AnyAsync(x => x.Id == id);
                return isExist;
            }
            catch (Exception ex)
            {
                throw new DataOperationException("Ошибка при получении записи", ex);
            }
        }
    }
}
