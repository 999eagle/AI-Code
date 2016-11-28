using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public sealed class BreadthFirstSearch
	{
		ISearchProblem _problem;
		List<Node> _openList = new List<Node>();

		public BreadthFirstSearch(ISearchProblem problem)
		{
			_problem = problem;
		}

		public List<StateTransition> Search()
		{
			_openList.Clear();
			_openList.Add(new Node(_problem.GetStartState(), null, null, 0));
			while (true)
			{
				var node = _openList[0];
				foreach (var transition in _problem.GetTransitions(node.State))
				{
					var newNode = new Node(node, transition);
					if (_problem.IsFinalState(newNode.State))
					{
						return GetTransitionsToNode(newNode);
					}
					_openList.Add(newNode);
				}
				_openList.Remove(node);
			}
		}

		private List<StateTransition> GetTransitionsToNode(Node node)
		{
			if (node.Parent == null)
			{
				var list = new List<StateTransition>();
				list.Add(new StateTransition { Action = "Start", ActionCost = 0, NewState = node.State });
				return list;
			}
			var parentTransitions = GetTransitionsToNode(node.Parent);
			parentTransitions.Add(new StateTransition {
				Action = node.Action,
				ActionCost = node.PathCost - node.Parent.PathCost,
				NewState = node.State });
			return parentTransitions;
		}
	}
}
