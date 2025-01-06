using PeikkoPrecastWallDesigner.Domain.Services.Computations;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace PeikkoPrecastWallDesigner.Background.Common.BackgroundTaskQueue
{
	public class BackgroundTaskQueue<T> : IBackgroundTaskQueue<T>
	{
		private readonly Channel<Func<CancellationToken, ValueTask<T>>> _queue;

		public BackgroundTaskQueue(int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
			}

			var options = new BoundedChannelOptions(capacity)
			{
				FullMode = BoundedChannelFullMode.Wait
			};
			_queue = Channel.CreateBounded<Func<CancellationToken, ValueTask<T>>>(options);
		}

		public async ValueTask QueueTaskAsync(Func<CancellationToken, ValueTask<T>> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException(nameof(workItem), "Work item cannot be null.");
			}

			await _queue.Writer.WriteAsync(workItem);
		}

		public async ValueTask<Func<CancellationToken, ValueTask<T>>> DequeueAsync(CancellationToken cancellationToken)
		{
			return await _queue.Reader.ReadAsync(cancellationToken);
		}
	}
}
