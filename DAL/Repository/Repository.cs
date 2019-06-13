using DAL.Contract;
using ENT.ParentEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : GenericEntity, new()
    {
        internal MyDBContext context;
        internal DbSet<T> dbSet;
        public Repository(MyDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();

        }
        public void Update(T entidad)
        {
            dbSet.Attach(entidad);
            context.Entry(entidad).State = EntityState.Modified;
            context.SaveChanges();
        }

        public T add(T entidad)
        {
            context.Entry(entidad).State = EntityState.Added;
            context.SaveChanges();
            return entidad;
        }

        public T AddOrUpdate(T entidad)
        {
            var entry = context.Entry(entidad);
            switch (entry.State)
            {
                case EntityState.Detached:
                    add(entidad);
                    break;
                case EntityState.Modified:
                    Update(entidad);
                    break;
                case EntityState.Added:
                    add(entidad);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();

            }
            return entidad;
        }

        public void Delete(int id)
        {
            T existing = dbSet.Find(id);
            if (existing != null)
            {
                dbSet.Remove(existing);
            }
            //var entidad = new T() { Id = id };
            //context.Entry(entidad).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public IEnumerable<T> FindBy(QueryParameter<T> queryParameter)
        {
            var orderByClass = GetOrderBy(queryParameter);
            Expression<Func<T, bool>> whereTrue = x => true;
            var where = (queryParameter.Where == null) ? whereTrue : queryParameter.Where;
            if (orderByClass.IsAscending)
            {
                return context.Set<T>().Where(where).OrderBy(orderByClass.OrderBy)
                .Skip((queryParameter.Page - 1) * queryParameter.Top)
                .Take(queryParameter.Top).ToList();
            }
            else
            {
                return context.Set<T>().Where(where).OrderByDescending(orderByClass.OrderBy)
                .Skip((queryParameter.Page - 1) * queryParameter.Top)
                .Take(queryParameter.Top).ToList();
            }
        }

        public IEnumerable<T> FindWhere(Expression<Func<T, bool>> LamdaExpression)
        {
            return context.Set<T>().Where(LamdaExpression).ToList();
        }

        public T FindFirstWhere(Expression<Func<T, bool>> LamdaExpression)
        {
            return context.Set<T>().Where(LamdaExpression).FirstOrDefault();
        }

        private OrderByClass GetOrderBy(QueryParameter<T> queryParameter)
        {
            if (queryParameter.OrderBy == null && queryParameter.OrderByDescending == null)
            {
                return new OrderByClass(x => x.Id, true);
            }

            return (queryParameter.OrderBy != null)
                ? new OrderByClass(queryParameter.OrderBy, true) :
                new OrderByClass(queryParameter.OrderByDescending, false);
        }

        public T FindById(int id)
        {
            return context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return context.Set<T>().Where(where).Count();
        }

        public IEnumerable<T> Getall()
        {
            return dbSet.ToList();
        }

        public void InsertBulk(IEnumerable<T> entities, string parentTableName)
        {
            BulkOperation.Operation bulkOperation = new BulkOperation.Operation(context.Database);
            bulkOperation.BulkInsert(entities, parentTableName);
        }

        public void InsertBulk(IEnumerable<T> entities, string parentTableName, int batchSize)
        {
            BulkOperation.Operation bulkOperation = new BulkOperation.Operation(context.Database);
            bulkOperation.BulkInsert(entities, parentTableName,batchSize);
        }

        public void DeleteBulk(IEnumerable<T> entities, string tableName)
        {
            BulkOperation.Operation bulkOperation = new BulkOperation.Operation(context.Database);
            bulkOperation.BulkInsert(entities, tableName);
        }

        private class OrderByClass
        {

            public OrderByClass()
            {

            }

            public OrderByClass(Func<T, object> orderBy, bool isAscending)
            {
                OrderBy = orderBy;
                IsAscending = isAscending;
            }


            public Func<T, object> OrderBy { get; set; }
            public bool IsAscending { get; set; }
        }
    }
}
