using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodriguez.Repo.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        IEnumerable<TEntity> Get();
        TEntity Get(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Save();
    }
}
