using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public struct StateTransition
	{
		public State NewState { get; set; }
		public string Action { get; set; }
		public int ActionCost { get; set; }
	}
}
