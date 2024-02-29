using App.Repository.Context;
using App.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;

        public EFRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetLazyLoading(bool state)
        {
            _dbContext.ChangeTracker.LazyLoadingEnabled = state;
        }

        public async Task<TEntity> AddAndSaveAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>()?.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>()?.Add(entity);
        }
        public TEntity AddAndSave(TEntity entity)
        {
            _dbContext.Set<TEntity>()?.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }
        public async Task<TEntity> UpdateAndSaveAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public TEntity UpdateAndSave(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public IList<TEntity>? Search(Expression<Func<TEntity, bool>> predicado)
        {
            return _dbContext.Set<TEntity>()?.Where(predicado)?.ToList();
        }
        public IEnumerable<TEntity> SearchNoTracking(Expression<Func<TEntity, bool>> predicado)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().Where(predicado).AsEnumerable();
        }

        public async Task<IList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicado)
        {
            return await _dbContext.Set<TEntity>().Where(predicado).ToListAsync();
        }

        public DbSet<TEntity> DbSet()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity? GetById(int Id)
        {
            return _dbContext.Set<TEntity>().Find(Id);
        }
        public TEntity? GetById(string Guid)
        {
            return _dbContext.Set<TEntity>().Find(Guid);
        }

        public IEnumerable<TEntity>? GetAll()
        {
            return _dbContext.Set<TEntity>()?.AsEnumerable();
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>()?.Remove(entity);
        }

        public void Add(params TEntity[] obj)
        {
            _dbContext.Set<TEntity>()?.AddRange(obj);
        }

        public void Update(TEntity obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.Set<TEntity>()?.Update(obj);
        }
        public void Update(params TEntity[] obj)
        {
            _dbContext.Set<TEntity>()?.UpdateRange(obj);
        }

        public void Delete(params TEntity[] obj)
        {
            _dbContext.Set<TEntity>()?.RemoveRange(obj);
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAfterDelete(List<TEntity> entity, Expression<Func<TEntity, bool>> predicate)
        {
            var objs = _dbContext.Set<TEntity>()?.Where(predicate);
            if (objs != null && objs.Any())
                _dbContext.Set<TEntity>()?.RemoveRange(objs);

            _dbContext.Set<TEntity>()?.AddRange(entity.ToArray());
            await _dbContext.SaveChangesAsync();
        }

        public async Task SearchAndDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var objs = _dbContext.Set<TEntity>()?.Where(predicate);
            if (objs != null)
            {
                this.Delete(objs.ToArray());
                await this.SaveChangesAsync();
            }
        }

        public bool Exists()
        {
            try
            {
                _dbContext.Set<TEntity>().FirstOrDefault();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
