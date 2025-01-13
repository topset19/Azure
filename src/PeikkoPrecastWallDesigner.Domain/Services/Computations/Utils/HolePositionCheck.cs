using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Enums;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Services.Computations
{
	public partial class ComputingDomainService : IComputingDomainService
	{
		private bool HolePositionValidation(Layers data)
		{
			bool CheckHoleAllowedPosition(Hole hole, EHolePosition overlapPosition)
			{
				if ((hole.Position == EHolePosition.Both)
					&& (overlapPosition != EHolePosition.Both))
				{
					return (false);
				}
				return (true);
			}
			Nullable<EHolePosition>	 overlapPosition = HoleOverlapPosition(data);
			if (overlapPosition == null)
			{
				return (false);
			}
			if (!CheckHoleAllowedPosition(data.Hole, overlapPosition.Value))
			{
			}
			return (true);	
		}
	}
}