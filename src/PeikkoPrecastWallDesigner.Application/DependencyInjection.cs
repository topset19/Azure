using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeikkoPrecastWallDesigner.Domain;
using PeikkoPrecastWallDesigner.Application.Computations.Services;

namespace PeikkoPrecastWallDesigner.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDomain(configuration);
			services.AddScoped<ComputingAService>();
			return (services);
		}
	}
}
