namespace AccountantWPF.BaseRepositories
{
    public interface IRepository<TEntity>
    {
        Task<ICollection<TEntity>> AllAsync();
        Task<bool> CreateAsync(TEntity entity);
        Task<TEntity> ByNameAndDateAsync(string name, DateTime date);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
