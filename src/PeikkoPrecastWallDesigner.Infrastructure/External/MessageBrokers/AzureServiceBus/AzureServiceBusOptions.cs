namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus
{
	public class AzureServiceBusOptions
	{
		public string ConnectionString { get; set; } = string.Empty;
		public Dictionary<string, string> QueueNames { get; set; }
	}
}
