namespace PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers
{
	public interface IMessageSender<T>
	{
		Task SendMessageAsync(T message);
	}
}
