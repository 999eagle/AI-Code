using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg
{
	/// <summary>
	/// Main class.
	/// </summary>
	class Program
	{
		/// <summary>
		/// Main entry point for the program.
		/// </summary>
		/// <param name="args">Command line arguments. Not used.</param>
		static void Main(string[] args)
		{
			var problem = new QueenProblem.Problem(5);
			var search = new Search.DepthFirstSearch(problem);
			Console.WriteLine("Suche...");
			foreach (var result in search.GetAllSearchResults())
			{
				Console.WriteLine("Ergebnis:");
				foreach (var transition in result.TransitionsToFinalState)
				{
					Console.WriteLine($"{transition.Action}: {transition.NewState.ToString()}");
				}
			}
			Console.ReadLine();
		}
	}
}
