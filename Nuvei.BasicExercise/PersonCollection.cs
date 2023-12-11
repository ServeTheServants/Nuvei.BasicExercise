using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nuvei.BasicExercise
{
	public class PersonCollection : IAsyncDisposable
	{
		private readonly List<IPerson> _persons = new();

		private readonly List<IPersonCollectionSubscriber> _subscribers = new();

		private readonly IComparer<IPerson> _personComparer;

		private readonly IPeriodicTimerTask _periodicTimerTask;

		private readonly object _personsLockObject = new();

		private readonly object _subscribersLockObject = new();

		public PersonCollection(IComparer<IPerson> personComparer, IPeriodicTimerTask periodicTimerTask)
		{
			_personComparer = personComparer;

			_periodicTimerTask = periodicTimerTask;

			_periodicTimerTask.Start(cancellationToken =>
				Task.Run(() => Publish(), cancellationToken)
			);
		}

		public void Add(IPerson person)
		{
			lock (_personsLockObject)
			{
				_persons.Add(person);
				_persons.Sort(_personComparer);
				Publish();
			}
		}

		public IPerson Remove()
		{
			lock (_personsLockObject)
			{
				if (_persons.Count == 0)
					return null;

				var maxPerson = _persons[^1];

				_persons.RemoveAt(_persons.Count - 1);

				Publish();

				return maxPerson;
			}
		}

		public void Add(IPersonCollectionSubscriber subscriber)
		{
			lock (_subscribersLockObject)
			{
				_subscribers.Add(subscriber);
			}
		}

		public bool Remove(IPersonCollectionSubscriber subscriber)
		{
			lock (_subscribersLockObject)
			{
				return _subscribers.Remove(subscriber);
			}
		}

		private void Publish()
		{
			lock (_subscribersLockObject)
			{
				lock (_personsLockObject)
				{
					_subscribers.ForEach(subscriber =>
						subscriber.Notify(_persons.Count)
					);
				}
			}
		}

		public async ValueTask DisposeAsync()
		{
			Console.WriteLine("PersonCollection disposed");
			await _periodicTimerTask?.StopAsync();
			GC.SuppressFinalize(this);
		}
	}
}