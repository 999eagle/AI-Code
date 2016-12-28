using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public sealed class AStarSearch : BaseTreeSearch
	{
		HeuristicNodeComparer _comparer;

		public AStarSearch(IHeuristicSearchProblem problem) : base(problem)
		{
			_comparer = new HeuristicNodeComparer();
		}

		protected override Node CreateStartNode()
		{
			return new HeuristicNode(_searchProblem.GetStartState(), null, null, 0, ((IHeuristicSearchProblem)_searchProblem).GetHeuristic);
		}
		protected override Node CreateNodeFromTransition(Node parent, StateTransition transition)
		{
			return new HeuristicNode((HeuristicNode)parent, transition, ((IHeuristicSearchProblem)_searchProblem).GetHeuristic);
		}
		protected override void InsertNodeIntoOpenList(Node newNode)
		{
			int i;
			for (i = 0; i < _openList.Count; i++)
			{
				if (_openList[i] == newNode)
				{
					// node with the same state (same node) already exists on the open list
					// --> replace if the path is shorter
					if (_openList[i].PathCost > newNode.PathCost)
					{
						_openList[i] = newNode;
					}
					break;
				}
			}
			// node wasn't found in open list --> add it
			if (i == _openList.Count)
			{
				_openList.Add(newNode);
			}
		}
		protected override void SortOpenList()
		{
			_openList.Sort(_comparer);
		}
	}
}
