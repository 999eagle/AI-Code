using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public interface ISearchProblem
	{
		State GetStartState();
		bool IsFinalState(State state);
		IEnumerable<StateTransition> GetTransitions(State state);
	}

	public abstract class SearchProblem<T> : ISearchProblem where T : State
	{
		public virtual State GetStartState() { return default(T); }

		public virtual bool IsFinalState(State state)
		{
			return IsFinalState((T)state);
		}

		protected abstract bool IsFinalState(T state);

		public virtual IEnumerable<StateTransition> GetTransitions(State state)
		{
			return GetTransitions((T)state);
		}

		protected abstract IEnumerable<StateTransition> GetTransitions(T state);
	}
}
