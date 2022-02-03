using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Interfaces.Repositories
{

    public interface IBaseRepository<TEntity> where TEntity : class
    {     
        Task<TEntity> Add(TEntity entity);       
        Task Delete(int id);       
        Task<TEntity> Modify(TEntity entity);       
        Task<IEnumerable<TEntity>> GetAll();       
        Task<TEntity> GetById(int id);
    }
}
