using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Implementation for a breadth-first search.
	/// </summary>
	public sealed class BreadthFirstSearch : BaseTreeSearch
	{
		/// <summary>
		/// Constructor for this algorithm.
		/// </summary>
		/// <param name="problem">The search problem to solve.</param>
		public BreadthFirstSearch(ISearchProblem problem) : base(problem)
		{
		}

		/// <summary>
		/// See <see cref="BaseTreeSearch.CreateStartNode"/>.
		/// </summary>
		protected override Node CreateStartNode()
		{
			return new Node(_searchProblem.GetStartState(), null, null, 0);
		}
		/// <summary>
		/// See <see cref="BaseTreeSearch.CreateNodeFromTransition(Node, StateTransition)"/>.
		/// </summary>
		protected override Node CreateNodeFromTransition(Node parent, StateTransition transition)
		{
			return new Node(parent, transition);
		}
		/// <summary>
		/// See <see cref="BaseTreeSearch.InsertNodeIntoOpenList(Node)"/>.
		/// </summary>
		protected override void InsertNodeIntoOpenList(Node newNode)
		{
			// insert node at the end
			_openList.Add(newNode);
		}
		/// <summary>
		/// See <see cref="BaseTreeSearch.SortOpenList"/>.
		/// </summary>
		protected override void SortOpenList()
		{
			// no sorting needed for a plain breadth first search because new nodes are always inserted at the end of the open list
			// thus the new nodes with the highest depth are always at the end --> breadth first search
		}
	}
}
