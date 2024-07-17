using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabatHost.DAL.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? GetById(int id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
