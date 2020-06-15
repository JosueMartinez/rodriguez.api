using Rodriguez.Data.Models;
using Rodriguez.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Rodriguez.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly RodriguezModel _db = null;
        public DbSet<T> table = null;

        public Repository(RodriguezModel db)
        {
            _db = db;
            this.table = _db.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IEnumerable<T> Get()
        {
            return table.ToList();
        }

        public T Get(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
