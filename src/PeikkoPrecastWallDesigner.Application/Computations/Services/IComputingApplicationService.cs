using PeikkoPrecastWallDesigner.Application.Common.DTOs;
using PeikkoPrecastWallDesigner.Application.Computations.DTOs;

namespace PeikkoPrecastWallDesigner.Application.Computations.Services
{
	public interface IComputingApplicationService
	{
		Task<ComputingResultDto> ComputeLayerLoadsAsync(LayersDto data);
		Task<ComputingResultDto> ComputeLayerLoadsBackgroundAsync(LayersDto data);

		Task<ComputingResultDto?> GetComputingResultAsync(Guid id);
		Task<bool> PatchComputingResultAsync(ComputingResultDto dto);
	}
}
