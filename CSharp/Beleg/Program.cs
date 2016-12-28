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
			var problem = new QueenProblem.Problem(12);
			var search = new Search.DepthFirstSearch(problem);
			Console.WriteLine("Suche...");
			var result = search.StartSearch();
			foreach (var transition in result.TransitionsToFinalState)
			{
				Console.WriteLine($"{transition.Action}: {transition.NewState.ToString()}");
			}
			Console.ReadLine();
		}
	}
}
