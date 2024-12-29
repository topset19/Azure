using PeikkoPrecastWallDesigner.Domain.Entities;

namespace PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories
{
	public interface IComputingResultRepository : ICosmosRepository<ComputingResult, Guid>
	{
	}
}
