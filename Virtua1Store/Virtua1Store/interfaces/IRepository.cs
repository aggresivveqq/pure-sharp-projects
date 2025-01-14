using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtStore.models;

namespace VirtStore.interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
        void Save(List<T> entities);
    }
}