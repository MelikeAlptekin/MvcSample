using Microsoft.EntityFrameworkCore;
using MvcLab4.Entities;
using MvcLab4.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLab4.Repository
{
  
    public abstract class EFCoreRepository<T> : IRepository<T> where T:class
    {

        protected ApplicationDbContext _dbContext;
    
        private DbSet<T> _dbSet;

        public EFCoreRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual void Create(T item)
        {
            _dbSet.Add(item);
    
        }

    

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

     
        public virtual int Save()
        {
            return _dbContext.SaveChanges();
        }


        public virtual T Find(int id)
        {
            return _dbSet.Find(id);
        }

     
        public virtual List<T> List()
        {
            return _dbSet.ToList();
        }

        public virtual void Update(T item)
        {
            _dbContext.Update(item);
        }

       
    }
}
