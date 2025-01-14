using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Azure.Identity;
using PeikkoPrecastWallDesigner.Application.Computations.DTOs;
using PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB;
using PeikkoPrecastWallDesigner.Infrastructure.External.MessageBrokers;


namespace PeikkoPrecastWallDesigner.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
		{
			var configuration = builder.Configuration;
			var appSettings = builder.Services.BuildServiceProvider().GetRequiredService<AppSettings>();

			services.AddCosmosDB(configuration, appSettings.CosmosDb, "pwd-result-db-endpoint", "pwd-result-db-key");

			services.AddMessageBusSender<LayerLoadComputingResultDto>(
				configuration,
				appSettings.MessageBrokers,
				"pwd-bus-backend-connection-string"
			);

			return (services);
		}
	}
}
