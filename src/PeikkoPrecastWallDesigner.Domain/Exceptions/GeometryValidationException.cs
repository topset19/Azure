using PeikkoPrecastWallDesigner.Domain.Enums;

namespace PeikkoPrecastWallDesigner.Domain.Exceptions
{
	public class GeometryValidationException : Exception
	{
		public EGeometryValidationExceptionType ExceptionType { get; }
		public GeometryValidationException(string message, EGeometryValidationExceptionType exceptionType) : base(message)
		{
			ExceptionType = exceptionType;
		}
	}
}
