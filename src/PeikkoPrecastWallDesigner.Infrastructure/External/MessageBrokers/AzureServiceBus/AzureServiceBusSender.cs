using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers;

namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus
{
	public class AzureServiceBusSender<T> : IMessageSender<T>
	{
		private readonly string _connectionString;
		private readonly string _queueName;

		public AzureServiceBusSender(string connectionString, string queueName)
		{
			_connectionString = connectionString;
			_queueName = queueName;
		}

		public async Task SendMessageAsync(T message, CancellationToken cancellationToken = default)
		{
			try
			{
				await using var client = new ServiceBusClient(_connectionString);
				ServiceBusSender sender = client.CreateSender(_queueName);
				var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Message<T>
				{
					Data = message,
				})));
				await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
			}
			catch (ServiceBusException ex)
			{
				throw new InvalidOperationException("Error occurred while sending the message to Azure Service Bus", ex);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while sending the message", ex);
			}
		}
	}
}
