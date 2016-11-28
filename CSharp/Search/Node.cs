using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public class Node : IEquatable<Node>
	{
		public State State { get; private set; }
		public int Depth { get; private set; }
		public Node Parent { get; private set; }
		public string Action { get; private set; }
		public int PathCost { get; private set; }

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

		internal Node(Node parent, StateTransition transition)
			: this(transition.NewState, parent, transition.Action, transition.ActionCost)
		{
		}

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
