namespace PeikkoPrecastWallDesigner.Domain.Entities;

public class InsulatedLayer : Layer
{

	public InsulatedLayer(string name, double thickness, Layer iLayer, Layer eLayer)
		:base(name, 0,0,0,0,thickness)
	{
		X = Math.Max(iLayer.X, eLayer.X);
		Y = Math.Max(iLayer.Y, eLayer.Y);
		Width = Math.Min(iLayer.X + iLayer.Width, eLayer.X + eLayer.Width) - Math.Max(iLayer.X, eLayer.X);
		Height = Math.Min(iLayer.Y + iLayer.Height, eLayer.Y + eLayer.Height) - Math.Max(iLayer.Y, eLayer.Y);
	}
}
