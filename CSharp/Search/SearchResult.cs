using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Represents the result of a search.
	/// </summary>
	public sealed class SearchResult
	{
		/// <summary>
		/// True if the search found a final node, otherwise false.
		/// </summary>
		public bool Success { get { return _finalNode != null; } }
		/// <summary>
		/// The final state found by the search.
		/// </summary>
		public State FinalState { get { return _finalNode?.State; } }
		/// <summary>
		/// The transitions taken from the start state to the final state.
		/// </summary>
		public List<StateTransition> TransitionsToFinalState
		{
			get { return _finalStateTransitions ?? (_finalStateTransitions = GetTransitionsToNode(_finalNode)); }
		}
		/// <summary>
		/// The states between the start state and the final state including both ends.
		/// </summary>
		public List<State> StatesToFinalState
		{
			get { return _finalStateStates ?? (_finalStateStates = GetStatesToNode(_finalNode)); }
		}

		/// <summary>
		/// Cache for <see cref="TransitionsToFinalState"/>.
		/// </summary>
		private List<StateTransition> _finalStateTransitions;
		/// <summary>
		/// Cache for <see cref="StatesToFinalState"/>.
		/// </summary>
		private List<State> _finalStateStates;
		/// <summary>
		/// Final node of the search.
		/// </summary>
		private Node _finalNode;

		/// <summary>
		/// Constructor for the search result.
		/// </summary>
		/// <param name="node">Final node found by the search. Should be null if no final state was reached to indicate failure.</param>
		internal SearchResult(Node node)
		{
			_finalNode = node;
		}

		/// <summary>
		/// Helper to get the list of transitions to a given node.
		/// </summary>
		/// <param name="node">The target node.</param>
		/// <returns>List of transitions from the root node to the given node.</returns>
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

		/// <summary>
		/// Helper to get the list of states before a given node.
		/// </summary>
		/// <param name="node">The target node.</param>
		/// <returns>List of states between the root node and the given node including both ends.</returns>
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
