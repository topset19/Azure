using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Enums;
using PeikkoPrecastWallDesigner.Domain.Exceptions;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Service.Computations
{
	/// <summary>
	/// Computing Domain Service
	/// </summary>
	public partial class ComputingDService : IComputingDService
	{
		public void GeometryValidation(Layers data)
		{
			if (!CenterGravityValidation(data))
				throw new GeometryValidationException(
					"Centre of gravity outside of internal layer",
					EGeometryValidationExceptionType.CenterGravityOutOfBounds);
			if (!InsulatedLayerValidation(data))
				throw new GeometryValidationException(
					"Invalid thickness of insulation",
					EGeometryValidationExceptionType.InsulationThicknessInvalid);
			if (!HolePositionValidation(data))
				throw new GeometryValidationException(
					"Invalid hole position",
					EGeometryValidationExceptionType.HolePositionInvalid);
		}
	}
}