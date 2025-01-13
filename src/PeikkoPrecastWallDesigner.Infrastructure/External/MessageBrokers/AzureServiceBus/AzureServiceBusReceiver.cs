using PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers;


namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus
{
	public class AzureServiceBusReceiver<T> : IMessageReceiver<T>
	{
		private readonly string _connectionString;
		private readonly string _queueName;

		public AzureServiceBusReceiver(string connectionString, string queueName)
		{
			_connectionString = connectionString;
			_queueName = queueName;
		}

		public async Task<T> ReceiveMessageAsync(CancellationToken cancellationToken = default)
		{
			return await Task.FromResult(default(T)); // change it when implementing
		}
	}
}
