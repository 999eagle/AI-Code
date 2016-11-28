using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public sealed class AStarSearch
	{
		IHeuristicSearchProblem _problem;

		public AStarSearch(IHeuristicSearchProblem problem)
		{
			_problem = problem;
		}

		public List<StateTransition> Search()
		{
			var closedList = new List<HeuristicNode>();
			var openList = new List<HeuristicNode>();
			openList.Add(new HeuristicNode(_problem.GetStartState(), null, null, 0, _problem.GetHeuristic));
			while (openList.Any())
			{
				// open list is sorted --> openList[0] is always the node with the lowest estimated total path cost
				var node = openList[0];
				if (_problem.IsFinalState(node.State))
				{
					// final state --> return path
					return GetTransitionsToNode(node);
				}
				openList.RemoveAt(0);
				closedList.Add(node);
				foreach (var transition in _problem.GetTransitions(node.State))
				{
					// any nodes on the closed list were already checked
					if (closedList.Any(h => h.State == transition.NewState))
						continue;
					// create the new node
					var newNode = new HeuristicNode(node, transition, _problem.GetHeuristic);
					int i;
					for (i = 0; i < openList.Count; i++)
					{
						if (openList[i] == newNode)
						{
							// the node already exists on the open list --> replace it if the new path is shorter
							if (openList[i].PathCost > newNode.PathCost)
							{
								openList[i] = newNode;
							}
							break;
						}
					}
					// node wasn't found in the open list --> add it
					if (i == openList.Count)
					{
						openList.Add(newNode);
					}
					// sort the open list. default sorting for heuristic nodes is by estimated total path cost
					openList.Sort();
				}
			}
			return null;
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
