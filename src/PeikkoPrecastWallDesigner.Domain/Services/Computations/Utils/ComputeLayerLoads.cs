using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Services.Computations
{
	public partial class ComputingDService : IComputingDService
	{
		public LayerLoads ComputeLayerLoads(Layer layer, double holeArea)
		{
			double surfaceArea = ((layer.Width * layer.Height) / 1_000_000 - holeArea);
			double volume = surfaceArea * (layer.Thickness / 1000);
			double density = 2400f;
			double weightKg = volume * density;
			double weightKn = weightKg * 0.01;
			return new LayerLoads
			{
				Name = layer.Name,
				SurfaceArea = Math.Round(surfaceArea, 2),
				Volume = Math.Round(volume, 2),
				WeightKg = Math.Round(weightKg, 2),
				WeightKn = Math.Round(weightKn, 2)
			};
		}
	}
}
