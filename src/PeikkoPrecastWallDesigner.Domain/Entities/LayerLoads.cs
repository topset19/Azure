namespace PeikkoPrecastWallDesigner.Domain.Entities;

public class LayerLoads
{
	public required string Name { get; set; }
	public double SurfaceArea { get; set; }
	public double Volume { get; set; }
	public double WeightKg { get; set; }
	public double WeightKn { get; set; }
}
