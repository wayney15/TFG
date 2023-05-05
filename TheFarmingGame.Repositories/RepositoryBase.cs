using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private TheFarmingGameDbContext _theFarmingGameDbContext { get; set; }
        public RepositoryBase(TheFarmingGameDbContext theFarmingGameDbContext)
        {
            _theFarmingGameDbContext = theFarmingGameDbContext;
        }
        public IQueryable<T> FindAll() => _theFarmingGameDbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _theFarmingGameDbContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _theFarmingGameDbContext.Set<T>().Add(entity);
        public void Update(T entity) => _theFarmingGameDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _theFarmingGameDbContext.Set<T>().Remove(entity);
    }
}
