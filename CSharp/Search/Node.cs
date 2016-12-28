using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Represents the node of the search tree in a tree search algorithm.
	/// </summary>
	public class Node : IEquatable<Node>
	{
		/// <summary>
		/// The state this node represents.
		/// </summary>
		public State State { get; private set; }
		/// <summary>
		/// The depth of this node from the root. Should be 0 for the root node.
		/// </summary>
		public int Depth { get; private set; }
		/// <summary>
		/// The parent of this node. Should be null for the root node.
		/// </summary>
		public Node Parent { get; private set; }
		/// <summary>
		/// Description of the transition to get to this node. Only used for output, thus should be human-readable.
		/// See also <seealso cref="StateTransition.Action"/>.
		/// </summary>
		public string Action { get; private set; }
		/// <summary>
		/// The total cost of the path from the root to this node. Should be 0 for the root node.
		/// </summary>
		public int PathCost { get; private set; }

		/// <summary>
		/// Constructor for creating a node.
		/// </summary>
		/// <param name="state">The state the new node represents.</param>
		/// <param name="parent">The parent of the new node. Pass null to create the new node as root node.</param>
		/// <param name="action">Description of the transition to get to the new node.</param>
		/// <param name="actionCost">Cost of the transition to get to the new node. Ignored if <paramref name="parent"/> is null.</param>
		internal Node(State state, Node parent, string action, int actionCost)
		{
			State = state;
			Parent = parent;
			Action = action;
			if (Parent == null)
			{
				Depth = 0;
				PathCost = 0;
			}
			else
			{
				Depth = Parent.Depth + 1;
				PathCost = Parent.PathCost + actionCost;
			}
		}

		/// <summary>
		/// Constructor for creating a node based on a parent node and a transition to the new node.
		/// </summary>
		/// <param name="parent">The parent of the new node. Should not be null.</param>
		/// <param name="transition">The transition to get to the new node.</param>
		internal Node(Node parent, StateTransition transition)
			: this(transition.NewState, parent, transition.Action, transition.ActionCost)
		{
		}

		/// <summary>
		/// Returns whether this instance is equal to another instance of <see cref="Node"/>. Equality is defined as both nodes representing the same state.
		/// </summary>
		/// <param name="other">The instance of <see cref="Node"/> to check for equality.</param>
		/// <returns>True if this instance and the other instance are equal, otherwise returns false.</returns>
		public bool Equals(Node other)
		{
			if (other == null)
				return false;
			return this.State == other.State;
		}

		public override bool Equals(object obj)
		{
			if (obj is Node)
			{
				return this.Equals(obj as Node);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		public static bool operator ==(Node lhs, Node rhs)
		{
			if (((object)lhs) == null)
			{
				return ((object)rhs) == null;
			}
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Node lhs, Node rhs)
		{
			return !(lhs == rhs);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
