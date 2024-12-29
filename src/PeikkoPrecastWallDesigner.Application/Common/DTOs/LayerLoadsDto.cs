namespace PeikkoPrecastWallDesigner.Application.Common.DTOs;

public record LayerLoadDto
{
	public required string Name { get; set; }
	public double SurfaceArea { get; set; }
	public double Volume { get; set; }
	public double WeightKg { get; set; }
	public double WeightKn { get; set; }
}