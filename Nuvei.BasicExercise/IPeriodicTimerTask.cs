using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nuvei.BasicExercise
{
	public interface IPeriodicTimerTask
	{
		void Start(Func<CancellationToken, Task> func);

		Task StopAsync();
	}
}