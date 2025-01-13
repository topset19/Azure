using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeikkoPrecastWallDesigner.Infrastructure
{
	public static class KeyVaultDependencyInjection
	{
		public static IServiceCollection AddKeyVault(
			this IServiceCollection services,
			WebApplicationBuilder builder)
		{
			try
			{
				Console.WriteLine("--------------------------------");
				Console.WriteLine("Started adding Key Vault Config");
				Console.WriteLine("--------------------------------");
				var keyVaultUrl = builder.Configuration["KeyVaultURL"]
					?? throw new Exception("KeyVault URL is missing from the configuration.");

				var azureCredentials = new DefaultAzureCredential();
				builder.Configuration.AddAzureKeyVault(
					new Uri(keyVaultUrl),
					azureCredentials,
					new Azure.Extensions.AspNetCore.Configuration.Secrets.AzureKeyVaultConfigurationOptions()
					{
						ReloadInterval = TimeSpan.FromMinutes(5)
					});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to configure Azure Key Vault: {ex}");
				throw;
			}
			
			return (services);
		}
	}
}
