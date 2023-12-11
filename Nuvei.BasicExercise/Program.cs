using Nuvei.BasicExercise;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

await TestAsync();

Console.WriteLine("Press key");
Console.ReadKey();

static async Task TestAsync()
{
	await using var periodicTimeTask = new PeriodicTimerTask(TimeSpan.FromSeconds(5));

	var personHeightComparer = new PersonIdComparer();

	await using var personCollection = new PersonCollection(personHeightComparer, periodicTimeTask);

	var subscriber1 = new PersonCollectionSubscriber1();

	var subscriber2 = new PersonCollectionSubscriber2();

	personCollection.Add(subscriber1);

	personCollection.Add(subscriber2);

	for (int i = 0; i < 5; i++)
	{
		var person = new Person
		{
			Id = i,
			FirstName = $"FirstName{i}",
			LastName = $"LastName{i}",
			DateOfBirth = new DateTime(1995 + i, 1, 1),
			Height = i
		};

		personCollection.Add(person);
	}

	Console.WriteLine("Press key");
	Console.ReadKey();

	Parallel.For(5, 100, i =>
	{
		var person = new Person
		{
			Id = i,
			FirstName = $"FirstName{i}",
			LastName = $"LastName{i}",
			DateOfBirth = new DateTime(1995 + i, 1, 1),
			Height = i
		};

		personCollection.Add(person);
	});

	personCollection.Remove(subscriber2);

	Console.WriteLine("Press key");
	Console.ReadKey();

	Parallel.For(5, 100, i =>
	{
		var removedPerson = personCollection.Remove();

		if (removedPerson is not null)
			Console.WriteLine($"Removed Person Id: {removedPerson.Id}");
	});

	Console.WriteLine("Press key");
	Console.ReadKey();
}

class PersonIdComparer : IComparer<IPerson>
{
	public int Compare(IPerson person1, IPerson person2)
	{
		return person1.Id.CompareTo(person2.Id);
	}
}

class PersonHeightComparer : IComparer<IPerson>
{
	public int Compare(IPerson person1, IPerson person2)
	{
		return person1.Height.CompareTo(person2.Height);
	}
}

class PersonCollectionSubscriber1 : IPersonCollectionSubscriber
{
	public void Notify(int count)
	{
		Console.WriteLine($"Subscriber1: {count}");
	}
}

class PersonCollectionSubscriber2 : IPersonCollectionSubscriber
{
	public void Notify(int count)
	{
		Console.WriteLine($"Subscriber2: {count}");
	}
}