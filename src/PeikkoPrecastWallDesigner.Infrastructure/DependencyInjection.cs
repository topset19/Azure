using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Azure.Messaging.ServiceBus;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories;
using PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers.AzureServiceBus;
using PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB.Repositories;
using PeikkoPrecastWallDesigner.Application.Computations.DTOs;
using Microsoft.AspNetCore.Builder;

namespace PeikkoPrecastWallDesigner.Infrastructure
{
	public static class DependencyInjection
	{
		/// <summary>
		/// -----------------------------------------------------
		/// Main function for dependency injection
		/// -----------------------------------------------------
		/// <summary>
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
		{
			var configuration = builder.Configuration;
	
			//services.AddKeyVault(builder);
			services.AddCosmosDB(configuration);
			services.AddAzureServiceBusSender<LayerLoadsComputingResultDto>(configuration, "LayerLoadsComputingQueue");
			return (services);
		}

		//public static IServiceCollection AddKeyVault(
		//	this IServiceCollection services,
		//	WebApplicationBuilder builder)
		//{
		//	var keyVaultUrl = builder.Configuration["KeyVaultURL"]
		//					  ?? throw new Exception("KeyVault URL is missing from the configuration.");

		//	Console.WriteLine(keyVaultUrl);

		//	var azureCredentials = new DefaultAzureCredential();

		//	builder.Configuration.AddAzureKeyVault(
		//		new Uri(keyVaultUrl),
		//		azureCredentials,
		//		new Azure.Extensions.AspNetCore.Configuration.Secrets.AzureKeyVaultConfigurationOptions()
		//		{
		//			ReloadInterval = TimeSpan.FromMinutes(5) // Optional: Adjust as needed
		//		}
		//	);

		//	return services;
		//}

		public static IServiceCollection AddAzureServiceBusSender<T>(this IServiceCollection services, IConfiguration configuration, string queueName)
		{
			var azureServiceBusOptions = configuration.GetSection("AzureServiceBus").Get<AzureServiceBusOptions>()
				?? throw new Exception("AzureServiceBus configuration section is missing.");

			if (string.IsNullOrEmpty(azureServiceBusOptions.ConnectionString))
				throw new Exception("AzureServiceBus connection string is missing.");
			if (!azureServiceBusOptions.QueueNames.TryGetValue(queueName, out var queueNameString))
				throw new Exception($"Queue name '{queueName}' is missing in AzureServiceBus configuration.");

			services.AddSingleton<IMessageSender<T>>(new AzureServiceBusSender<T>(
				azureServiceBusOptions.ConnectionString,
				queueNameString
			));
			return (services);
		}

		/// <summary>
		/// -----------------------------------------------------
		/// Cosmos DB configuration
		/// -----------------------------------------------------
		/// <summary>
		public static IServiceCollection AddCosmosDB(this IServiceCollection services, IConfiguration configuration)
		{
			var databaseName = configuration["CosmosDb:DatabaseName"]
				?? throw new Exception($"CosmosDB config has an error with `DatabaseName` field.");
			var containerName = configuration["CosmosDb:ContainerName"]
				?? throw new Exception($"CosmosDB config has an error with `ContainerName` field.");
			var partitionKey = configuration["CosmosDb:PartitionKey"]
				?? throw new Exception($"CosmosDB config has an error with `PartitionKey` field.");

			var cosmosClient = new CosmosClient(
				configuration["CosmosDb:Endpoint"],
				configuration["CosmosDb:Key"],
				new CosmosClientOptions
				{
					SerializerOptions = new CosmosSerializationOptions
					{
						PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
					}
				});
			var database = cosmosClient.GetDatabase(databaseName)
				?? throw new Exception($"CosmosDB: Cannot find `{databaseName} database`.");
			var container = database.GetContainer(containerName)
				?? throw new Exception($"CosmosDB: Cannot find {containerName}` container from database `{database.Id}`.");
			
			services.AddSingleton(cosmosClient);
			services.AddRepositories(container, partitionKey);
			return (services);
		}

		private static IServiceCollection AddRepositories(
			this IServiceCollection services,
			Container container,
			string partitionKey
			)
		{
			services.AddScoped<IComputingResultRepository>(sp =>
			{
				return new ComputingResultRepository(container, partitionKey);
			});
			return (services);
		}
	}
}
