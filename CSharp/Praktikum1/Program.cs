using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikum1
{
	class Program
	{
		static void Main(string[] args)
		{
			var problem = new Problem();
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
