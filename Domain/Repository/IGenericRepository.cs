using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    internal interface IGenericRepository<TEntity> where TEntity : class
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

