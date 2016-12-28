using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Represents the node of the search tree in a heuristic tree search algorithm.
	/// </summary>
	public class HeuristicNode : Node, IComparable<HeuristicNode>
	{
		/// <summary>
		/// Estimated cost of the path from this node to the final node.
		/// </summary>
		public int HeuristicCost { get; private set; }
		/// <summary>
		/// Estimated cost of the path from the start node to the final node via this node.
		/// </summary>
		public int EstimatedTotalPathCost
		{
			get { return PathCost + HeuristicCost; }
		}

		/// <summary>
		/// Constructor for creating a heuristic node.
		/// </summary>
		/// <param name="state">The state the new node represents.</param>
		/// <param name="parent">The parent of the new node. Pass null to create the new node as root node.</param>
		/// <param name="action">Description of the transistion to get to the new node.</param>
		/// <param name="actionCost">Cost of the transition to get to the new node. Ignored if <paramref name="parent"/> is null.</param>
		/// <param name="heuristic">Function to estimate the cost of the path from the new node to the final node.</param>
		internal HeuristicNode(State state, HeuristicNode parent, string action, int actionCost, Func<State, int> heuristic)
			: base(state, parent, action, actionCost)
		{
			HeuristicCost = heuristic(state);
		}

		/// <summary>
		/// Constructor for creating a heuristic node based on a parent node and a transition to the new node.
		/// </summary>
		/// <param name="parent">The parent of the new node. Should not be null.</param>
		/// <param name="transition">The transition to get to the new node.</param>
		/// <param name="heuristic">Function to estimate the cost of the path from the new node to the final node.</param>
		internal HeuristicNode(HeuristicNode parent, StateTransition transition, Func<State, int> heuristic)
			: this(transition.NewState, parent, transition.Action, transition.ActionCost, heuristic)
		{
		}

		/// <summary>
		/// Implementation of <see cref="IComparable{HeuristicNode}.CompareTo(HeuristicNode)"/>. Compares this node to another node.
		/// </summary>
		/// <param name="other">The node to compare this node to.</param>
		/// <returns>A negative value if this instance precedes the other instance, 0 if both instances are in the same position and a positive value if
		/// this instance follows the other instance.</returns>
		public int CompareTo(HeuristicNode other)
		{
			return this.EstimatedTotalPathCost.CompareTo(other.EstimatedTotalPathCost);
		}
	}
	/// <summary>
	/// Comparer for sorting heuristic nodes based on their default sorting.
	/// </summary>
	public class HeuristicNodeComparer : IComparer<Node>
	{
		/// <summary>
		/// Compares to nodes and determines their sort order.
		/// </summary>
		/// <param name="lhs">First node.</param>
		/// <param name="rhs">Second node.</param>
		/// <returns>A negative value if the first node precedes the second node, 0 if both nodes are in the same position and a positive value if
		/// the first node follows the second node.</returns>
		public int Compare(Node lhs, Node rhs)
		{
			return ((HeuristicNode)lhs).CompareTo((HeuristicNode)rhs);
		}
	}
}
