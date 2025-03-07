using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLab4.Repository
{
    public interface IRepository<T> 
    {
        void Create(T item);
        void Delete(int id);
        void Update(T item);
        T Find(int id);
        List<T> List();
        int Save();
    }
}
