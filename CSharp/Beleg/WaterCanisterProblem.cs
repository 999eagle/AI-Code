using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search;

namespace Beleg.WaterCanisterProblem
{
	/// <summary>
	/// Represents a state of the search.
	/// </summary>
	class State : Search.State
	{
		/// <summary>
		/// The amount of water in the first canister.
		/// </summary>
		public int K1 { get; set; }
		/// <summary>
		/// The amount of water in the second canister.
		/// </summary>
		public int K2 { get; set; }
		/// <summary>
		/// Create a new instance of this class using the specified amounts of water for both canisters.
		/// </summary>
		/// <param name="k1">The amount of water in the first canister.</param>
		/// <param name="k2">The amount of water in the second canister.</param>
		public State(int k1, int k2)
		{
			K1 = k1;
			K2 = k2;
		}

		public override string ToString()
		{
			return $"K1: {K1}, K2: {K2}";
		}

		/// <summary>
		/// Checks whether two states are equal. Two states are defined equal when both canisters contain exactly the same amount of water.
		/// </summary>
		/// <param name="other">The other state to test for equality with this state.</param>
		/// <returns>True if both states are equal, otherwise false.</returns>
		public override bool Equals(Search.State other)
		{
			var s = other as State;
			if (s == null)
				return false;
			return s.K1 == this.K1 && s.K2 == this.K2;
		}
	}
	/// <summary>
	/// Represents the queen problem with a given board size as a search problem solvable by the Search library.
	/// </summary>
	class Problem : Search.SearchProblem<State>
	{
		/// <summary>
		/// The target amount of water in any canister.
		/// </summary>
		private int _targetAmount;
		/// <summary>
		/// Creates a new instance of the water canister problem with a given target amount of water.
		/// </summary>
		/// <param name="targetAmount">The target amount of water in any canister.</param>
		public Problem(int targetAmount)
		{
			_targetAmount = targetAmount;
		}
		/// <summary>
		/// Gets the state where the search should start.
		/// </summary>
		/// <returns>The state where the search should start.</returns>
		public override Search.State GetStartState()
		{
			return new State(0, 0);
		}
		/// <summary>
		/// Determines whether a given state is a final state of the search.
		/// </summary>
		/// <param name="state">The state to test.</param>
		/// <returns>True if the given state is a final state, otherwise false.</returns>
		protected override bool IsFinalState(State state)
		{
			return (state.K1 == _targetAmount) || (state.K2 == _targetAmount);
		}
		/// <summary>
		/// Gets a list of transitions possible from a given state.
		/// </summary>
		/// <param name="state">The state to transition from.</param>
		/// <returns>An enumerable of possible transitions from the given state.</returns>
		protected override IEnumerable<Search.StateTransition> GetTransitions(State state)
		{
			var transitions = new List<Search.StateTransition>();
			if (state.K1 < 5)
			{
				transitions.Add(new Search.StateTransition { Action = "K1 füllen", ActionCost = 1, NewState = new State(5, state.K2) });
			}
			if (state.K2 < 7)
			{
				transitions.Add(new Search.StateTransition { Action = "K2 füllen", ActionCost = 1, NewState = new State(state.K1, 7) });
			}
			if (state.K1 > 0)
			{
				transitions.Add(new Search.StateTransition { Action = "K1 leeren", ActionCost = 1, NewState = new State(0, state.K2) });
			}
			if (state.K2 > 0)
			{
				transitions.Add(new Search.StateTransition { Action = "K2 leeren", ActionCost = 1, NewState = new State(state.K1, 0) });
			}
			if (state.K1 > 0 && state.K2 < 7)
			{
				int u = Math.Min(7 - state.K2, state.K1);
				transitions.Add(new Search.StateTransition { Action = "K1 in K2 umfüllen", ActionCost = 1, NewState = new State(state.K1 - u, state.K2 + u) });
			}
			if (state.K1 < 5 && state.K2 > 0)
			{
				int u = Math.Min(5 - state.K1, state.K2);
				transitions.Add(new Search.StateTransition { Action = "K2 in K1 umfüllen", ActionCost = 1, NewState = new State(state.K1 + u, state.K2 - u) });
			}
			return transitions;
		}
	}
}
