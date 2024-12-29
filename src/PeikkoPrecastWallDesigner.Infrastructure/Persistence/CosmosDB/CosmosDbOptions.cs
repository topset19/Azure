namespace PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB
{
	public class CosmosConfig
	{
		public required string Endpoint { get; set; }
		public required string Key { get; set; }
		public List<CosmosDatabaseConfig> Databases { get; set; } = new List<CosmosDatabaseConfig>();
	}

	public class CosmosDatabaseConfig
	{
		public required string DatabaseName { get; set; }
		public List<CosmosContainerConfig> Containers { get; set; } = new List<CosmosContainerConfig>();
	}

	public class CosmosContainerConfig
	{
		public required string ContainerName { get; set; }
		public required string PartitionKey { get; set; }
	}
}
