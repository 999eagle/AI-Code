using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
	/// <summary>
	/// Abstract base class describing the full state for a search problem.
	/// </summary>
	public abstract class State : IEquatable<State>
	{
		/// <summary>
		/// When overridden in a derived class, returns whether this instance is equal to another instance of <see cref="State"/>.
		/// </summary>
		/// <param name="other">The instance of <see cref="State"/> to check for equality.</param>
		/// <returns>True if this instance and the other instance are equal, otherwise returns false.</returns>
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
