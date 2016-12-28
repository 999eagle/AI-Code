using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Abstract base class for tree search algorithms.
	/// </summary>
	public abstract class BaseTreeSearch : ITreeSearch
	{
		/// <summary>
		/// List of nodes to check in the search.
		/// </summary>
		protected List<Node> _openList;
		/// <summary>
		/// List of checked nodes.
		/// </summary>
		protected List<Node> _closedList;
		/// <summary>
		/// Problem to solve in this search.
		/// </summary>
		protected ISearchProblem _searchProblem;
		/// <summary>
		/// Indicates whether the search was already started with <see cref="StartSearch"/>.
		/// </summary>
		protected bool _searchStarted = false;

		/// <summary>
		/// Constructor to be called from derived classes.
		/// </summary>
		/// <param name="problem">The problem to solve in this search.</param>
		protected BaseTreeSearch(ISearchProblem problem)
		{
			_searchProblem = problem;
			_openList = new List<Node>();
			_closedList = new List<Node>();
		}

		/// <summary>
		/// Starts searching until either the first result is found or no result exists.
		/// </summary>
		/// <returns>A <see cref="SearchResult"/> indicating the result of the search.</returns>
		public SearchResult StartSearch()
		{
			// initialize open and closed list
			_openList.Clear();
			_closedList.Clear();
			_openList.Add(CreateStartNode());
			_searchStarted = true;
			return ContinueSearch();
		}

		/// <summary>
		/// Continues the search started by <see cref="StartSearch"/> until either the next result is found or no result exists.
		/// </summary>
		/// <returns>A <see cref="SearchResult"/> indicating the result of the search.</returns>
		public SearchResult ContinueSearch()
		{
			if (!_searchStarted)
			{
				return null;
			}
			while (_openList.Any())
			{
				var node = _openList[0];
				// check for final node
				if (_searchProblem.IsFinalState(node.State))
				{
					return new SearchResult(node);
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
			return new SearchResult(null);
		}

		/// <summary>
		/// Gets multiple search results or all results if limit is 0.
		/// </summary>
		/// <param name="limit">The maximum number of results to return or 0 to return all results.</param>
		/// <returns>An enumerator of search results.</returns>
		public IEnumerator<SearchResult> GetAllSearchResults(int limit = 0)
		{
			int found = 0;
			SearchResult result;
			do
			{
				if (!_searchStarted)
				{
					result = StartSearch();
				}
				else
				{
					result = ContinueSearch();
				}
				found++;
				if (result.Success)
				{
					yield return result;
				}
				else
				{
					yield break;
				}
			} while (limit == 0 || found < limit);
		}

		/// <summary>
		/// When overridden in a derived class, returns the <see cref="Node"/> at which the search should start.
		/// </summary>
		/// <returns>The <see cref="Node"/> at which the search should start.</returns>
		protected abstract Node CreateStartNode();
		/// <summary>
		/// When overridden in a derived class, returns a new <see cref="Node"/> corresponding to a given transition from a given node.
		/// </summary>
		/// <param name="parent">The <see cref="Node"/> from which to transition.</param>
		/// <param name="transition">The <see cref="StateTransition"/> describing the transition to the new node.</param>
		/// <returns>The <see cref="Node"/> corresponding to applying the given transition to the given node.</returns>
		protected abstract Node CreateNodeFromTransition(Node parent, StateTransition transition);
		/// <summary>
		/// When overridden in a derived class, inserts a node into the open list.
		/// </summary>
		/// <param name="newNode">The <see cref="Node"/> to insert into the open list.</param>
		protected abstract void InsertNodeIntoOpenList(Node newNode);
		/// <summary>
		/// When overridden in a derived class, sorts the open list according to the search algorithm implemented.
		/// </summary>
		protected abstract void SortOpenList();
	}
}
