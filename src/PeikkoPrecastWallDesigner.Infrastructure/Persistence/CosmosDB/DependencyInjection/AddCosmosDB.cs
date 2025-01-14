using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories;
using PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB.Repositories;
using PeikkoPrecastWallDesigner.Domain.Entities;

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
				}
			);

			services.AddCosmosDbRepositories<ComputingResult, Guid>(options, cosmosClient, "ComputingResults", "LayerLoads");

			services.AddSingleton(cosmosClient);
			return (services);
		}

		private static IServiceCollection AddCosmosDbRepositories<TEntity, TId>(
			this IServiceCollection services,
			CosmosOptions options,
			CosmosClient cosmosClient,
			string databaseName,
			string containerName
		)
			where TEntity : Entity<TId>
		{
			bool found = false;

			foreach (var databaseConfig in options.Databases)
			{
				if (databaseConfig.DatabaseName == databaseName)
				{
					var database = cosmosClient.GetDatabase(databaseConfig.DatabaseName)
						?? throw new Exception($"CosmosDB: Cannot find `{databaseConfig.DatabaseName}` database.");
					foreach (var containerConfig in databaseConfig.Containers)
					{
						if (containerConfig.ContainerName == containerName)
						{
							var container = database.GetContainer(containerConfig.ContainerName)
								?? throw new Exception($"CosmosDB: Cannot find `{containerConfig.ContainerName}` container in database `{databaseConfig.DatabaseName}`.");
							var containerPartitionKey = containerConfig.PartitionKey;

							services.AddScoped<ICosmosRepository<TEntity, TId>>(sp => new CosmosRepository<TEntity, TId>(container, containerPartitionKey));
							found = true;
						}
					}
				}
			}
			if (!found)
				throw new Exception($"AddCosmosDB: AddCosmosDbRepositories: Failed to find {databaseName} database, {containerName} container.");
			return services;
		}
	}
}
