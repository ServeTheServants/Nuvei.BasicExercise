using System;

namespace Nuvei.BasicExercise
{
	public class Person : IPerson
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

		public int Height { get; set; }
	}
}