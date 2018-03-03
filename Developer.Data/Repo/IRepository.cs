using Developer.Entity;
using System.Collections.Generic;

namespace Developer.Data.Repo
{
    interface IRepository<T>  where T : BaseEntity
    {
        void Insert(T entity);
        void Update(T entity);
        T GetbyId(long Id);
        void Delete(T entity);
        

    }
}
