using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg
{
	class Program
	{
		static void Main(string[] args)
		{
			var problem = new QueenProblem.Problem(8);
			var search = new Search.BreadthFirstSearch(problem);
			Console.WriteLine("Suche...");
			var transitions = search.Search();
			foreach (var transition in transitions)
			{
				Console.WriteLine($"{transition.Action}: {transition.NewState.ToString()}");
			}
			Console.ReadLine();
		}
	}
}
