using DietApp.DAL.Abstract;
using DietApp.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.DAL.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public GenericRepository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public T? Get(int id)
        {
            return DbSet.Where(x => x.ID == id).FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public IQueryable<T> GetQueryable()
        {
           return DbSet.AsQueryable();
        }

        public void Update(T entity)
        {
            var oldData = DbSet.Find(entity.ID);
            oldData = entity;
            SaveChanges();
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
