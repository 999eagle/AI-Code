using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search;

namespace Beleg.QueenProblem
{
	/// <summary>
	/// Represents the position of a single queen on the chess board.
	/// </summary>
	struct Position
	{
		/// <summary>
		/// The X coordinate of this position.
		/// </summary>
		public int X { get; set; }
		/// <summary>
		/// The Y coordinate of this position.
		/// </summary>
		public int Y { get; set; }

		public override string ToString()
		{
			return $"({X}, {Y})";
		}
	}
	/// <summary>
	/// Represents a state of the search.
	/// </summary>
	class State : Search.State
	{
		/// <summary>
		/// Contains all positions of currently placed queens.
		/// </summary>
		public Position[] Positions { get; }
		/// <summary>
		/// Creates a new instance of this class using the specified positions for queens.
		/// </summary>
		/// <param name="positions">An array containing the positions of all currently placed queens.</param>
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

		/// <summary>
		/// Checks whether two states are equal. Two states are defined equal when they contain the same positions in not necessarily the same order.
		/// </summary>
		/// <param name="other">The other state to test for equality with this state.</param>
		/// <returns>True if both states are equal, otherwise false.</returns>
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
	/// <summary>
	/// Represents the queen problem with a given board size as a search problem solvable by the Search library.
	/// </summary>
	class Problem : SearchProblem<State>
	{
		/// <summary>
		/// The size of the chess board and also the number of queens to place.
		/// </summary>
		private int _boardSize;
		/// <summary>
		/// Creates a new instance of the queen problem with a given board size.
		/// </summary>
		/// <param name="boardSize">The board size for which the queen problem should be solved.</param>
		public Problem(int boardSize)
		{
			_boardSize = boardSize;
		}
		/// <summary>
		/// Gets the state where the search should start.
		/// </summary>
		/// <returns>The state where the search should start.</returns>
		public override Search.State GetStartState()
		{
			return new State(new Position[0]);
		}
		/// <summary>
		/// Determines whether a given state is a final state of the search.
		/// </summary>
		/// <param name="state">The state to test.</param>
		/// <returns>True if the given state is a final state, otherwise false.</returns>
		protected override bool IsFinalState(State state)
		{
			return state.Positions.Length == _boardSize;
		}
		/// <summary>
		/// Gets a list of transitions possible from a given state.
		/// </summary>
		/// <param name="state">The state to transition from.</param>
		/// <returns>An enumerable of possible transitions from the given state.</returns>
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

		/// <summary>
		/// Given the current state and a position for a new queen, creates a new state representing all placed queens and the new queen.
		/// </summary>
		/// <param name="old">The old state containing all currently placed queens.</param>
		/// <param name="additionalPosition">The position of the new queen to include in the created state.</param>
		/// <returns>A new state representing all placed queens and the new queen.</returns>
		private State CreateStateWithAddedPosition(State old, Position additionalPosition)
		{
			var newPos = new Position[old.Positions.Length + 1];
			Array.Copy(old.Positions, newPos, old.Positions.Length);
			newPos[newPos.Length - 1] = additionalPosition;
			return new State(newPos);
		}
		/// <summary>
		/// Checks that a given position is still free and valid for a new queen on the board.
		/// </summary>
		/// <param name="state">The current state the board is in.</param>
		/// <param name="x">The x coord to check.</param>
		/// <param name="y">The y coord to check.</param>
		/// <returns>True if a queen can be placed on the given coordinates, otherwise false.</returns>
		private bool PositionFree(State state, int x, int y)
		{
			// if any position has either same x, same y or same diagonal the position is not free
			return !state.Positions.Any(p => p.X == x || p.Y == y || Math.Abs(x - p.X) == Math.Abs(y - p.Y));
		}
	}
}
