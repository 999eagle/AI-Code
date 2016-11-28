using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public interface IHeuristicSearchProblem : ISearchProblem
	{
		int GetHeuristic(State state);
	}

	public abstract class HeuristicSearchProblem<T> : SearchProblem<T>, IHeuristicSearchProblem where T : State
	{
		public virtual int GetHeuristic(State state)
		{
			return GetHeuristic((T)state);
		}

		protected abstract int GetHeuristic(T state);
	}
}
