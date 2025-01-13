using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers
{
	public interface IMessageReceiver<T>
	{
		Task<T> ReceiveMessageAsync(CancellationToken cancellationToken = default);
	}
}
