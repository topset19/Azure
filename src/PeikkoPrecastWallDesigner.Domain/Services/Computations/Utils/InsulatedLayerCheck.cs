using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Service.Computations
{
	public partial class ComputingDService : IComputingDService
	{
		private bool InsulatedLayerValidation(Layers data)
		{
			double	thickness = data.InsulatedLayer.Thickness;

			if (thickness < 40 || thickness > 390)
				return (false);
			return (true);
		}
	}
}