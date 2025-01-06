using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Enums;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Services.Computations
{
	public partial class ComputingDService : IComputingDService
	{
		private EHolePosition? HoleOverlapPosition(Layers data)
		{
			double	internalOverlap = HoleOverlap(data.Hole, data.InternalLayer);
			double	externalOverlap = HoleOverlap(data.Hole, data.ExternalLayer);
			EHolePosition	overlapPosition;
			
			if (internalOverlap > 0 && externalOverlap > 0)
				overlapPosition = EHolePosition.Both;
			else if (internalOverlap > 0)
				overlapPosition = EHolePosition.Internal;
			else if (externalOverlap > 0)
				overlapPosition = EHolePosition.External;
			else
				return (null);
			return (overlapPosition);
		}
	}
}