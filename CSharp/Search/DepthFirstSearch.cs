using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Implementation for a depth-first search.
	/// </summary>
	public class DepthFirstSearch : BaseTreeSearch
	{
		/// <summary>
		/// Constructor for this algorithm.
		/// </summary>
		/// <param name="problem">The search problem to solve.</param>
		public DepthFirstSearch(ISearchProblem problem) : base(problem)
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
			_openList.Insert(0, newNode);
		}
		/// <summary>
		/// See <see cref="BaseTreeSearch.SortOpenList"/>.
		/// </summary>
		protected override void SortOpenList()
		{
		}
	}
}
