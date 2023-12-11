using System;

namespace Nuvei.BasicExercise
{
	public interface IPerson
	{
		int Id { get; }

		string FirstName { get; }

		string LastName { get; }

		DateTime DateOfBirth { get; }

		int Height { get; }
	}
}