using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Interface for a heuristic search problem. Derived from <see cref="ISearchProblem"/>.
	/// </summary>
	public interface IHeuristicSearchProblem : ISearchProblem
	{
		/// <summary>
		/// Gets the heuristic for a given state.
		/// </summary>
		/// <param name="state">The state to get the heuristic for.</param>
		/// <returns>Heurisitc of the given state.</returns>
		int GetHeuristic(State state);
	}

	/// <summary>
	/// Generic abstract base class for a heuristic search problem using custom states. Derived from <see cref="SearchProblem{T}"/> with the.
	/// </summary>
	/// <typeparam name="T">The type representing the states of the search problem. Must be derived from <see cref="State"/>.</typeparam>
	public abstract class HeuristicSearchProblem<T> : SearchProblem<T>, IHeuristicSearchProblem where T : State
	{
		/// <summary>
		/// Gets the heuristic for a given state.
		/// Wrapper for <see cref="GetHeuristic(T)"/> compatible with <see cref="IHeuristicSearchProblem"/>.
		/// </summary>
		/// <param name="state">The state to get the heuristic for.</param>
		/// <returns>Heurisitc of the given state.</returns>
		public virtual int GetHeuristic(State state)
		{
			return GetHeuristic((T)state);
		}

		/// <summary>
		/// When overridden in a derived class, gets the heuristic for a given state.
		/// </summary>
		/// <param name="state">The state to get the heuristic for.</param>
		/// <returns>Heurisitc of the given state.</returns>
		protected abstract int GetHeuristic(T state);
	}
}
