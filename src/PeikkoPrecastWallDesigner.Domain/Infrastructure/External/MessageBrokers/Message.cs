using System.Text.Json;
using System.Text;

namespace PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers
{
	public class Message<T>
	{

		public T Data { get; set; }

		public string SerializeObject()
		{
			return JsonSerializer.Serialize(this);
		}

		public byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(SerializeObject());
		}
	}
}
