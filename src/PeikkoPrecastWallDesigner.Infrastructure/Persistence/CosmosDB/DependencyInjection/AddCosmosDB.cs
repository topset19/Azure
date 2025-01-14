using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories;
using PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB.Repositories;

namespace PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB
{
	public static class CosmosDbDependencyInjection
	{
		/// <summary>
		/// -----------------------------------------------------
		/// Cosmos DB configuration for one account with
		/// many databases and containers
		/// -----------------------------------------------------
		/// <summary>
		public static IServiceCollection AddCosmosDB(
			this IServiceCollection services,
			IConfiguration configuration,
			CosmosOptions options,
			string dbEndpointKV,
			string dbKeyKV
		)
		{
			Console.WriteLine("---------------------------------------------------");
			Console.WriteLine("Started adding CosmosDB Config");
			Console.WriteLine($"dbEndpointKV: {dbEndpointKV}");
			Console.WriteLine($"dbKeyKV: {dbKeyKV}");
			Console.WriteLine("---------------------------------------------------");

			// Fetching secrets from Key Vault
			var cosmosDbEndpoint = configuration[dbEndpointKV]
				?? throw new Exception("Cosmos DB endpoint is missing in Key Vault.");
			var cosmosDbKey = configuration[dbKeyKV]
				?? throw new Exception("Cosmos DB key is missing in Key Vault.");

			options.Endpoint = cosmosDbEndpoint;
			options.Key = cosmosDbKey;

			// Initialize CosmosClient and register services
			var cosmosClient = new CosmosClient(
				options.Endpoint,
				options.Key,
				new CosmosClientOptions
				{
					SerializerOptions = new CosmosSerializationOptions
					{
						PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
					}
				});
			foreach (var databaseConfig in options.Databases)
			{
				var database = cosmosClient.GetDatabase(databaseConfig.DatabaseName)
					?? throw new Exception($"CosmosDB: Cannot find `{databaseConfig.DatabaseName}` database.");
				foreach (var containerConfig in databaseConfig.Containers)
				{
					var container = database.GetContainer(containerConfig.ContainerName)
						?? throw new Exception($"CosmosDB: Cannot find `{containerConfig.ContainerName}` container in database `{databaseConfig.DatabaseName}`.");
					services.AddCosmosDbRepositories(container, containerConfig.PartitionKey); // change this once having multiple containers
				}
			}
			services.AddSingleton(cosmosClient);
			return (services);
		}

		private static IServiceCollection AddCosmosDbRepositories(
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
