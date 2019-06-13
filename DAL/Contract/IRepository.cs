using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
    public interface IRepository<T>
    {
        T add(T entidad);
        void Delete(int id);
        void Update(T entidad);
        int Count(Expression<Func<T, bool>> where);
        T FindById(int id);
        IEnumerable<T> FindBy(QueryParameter<T> parametrosDeQuery);
        IEnumerable<T> FindWhere(Expression<Func<T, bool>> LamdaExpression);
        IEnumerable<T> Getall();
        T FindFirstWhere(Expression<Func<T, bool>> LamdaExpression);

        T AddOrUpdate(T entidad);

        void InsertBulk(IEnumerable<T> entities, string parentTableName);

        void InsertBulk(IEnumerable<T> entities, string parentTableName, int batchSize);

        void DeleteBulk(IEnumerable<T> entities, string tableName);
    }
}
