using MvcLab4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLab4.Repository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        List<Category> FilterByCategoryName(string categoryName);
    }
}
