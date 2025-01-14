using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace PeikkoPrecastWallDesigner.Infrastructure
{
	public static class AppSettingDependencyInjection
	{
		public static IServiceCollection AddAppSettings(this IServiceCollection services, WebApplicationBuilder builder)
		{
			try
			{
				var appSettings = new AppSettings();

				builder.Configuration.Bind(appSettings); // partially binding, some secrets must be fetched from Key Vault

				builder.Services.AddSingleton(appSettings);
			}
			catch (Exception ex)
			{
				throw new Exception("Error binding AppSettings from appsettings.json", ex);
			}

			return (services);
		}
	}
}
