using AccountantWPF.Data;
using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.BaseRepositories
{
    public abstract class BaseDeletableRepository<TEntity> : BaseRepository<TEntity>, IDeletableRepository<TEntity> 
        where TEntity : BaseDeletableModel
    {
        public BaseDeletableRepository(AccountantDbContext db) : base(db)
        {
        }

        public virtual async Task<ICollection<TEntity>> AllWithDeleted() => await base.AllAsync();

        public virtual async Task<TEntity> ByNameAndDateWithDeletedAsync(string name, DateTime date) 
            => await base.ByNameAndDateAsync(name, date);

        public virtual async Task HardDelete(TEntity entity) => await base.Delete(entity);

        public virtual async Task Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            await base.Update(entity);
        }

        public override async Task<ICollection<TEntity>> AllAsync()
        {
            var entities = await base.AllAsync();
            return entities.Where(x => !x.IsDeleted).ToList();
        }

        public override async Task<TEntity> ByNameAndDateAsync(string name, DateTime date)
        {
            var entity = await base.ByNameAndDateAsync(name, date);

            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        public override async Task Delete(TEntity entity)
        {
            entity.DeletedOn = DateTime.Now;
            entity.ModifiedOn = DateTime.Now;
            entity.IsDeleted = true;
            await base.Update(entity);
        }
    }
}
