namespace PeikkoPrecastWallDesigner.Application.Common.DTOs
{
	public record LayersDto
	{
		public LayerDto InternalLayer { get; set; }
		public LayerDto ExternalLayer { get; set; }
		public double InsulatedLayerThickness { get; set; }
		public HoleDto Hole { get; set; }
	}
}
