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
        public T Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            int x = await context.SaveChangesAsync();
            return entity;
        }

        public T add(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
            return entity;
        }

        public async Task<T> addAsyc(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
            var x = await context.SaveChangesAsync();
            return entity;
        }

        public T AddOrUpdate(T entity)
        {
            if (context.Set<T>().Any(e => e.Id == entity.Id))
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                context.Entry(entity).State = EntityState.Added;
            }

            context.SaveChanges();

            return entity;
        }

        public async Task<T> AddOrUpdateAsync(T entity)
        {
            if (context.Set<T>().Any(e => e.Id == entity.Id))
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                context.Entry(entity).State = EntityState.Added;
            }

            await context.SaveChangesAsync();

            return entity;
        }

        public void Delete(int id)
        {
            T existing = dbSet.Find(id);
            if (existing != null)
            {
                dbSet.Remove(existing);
            }
            //var entity = new T() { Id = id };
            //context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            T existing = dbSet.Find(id);
            if (existing != null)
            {
                dbSet.Remove(existing);
            }
            //var entity = new T() { Id = id };
            //context.Entry(entity).State = EntityState.Deleted;
            var x = await context.SaveChangesAsync();
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

        public async Task<IEnumerable<T>> FindWhereAsync(Expression<Func<T, bool>> LamdaExpression)
        {
            return await context.Set<T>().Where(LamdaExpression).ToListAsync<T>();
        }

        public T FindFirstWhere(Expression<Func<T, bool>> LamdaExpression)
        {
            return context.Set<T>().Where(LamdaExpression).FirstOrDefault();
        }

        public async Task<T> FindFirstWhereAsync(Expression<Func<T, bool>> LamdaExpression)
        {
            return await context.Set<T>().Where(LamdaExpression).FirstOrDefaultAsync<T>();
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

        public async Task<T> FindByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync<T>(x => x.Id == id);
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return context.Set<T>().Where(where).Count();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await context.Set<T>().Where(where).CountAsync<T>();
        }

        public IEnumerable<T> Getall()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetallAsyc()
        {
            return await dbSet.ToListAsync<T>();
        }

        public void InsertBulk(IEnumerable<T> entities, string parentTableName)
        {
            BulkOperation.Operation bulkOperation = new BulkOperation.Operation(context.Database);
            bulkOperation.BulkInsert(entities, parentTableName);
        }

        public void InsertBulk(IEnumerable<T> entities, string parentTableName, int batchSize)
        {
            BulkOperation.Operation bulkOperation = new BulkOperation.Operation(context.Database);
            bulkOperation.BulkInsert(entities, parentTableName, batchSize);
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
