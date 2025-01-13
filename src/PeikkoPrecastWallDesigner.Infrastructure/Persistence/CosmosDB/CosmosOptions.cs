namespace PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB
{
	public class CosmosOptions
	{
		public string Endpoint { get; set; } = string.Empty;
		public string Key { get; set; } = string.Empty;
		public List<CosmosDatabaseOptions> Databases { get; set; } = new();
		//public CosmosOptions(string endpoint, string key)
		//{
		//	Endpoint = endpoint;
		//	Key = key;
		//	Databases = new();
		//}
	}

	public class CosmosDatabaseOptions
	{
		public required string DatabaseName { get; set; }
		public List<CosmosContainerOptions> Containers { get; set; } = new();
	}

	public class CosmosContainerOptions
	{
		public required string ContainerName { get; set; }
		public required string PartitionKey { get; set; }
	}
}
