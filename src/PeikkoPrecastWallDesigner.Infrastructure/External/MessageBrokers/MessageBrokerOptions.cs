using PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers
{
	public class MessageBrokerOptions
	{
		public List<string> Providers { get; set; } = new();

		public AzureServiceBusOptions AzureServiceBus { get; set; } = new();

		public bool UsedAzureServiceBus()
		{
			return Providers.Any(p => p == "AzureServiceBus");
		}
	}
}
