using System.Text.Json.Serialization;

namespace PeikkoPrecastWallDesigner.Domain.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum EHolePosition
	{
		Internal,
		External,
		Both
	}
}
