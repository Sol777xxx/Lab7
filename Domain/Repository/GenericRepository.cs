using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly HotelContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(HotelContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity? GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
            SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
            SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public virtual void DeleteByID(int id)
        {
            var entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                SaveChanges();
            }
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
