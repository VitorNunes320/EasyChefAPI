namespace Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity obj);
        void Remove(TEntity obj);
        void Edit(TEntity obj);
        void Dispose();
    }
}
