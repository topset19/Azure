using PeikkoPrecastWallDesigner.Domain.Enums;

namespace PeikkoPrecastWallDesigner.Domain.Entities
{
	public class Hole
	{
		public string Name { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public double Thickness { get; private set; } = 0;
		public EHolePosition Position { get; set; }

		public Hole(string name, double x, double y, double width, double height,
			EHolePosition position, Layer internalLayer, Layer externalLayer, Layer? insulatedLayer)
		{
			Name = name;
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Position = position;
			Thickness = Position switch
			{
				EHolePosition.Internal => internalLayer.Thickness,
				EHolePosition.External => externalLayer.Thickness,
				EHolePosition.Both => (internalLayer?.Thickness ?? 0) +
									  (insulatedLayer?.Thickness ?? 0) +
									  (externalLayer?.Thickness ?? 0),
				_ => 0
			};
		}
	}
}
