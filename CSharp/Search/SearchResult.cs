using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public sealed class SearchResult
	{
		public bool Success { get { return _finalNode != null; } }
		public State FinalState { get { return _finalNode?.State; } }
		public List<StateTransition> TransitionsToFinalState
		{
			get { return _finalStateTransitions ?? (_finalStateTransitions = GetTransitionsToNode(_finalNode)); }
		}
		public List<State> StatesToFinalState
		{
			get { return _finalStateStates ?? (_finalStateStates = GetStatesToNode(_finalNode)); }
		}

		private List<StateTransition> _finalStateTransitions;
		private List<State> _finalStateStates;
		private Node _finalNode;

		internal SearchResult(Node node)
		{
			_finalNode = node;
		}

		private List<StateTransition> GetTransitionsToNode(Node node)
		{
			if (node == null)
			{
				return null;
			}
			if (node.Parent == null)
			{
				var list = new List<StateTransition>();
				list.Add(new StateTransition { Action = "Start", ActionCost = 0, NewState = node.State });
				return list;
			}
			var parentTransitions = GetTransitionsToNode(node.Parent);
			parentTransitions.Add(new StateTransition
			{
				Action = node.Action,
				ActionCost = node.PathCost - node.Parent.PathCost,
				NewState = node.State
			});
			return parentTransitions;
		}

		private List<State> GetStatesToNode(Node node)
		{
			if (node == null)
			{
				return null;
			}
			if (node.Parent == null)
			{
				var list = new List<State>();
				list.Add(node.State);
				return list;
			}
			var parentStates = GetStatesToNode(node.Parent);
			parentStates.Add(node.State);
			return parentStates;
		}
	}
}
