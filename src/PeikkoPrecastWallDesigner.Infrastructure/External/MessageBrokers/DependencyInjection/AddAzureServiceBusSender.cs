using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers;
using PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus;

namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers
{
	public static partial class MessageBrokerDepdendencyInjection
	{
		public static IServiceCollection AddAzureServiceBusSender<T>(
			this IServiceCollection services,
			IConfiguration configuration,
			AzureServiceBusOptions options
		)
		{
			services.AddSingleton<IMessageSender<T>>(new AzureServiceBusSender<T>(
						 options.ConnectionString,
						 options.QueueNames[typeof(T).Name]));

			return (services);
		}
	}

}
