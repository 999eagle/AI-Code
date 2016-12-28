using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public abstract class BaseTreeSearch : ITreeSearch
	{
		protected List<Node> _openList, _closedList;
		protected ISearchProblem _searchProblem;

		protected BaseTreeSearch(ISearchProblem problem)
		{
			_searchProblem = problem;
			_openList = new List<Node>();
			_closedList = new List<Node>();
		}

		public IEnumerable<StateTransition> Search()
		{
			// initialize open and closed list
			_openList.Clear();
			_closedList.Clear();
			_openList.Add(CreateStartNode());
			while (_openList.Any())
			{
				var node = _openList[0];
				// check for final node
				if (_searchProblem.IsFinalState(node.State))
				{
					return GetTransitionsToNode(node);
				}
				_openList.Remove(node);
				_closedList.Add(node);
				// iterate through transitions
				foreach (var transition in _searchProblem.GetTransitions(node.State))
				{
					// transition brings us to state on the closed list --> no need to investigate this anymore
					if (_closedList.Any(n => n.State == transition.NewState))
						continue;
					// create child node
					var newNode = CreateNodeFromTransition(node, transition);
					InsertNodeIntoOpenList(newNode);
				}
				SortOpenList();
			}
			return null;
		}

		protected abstract Node CreateStartNode();
		protected abstract Node CreateNodeFromTransition(Node parent, StateTransition transition);
		protected abstract void InsertNodeIntoOpenList(Node newNode);
		protected abstract void SortOpenList();

		protected List<StateTransition> GetTransitionsToNode(Node node)
		{
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
	}
}
