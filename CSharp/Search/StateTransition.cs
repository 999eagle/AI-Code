using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Represents a transition to a new state.
	/// </summary>
	public struct StateTransition
	{
		/// <summary>
		/// New state after the transition.
		/// </summary>
		public State NewState { get; set; }
		/// <summary>
		/// Description of the transition. Only used for output and thus should be human-readable.
		/// </summary>
		public string Action { get; set; }
		/// <summary>
		/// Cost of this transition.
		/// </summary>
		public int ActionCost { get; set; }
	}
}
