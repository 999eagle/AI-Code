using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	interface IHeuristicSearchProblem : ISearchProblem
	{
		float GetHeuristic(State state);
	}

	public abstract class HeuristicSearchProblem<T> : SearchProblem<T>, IHeuristicSearchProblem where T : State
	{
		public virtual float GetHeuristic(State state)
		{
			return GetHeuristic((T)state);
		}

		protected abstract float GetHeuristic(T state);
	}
}
