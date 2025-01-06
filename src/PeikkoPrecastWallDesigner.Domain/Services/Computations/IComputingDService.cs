using PeikkoPrecastWallDesigner.Domain.Entities;

namespace PeikkoPrecastWallDesigner.Domain.Services.Computations
{
	public interface IComputingDService
	{
		void GeometryValidation(Layers data);
		List<LayerLoads> ComputeLoads(Layers data);
	}

}
