using AccountantWPF.Data;
using AccountantWPF.Data.BaseModels;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.BaseRepositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        protected DbSet<TEntity> DbSet { get; set; }
        protected AccountantDbContext DbContext { get; }

        public BaseRepository(AccountantDbContext db)
        {
            DbContext = db ?? throw new ArgumentNullException(nameof(db));
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> AllAsync()
            => await DbSet.OrderByDescending(x => x.CreatedOn).ThenBy(x => x.ModifiedOn).ToListAsync();

        public virtual async Task<TEntity> ByNameAndDateAsync(int parentId, string name, DateTime date)
        {
            return await DbSet.Where(x => x.Name == name && x.ParentId == parentId).SingleOrDefaultAsync(x => x.CreatedOn == date);
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            if (DbSet.Any(x => x.CreatedOn == entity.CreatedOn && x.Name == entity.Name && x.ParentId == entity.ParentId))
            {
                return false;
            }

            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            var entry = DbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            entity.ModifiedOn = DateTime.UtcNow;
            await DbContext.SaveChangesAsync();
        }
    }
}
