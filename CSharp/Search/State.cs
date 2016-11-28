using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	public abstract class State : IEquatable<State>
	{
		public abstract bool Equals(State other);

		public override bool Equals(object obj)
		{
			if (obj is State)
			{
				return Equals((State)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		public static bool operator ==(State left, State right)
		{
			if (((object)left) == null)
			{
				return ((object)right) == null;
			}
			return left.Equals(right);
		}

		public static bool operator !=(State left, State right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
