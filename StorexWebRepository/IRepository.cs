using StorexWebCore.Enities;
using System.Linq.Expressions;

namespace StorexWebRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        bool Insert(T entity);
        bool RangeInsert(List<T> entity);
        T InsertAndReturn(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IQueryable<T> Table { get; }

        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
        IQueryable<T> Include(string path);

        IQueryable<T> Include<TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> path);
        IQueryable<T> Include(IQueryable<T> query, string path);
        // Test Commit
    }
}
