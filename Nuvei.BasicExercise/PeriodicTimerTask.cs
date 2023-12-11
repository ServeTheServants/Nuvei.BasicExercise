using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nuvei.BasicExercise
{
	public class PeriodicTimerTask : IPeriodicTimerTask, IAsyncDisposable
	{
		private Task _periodicTimerTask;

		private readonly PeriodicTimer _periodicTimer;

		private readonly CancellationTokenSource _cancellationTokenSource = new();

		public PeriodicTimerTask(TimeSpan period)
		{
			_periodicTimer = new PeriodicTimer(period);
		}

		public void Start(Func<CancellationToken, Task> func)
		{
			if (_periodicTimerTask is not null)
				return;

			_periodicTimerTask = RunTaskAsync(func);
		}

		public async Task StopAsync()
		{
			if (_periodicTimerTask is null)
				return;

			_cancellationTokenSource.Cancel();

			await _periodicTimerTask;
		}

		private async Task RunTaskAsync(Func<CancellationToken, Task> func)
		{
			try
			{
				while (await _periodicTimer.WaitForNextTickAsync(_cancellationTokenSource.Token))
					await func(_cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
			}
		}

		public async ValueTask DisposeAsync()
		{
			Console.WriteLine("PeriodicTimerTask disposed");
			await StopAsync();
			_cancellationTokenSource?.Dispose();
			_periodicTimerTask?.Dispose();
			_periodicTimer?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}