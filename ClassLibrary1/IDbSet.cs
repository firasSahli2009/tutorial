using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IDbSet<T> where T: Entity
    {
        List<Entity> Elements { get; }
        T Add(T entity);
        void Delete(int id);
        T Update(T entity);
        T FindById(int Id);
    }
}