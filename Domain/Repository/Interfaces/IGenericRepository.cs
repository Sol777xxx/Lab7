namespace Domain.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity? GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteByID(int id);
        void SaveChanges();
    }
}

