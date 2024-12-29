using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;

namespace PeikkoPrecastWallDesigner.Domain.Service.Computations
{
	public partial class ComputingDService : IComputingDService
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

