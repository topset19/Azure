using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Service.Computations
{
	public partial class ComputingDService : IComputingDService
	{
		private double HoleOverlap(Hole hole, Layer layer)
		{
			double	overlapX = Math.Max(layer.X, hole.X);
			double	overlapY = Math.Max(layer.Y, hole.Y);
			double	overlapWidth = Math.Max(0, Math.Min(layer.X + layer.Width, hole.X + hole.Width) - overlapX);
			double	overlapHeight = Math.Max(0, Math.Min(layer.Y + layer.Height, hole.Y + hole.Height) - overlapY);
			double	overlapArea = (overlapWidth * overlapHeight) / 1_000_000;
			return (overlapArea);
		}
	}
}