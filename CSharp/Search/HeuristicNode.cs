using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public class HeuristicNode : Node, IComparable<HeuristicNode>
	{
		public int HeuristicCost { get; private set; }
		public int EstimatedTotalPathCost
		{
			get { return PathCost + HeuristicCost; }
		}

		internal HeuristicNode(State state, HeuristicNode parent, string action, int actionCost, Func<State, int> heuristic)
			: base(state, parent, action, actionCost)
		{
			HeuristicCost = heuristic(state);
		}

		internal HeuristicNode(HeuristicNode parent, StateTransition transition, Func<State, int> heuristic)
			: this(transition.NewState, parent, transition.Action, transition.ActionCost, heuristic)
		{
		}

		public int CompareTo(HeuristicNode other)
		{
			return this.EstimatedTotalPathCost.CompareTo(other.EstimatedTotalPathCost);
		}
	}
	public class HeuristicNodeComparer : IComparer<Node>
	{
		public int Compare(Node lhs, Node rhs)
		{
			return ((HeuristicNode)lhs).CompareTo((HeuristicNode)rhs);
		}
	}
}
