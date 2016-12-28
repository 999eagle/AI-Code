using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search;

namespace Beleg.QueenProblem
{
	struct Position
	{
		public int X { get; set; }
		public int Y { get; set; }

		public override string ToString()
		{
			return $"({X}, {Y})";
		}
	}
	class State : Search.State
	{
		public Position[] Positions { get; }
		public State(Position[] positions)
		{
			Positions = positions;
		}

		public override string ToString()
		{
			if (Positions.Length == 0)
			{
				return "Keine Damen auf dem Brett";
			}
			if (Positions.Length == 1)
			{
				return $"Dame auf {Positions[0]}";
			}
			return "Damen auf " + Positions.Select(p => p.ToString()).Aggregate((a,b) => $"{a}, {b}");
		}

		public override bool Equals(Search.State other)
		{
			var s = other as State;
			if (s == null)
				return false;
			if (s.Positions.Length != this.Positions.Length)
				return false;
			for (int i = 0; i < Positions.Length; i++)
			{
				// if all positions in s are different from Positions[i], return false
				if (s.Positions.All(p => p.X != Positions[i].X || p.Y != Positions[i].Y))
					return false;
			}
			return true;
		}
	}
	class Problem : SearchProblem<State>
	{
		private int _boardSize;
		public Problem(int boardSize)
		{
			_boardSize = boardSize;
		}

		public override Search.State GetStartState()
		{
			return new State(new Position[0]);
		}
		protected override bool IsFinalState(State state)
		{
			return state.Positions.Length == _boardSize;
		}
		protected override IEnumerable<StateTransition> GetTransitions(State state)
		{
			var transitions = new List<StateTransition>();
			int x = 0;
			if (state.Positions.Length > 0)
			{
				// only create new states in the next column because every column needs to have a queen, but no column can have more than one queen
				// optimizes from n*n transitions to n
				x = state.Positions.Max(p => p.X) + 1;
			}
			for (int y = 0; y < _boardSize; y++)
			{
				if (PositionFree(state, x, y))
				{
					var pos = new Position { X = x, Y = y };
					transitions.Add(new StateTransition
					{
						Action = $"Dame auf {pos} platzieren",
						ActionCost = 1,
						NewState = CreateStateWithAddedPosition(state, pos)
					});
				}
			}
			return transitions;
		}

		private State CreateStateWithAddedPosition(State old, Position additionalPosition)
		{
			var newPos = new Position[old.Positions.Length + 1];
			Array.Copy(old.Positions, newPos, old.Positions.Length);
			newPos[newPos.Length - 1] = additionalPosition;
			return new State(newPos);
		}
		private bool PositionFree(State state, int x, int y)
		{
			// if any position has either same x, same y or same diagonal the position is not free
			return !state.Positions.Any(p => p.X == x || p.Y == y || Math.Abs(x - p.X) == Math.Abs(y - p.Y));
		}
	}
}
