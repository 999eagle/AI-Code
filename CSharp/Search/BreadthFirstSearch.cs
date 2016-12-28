using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public sealed class BreadthFirstSearch : BaseTreeSearch
	{
		public BreadthFirstSearch(ISearchProblem problem) : base(problem)
		{
		}

		protected override Node CreateStartNode()
		{
			return new Node(_searchProblem.GetStartState(), null, null, 0);
		}
		protected override Node CreateNodeFromTransition(Node parent, StateTransition transition)
		{
			return new Node(parent, transition);
		}
		protected override void InsertNodeIntoOpenList(Node newNode)
		{
			// insert node at the end
			_openList.Add(newNode);
		}
		protected override void SortOpenList()
		{
			// no sorting needed for a plain breadth first search because new nodes are always inserted at the end of the open list
			// thus the new nodes with the highest depth are always at the end --> breadth first search
		}
	}
}
