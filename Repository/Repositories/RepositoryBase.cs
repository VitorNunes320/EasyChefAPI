using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(Guid id)
        {

            return _context.Set<TEntity>().Find(id);

        }

        public virtual void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public virtual void Edit(TEntity obj)
        {

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
