using PeikkoPrecastWallDesigner.Domain.Entities;

namespace PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories
{
	public interface IRepository<TEntity, Tid> where TEntity : Entity<Tid>
	{
		Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(Tid id);
	}
}
