using AdminStaff.Data;
using AdminStaff.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdminStaff.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> GetAll()
        { 
            return  context.Set<T>().ToList();
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<T> FindAsync(int? id)
        {
           return  await context.Set<T>().FindAsync(id);
           
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);

        }
        public  IEnumerable<T> FindAll( string[] includes )
        {
            IQueryable<T> query = context.Set<T>();
            
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include); 
            return  query.ToList();
        }
        public  IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return  query.Where(criteria).ToList();
        }
        public IEnumerable<T> FindAllfamilies(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                    for (var q=1; query.Count()>=q;q++)
                        query = query.Include(include);
            }
            return query.Where(criteria);
        }

        public T Update(T entity)
        {
            context.Update(entity);
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            context.Update(entity);
            return  entity;
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
