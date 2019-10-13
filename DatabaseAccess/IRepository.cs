using System.Collections.Generic;

namespace DatabaseAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        void Add(T entity);

        void Save();
    }
}
