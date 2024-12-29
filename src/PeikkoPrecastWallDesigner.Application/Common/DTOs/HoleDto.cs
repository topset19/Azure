using System.ComponentModel.DataAnnotations;

namespace PeikkoPrecastWallDesigner.Application.Common.DTOs
{
	public record HoleDto
	{
		public required string Name { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public required string Position { get; set; }
	}
}
