using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Implementation for the A* algorithm.
	/// </summary>
	public sealed class AStarSearch : BaseTreeSearch
	{
		/// <summary>
		/// Instance of <see cref="HeuristicNodeComparer"/> to sort the nodes in the open list.
		/// </summary>
		HeuristicNodeComparer _comparer;

		/// <summary>
		/// Constructor for this algorithm.
		/// </summary>
		/// <param name="problem">The search problem to solve.</param>
		public AStarSearch(IHeuristicSearchProblem problem) : base(problem)
		{
			_comparer = new HeuristicNodeComparer();
		}

		/// <summary>
		/// See <see cref="BaseTreeSearch.CreateStartNode"/>.
		/// </summary>
		protected override Node CreateStartNode()
		{
			return new HeuristicNode(_searchProblem.GetStartState(), null, null, 0, ((IHeuristicSearchProblem)_searchProblem).GetHeuristic);
		}
		/// <summary>
		/// See <see cref="BaseTreeSearch.CreateNodeFromTransition(Node, StateTransition)"/>.
		/// </summary>
		protected override Node CreateNodeFromTransition(Node parent, StateTransition transition)
		{
			return new HeuristicNode((HeuristicNode)parent, transition, ((IHeuristicSearchProblem)_searchProblem).GetHeuristic);
		}
		/// <summary>
		/// See <see cref="BaseTreeSearch.InsertNodeIntoOpenList(Node)"/>.
		/// </summary>
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
		/// <summary>
		/// See <see cref="BaseTreeSearch.SortOpenList"/>.
		/// </summary>
		protected override void SortOpenList()
		{
			_openList.Sort(_comparer);
		}
	}
}
