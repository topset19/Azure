using PeikkoPrecastWallDesigner.Domain.Entities;

namespace PeikkoPrecastWallDesigner.Domain.Services.Computations
{
	public interface IComputingDomainService
	{
		void GeometryValidation(Layers data);
		List<LayerLoads> ComputeLoads(Layers data);
	}

}
