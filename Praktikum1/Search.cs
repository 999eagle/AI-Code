using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikum1
{
	class State : Search.State
	{
		public int K1 { get; set; }
		public int K2 { get; set; }
		public State(int k1, int k2)
		{
			K1 = k1;
			K2 = k2;
		}

		public override string ToString()
		{
			return $"K1: {K1}, K2: {K2}";
		}
	}

	class Problem : Search.SearchProblem<State>
	{
		public override Search.State GetStartState()
		{
			return new State(0, 0);
		}

		protected override bool IsFinalState(State state)
		{
			return (state.K1 == 3) || (state.K2 == 3);
		}

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
