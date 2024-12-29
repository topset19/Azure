
namespace PeikkoPrecastWallDesigner.Domain.Entities
{
	public class Layers
	{
		public Layer InternalLayer { get; set; }
		public Layer ExternalLayer { get; set; }
		public InsulatedLayer InsulatedLayer { get; protected set; }
		public Hole Hole { get; set; }

		public Layers(Layer internalLayer, Layer externalLayer, InsulatedLayer insulatedLayer, Hole hole)
		{
			InternalLayer = internalLayer;
			ExternalLayer = externalLayer;
			InsulatedLayer = insulatedLayer;
			Hole = hole;
		}
	}
}
