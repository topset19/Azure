using PeikkoPrecastWallDesigner.Application.Computations.DTOs;
using PeikkoPrecastWallDesigner.Domain.Entities;
namespace PeikkoPrecastWallDesigner.Application.Computations.Mappers
{
	public static class ComputingResultMapper
	{
		public static ComputingResultDto ToDto(ComputingResult result)
		{
			return new ComputingResultDto
			{
				Id = result.Id,
				Value = result.Value,
				Status = result.Status,
			};
		}
	}
}
