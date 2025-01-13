using PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers;
using PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB;
using PeikkoPrecastWallDesigner.Infrastructure.Logging;

public class AppSettings
{
	public LoggingOptions Logging { get; set; }

	public string AllowedHosts { get; set; }

	public string KeyVaultURL { get; set; }

	public MessageBrokerOptions MessageBrokers { get; set; }

	public CosmosOptions CosmosDb { get; set; }
}