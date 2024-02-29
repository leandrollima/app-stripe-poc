using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void SetLazyLoading(bool state);
        Task<TEntity> AddAndSaveAsync(TEntity entity);
        TEntity AddAndSave(TEntity entity);
        Task<TEntity> UpdateAndSaveAsync(TEntity entity);
        TEntity UpdateAndSave(TEntity entity);

        void Add(TEntity entity);
        void Add(params TEntity[] obj);
        void Update(TEntity obj);
        void Update(params TEntity[] obj);
        void Delete(params TEntity[] obj);
        void Delete(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">Array with objects to savev</param>
        /// <param name="predicate">Condition to delete objects before saving</param>
        /// <returns></returns>
        Task SaveAfterDelete(List<TEntity> entity, Expression<Func<TEntity, bool>> predicate);
        Task SearchAndDelete(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity>? GetAll();
        TEntity? GetById(int Id);
        TEntity? GetById(string Guid);
        IList<TEntity>? Search(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SearchNoTracking(Expression<Func<TEntity, bool>> predicate);
        DbSet<TEntity> DbSet();

        void SaveChanges();
        Task<int> SaveChangesAsync();

        bool Exists();
    }
}