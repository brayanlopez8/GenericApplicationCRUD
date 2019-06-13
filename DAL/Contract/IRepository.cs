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
        T add(T entity);

        Task<T> addAsyc(T entity);

        void Delete(int id);

        Task DeleteAsync(int id);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        int Count(Expression<Func<T, bool>> where);

        Task<int> CountAsync(Expression<Func<T, bool>> where);

        T FindById(int id);

        Task<T> FindByIdAsync(int id);

        IEnumerable<T> FindBy(QueryParameter<T> QueryParameter);

        IEnumerable<T> FindWhere(Expression<Func<T, bool>> LamdaExpression);

        Task<IEnumerable<T>> FindWhereAsync(Expression<Func<T, bool>> LamdaExpression);

        IEnumerable<T> Getall();

        Task<IEnumerable<T>> GetallAsyc();

        T FindFirstWhere(Expression<Func<T, bool>> LamdaExpression);

        Task<T> FindFirstWhereAsync(Expression<Func<T, bool>> LamdaExpression);

        T AddOrUpdate(T entity);

        void InsertBulk(IEnumerable<T> entities, string parentTableName);

        void InsertBulk(IEnumerable<T> entities, string parentTableName, int batchSize);

        void DeleteBulk(IEnumerable<T> entities, string tableName);
    }
}
