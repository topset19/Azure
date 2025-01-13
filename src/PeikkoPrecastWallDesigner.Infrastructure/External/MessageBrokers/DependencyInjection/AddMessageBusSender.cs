using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers
{
	public static partial class MessageBrokerDepdendencyInjection
	{
		public static IServiceCollection AddMessageBusSender<T>(
			this IServiceCollection services,
			IConfiguration configuration,
			MessageBrokerOptions options,
			string connectionStringKV
			)
		{
			if (options == null)
				throw new ArgumentNullException("MessageBrokerOptions cannot be null");
			if (string.IsNullOrEmpty(connectionStringKV) || string.IsNullOrEmpty(configuration[connectionStringKV]))
				throw new ArgumentNullException("AddMessageBusSender needs a valid connection string stored in Key vault");
			if (options.UsedAzureServiceBus())
			{
				options.AzureServiceBus.ConnectionString = configuration[connectionStringKV];
				services.AddAzureServiceBusSender<T>(configuration, options.AzureServiceBus);
			}
			return (services);
		}
	}
}
