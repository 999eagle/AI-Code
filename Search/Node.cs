using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public sealed class Node
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
	}
}
