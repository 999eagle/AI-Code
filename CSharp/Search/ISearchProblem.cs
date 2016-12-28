using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Interface for a search problem. Implementing this usually requires also creating a class for states derived from <see cref="State"/>.
	/// </summary>
	public interface ISearchProblem
	{
		/// <summary>
		/// Gets the state where the search should start.
		/// </summary>
		/// <returns>The state where the search should start.</returns>
		State GetStartState();
		/// <summary>
		/// Determines whether a given state is a final state of the search.
		/// </summary>
		/// <param name="state">The state to test.</param>
		/// <returns>True if the given state is a final state, otherwise false.</returns>
		bool IsFinalState(State state);
		/// <summary>
		/// Gets a list of transitions possible from a given state.
		/// </summary>
		/// <param name="state">The state to transition from.</param>
		/// <returns>An enumerable of possible transitions from the given state.</returns>
		IEnumerable<StateTransition> GetTransitions(State state);
	}

	/// <summary>
	/// Generic abstract base class for a search problem using custom states.
	/// </summary>
	/// <typeparam name="T">The type representing the states of the search problem. Must be derived from <see cref="State"/>.</typeparam>
	public abstract class SearchProblem<T> : ISearchProblem where T : State
	{
		/// <summary>
		/// Default implementation for getting the start state. Should be overridden in a derived class.
		/// </summary>
		/// <returns>The start state of the search.</returns>
		public virtual State GetStartState() { return default(T); }

		/// <summary>
		/// Determines whether a given state is a final state of the search.
		/// Wrapper for <see cref="IsFinalState(T)"/> compatible with <see cref="ISearchProblem"/>.
		/// </summary>
		/// <param name="state">The state to test.</param>
		/// <returns>True if the given state is a final state, otherwise false.</returns>
		public virtual bool IsFinalState(State state)
		{
			return IsFinalState((T)state);
		}

		/// <summary>
		/// When overridden in a derived class, determines whether a given state is a final state of the search.
		/// </summary>
		/// <param name="state">The state to test.</param>
		/// <returns>True if the given state is a final state, otherwise false.</returns>
		protected abstract bool IsFinalState(T state);

		/// <summary>
		/// Gets a list of transitions possible from a given state.
		/// Wrapper for <see cref="GetTransitions(T)"/> compatible with <see cref="ISearchProblem"/>.
		/// </summary>
		/// <param name="state">The state to transition from.</param>
		/// <returns>An enumerable of possible transitions from the given state.</returns>
		public virtual IEnumerable<StateTransition> GetTransitions(State state)
		{
			return GetTransitions((T)state);
		}

		/// <summary>
		/// When overridden in a derived class, gets a list of transitions possible from a given state.
		/// </summary>
		/// <param name="state">The state to transition from.</param>
		/// <returns>An enumerable of possible transitions from the given state.</returns>
		protected abstract IEnumerable<StateTransition> GetTransitions(T state);
	}
}
