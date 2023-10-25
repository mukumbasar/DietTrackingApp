using DietApp.BL.Services;
using DietApp.DAL.Concrete;
using DietApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Managers
{
    public abstract class BaseManager<T> : IService<T> where T : class, IEntity
    {
        protected GenericRepository<T> _genericRepository;

        public BaseManager(GenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Add(T entity)
        {
            _genericRepository.Add(entity);
        }

        public void Delete(T entity)
        {
            _genericRepository.Delete(entity);
        }

        public List<T> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public T? GetById(int id)
        {
            return _genericRepository.Get(id);
        }

        public void Update(T entity)
        {
             _genericRepository.Update(entity);
        }
    }
}
