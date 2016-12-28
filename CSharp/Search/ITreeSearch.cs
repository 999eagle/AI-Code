using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Interface for a tree search algorithm.
	/// </summary>
	public interface ITreeSearch
	{
		/// <summary>
		/// Starts searching until either the first result is found or no result exists.
		/// </summary>
		/// <returns>A <see cref="SearchResult"/> indicating the result of the search.</returns>
		SearchResult StartSearch();
		/// <summary>
		/// Continues the search started by <see cref="StartSearch"/> until either the next result is found or no result exists.
		/// </summary>
		/// <returns>A <see cref="SearchResult"/> indicating the result of the search.</returns>
		SearchResult ContinueSearch();
	}
}
