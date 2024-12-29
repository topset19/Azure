
namespace PeikkoPrecastWallDesigner.Application.Common.DTOs
{
	public record LayerDto
	{
		public required string Name { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public double Thickness { get; set; }
	}
}
