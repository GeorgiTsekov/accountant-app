namespace AccountantWPF.BaseRepositories
{
    public interface IDeletableRepository<TEntity> : IRepository<TEntity>
    {
        Task<ICollection<TEntity>> AllWithDeleted();
        Task<TEntity> ByNameAndDateWithDeletedAsync(string name, DateTime date);
        Task HardDelete(TEntity entity);
        Task Undelete(TEntity entity);
    }
}
