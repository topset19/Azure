namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus
{
	public class AzureServiceBusOptions
	{
		public string ConnectionString { get; set; }
		public Dictionary<string, string> QueueNames { get; set; }
	}
}
