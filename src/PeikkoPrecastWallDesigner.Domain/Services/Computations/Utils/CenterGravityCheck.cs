using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Services.Computations
{
	public partial class ComputingDomainService : IComputingDomainService
	{
		private bool CenterGravityValidation(Layers data)
		{
			Layer	iLayer = data.InternalLayer;
			Layer	eLayer = data.ExternalLayer;
			double 	externalCenterX = eLayer.X + (eLayer.Width / 2);
			double 	externalCenterY = eLayer.Y + (eLayer.Height / 2);

			return (iLayer.X <= externalCenterX && externalCenterX <= iLayer.X + iLayer.Width)
				&& (iLayer.Y <= externalCenterY && externalCenterY <= iLayer.Y + iLayer.Height);
		}
	}
}

