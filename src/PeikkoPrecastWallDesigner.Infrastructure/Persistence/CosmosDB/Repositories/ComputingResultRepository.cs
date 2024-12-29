using Microsoft.Azure.Cosmos;
using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories;

namespace PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB.Repositories
{
	public class ComputingResultRepository : CosmosRepository<ComputingResult, Guid>, IComputingResultRepository
	{
		public ComputingResultRepository(Container container, string partitionKey)
			: base(container, partitionKey)
		{

		}
	}
}
