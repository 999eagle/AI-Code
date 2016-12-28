using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public class DepthFirstSearch : BaseTreeSearch
	{
		public DepthFirstSearch(ISearchProblem problem) : base(problem)
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
			_openList.Insert(0, newNode);
		}
		protected override void SortOpenList()
		{
		}
	}
}
