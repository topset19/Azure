using System.Linq.Expressions;
using PeikkoPrecastWallDesigner.Domain.Entities;

namespace PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories
{
	public interface ICosmosRepository<TEntity, Tid> where TEntity : Entity<Tid>
	{
		Task<TEntity?> GetByIdAsync(Tid id);
		Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task PatchAsync(Tid id, Dictionary<string, object> patchOperations);
		Task DeleteAsync(Tid id);
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
	}
}
