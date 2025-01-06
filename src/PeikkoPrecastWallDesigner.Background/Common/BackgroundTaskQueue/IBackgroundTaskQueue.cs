namespace PeikkoPrecastWallDesigner.Background.Common.BackgroundTaskQueue
{
	public interface IBackgroundTaskQueue<T>
	{
		ValueTask QueueTaskAsync(Func<CancellationToken, ValueTask<T>> workItem);
		ValueTask<Func<CancellationToken, ValueTask<T>>> DequeueAsync(CancellationToken cancellationToken);
	}
}

