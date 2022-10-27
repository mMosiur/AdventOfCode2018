namespace AdventOfCode.Year2018.Day13;

struct Cart
{
	private int _intersectionsCrossed;
	private Orientation _orientation;

	public Coordinate Position { get; private set; }
	public TrackSymbol SymbolUnderneath { get; private set; }
	public Orientation Orientation
	{
		get => _orientation;
		private set
		{
			if (!Enum.IsDefined(typeof(Orientation), value))
			{
				throw new ArgumentOutOfRangeException(nameof(value), "Invalid orientation");
			}
			if (value is Orientation.Unknown or Orientation.Vertical or Orientation.Horizontal)
			{
				throw new ArgumentException($"Cart orientation cannot be {value}, use a specific direction instead", nameof(value));
			}
			_orientation = value;
		}
	}

	public Coordinate NextPosition
	{
		get
		{
			(int offsetX, int offsetY) = Orientation switch
			{
				Orientation.Up => (0, -1),
				Orientation.Down => (0, 1),
				Orientation.Left => (-1, 0),
				Orientation.Right => (1, 0),
				_ => throw new InvalidOperationException("Invalid orientation")
			};
			return new Coordinate(Position.X + offsetX, Position.Y + offsetY);
		}
	}

	private void TurnRight()
	{
		Orientation = Orientation switch
		{
			Orientation.Up => Orientation.Right,
			Orientation.Down => Orientation.Left,
			Orientation.Left => Orientation.Up,
			Orientation.Right => Orientation.Down,
			_ => throw new InvalidOperationException("Invalid orientation")
		};
	}

	private void TurnLeft()
	{
		Orientation = Orientation switch
		{
			Orientation.Up => Orientation.Left,
			Orientation.Down => Orientation.Right,
			Orientation.Left => Orientation.Down,
			Orientation.Right => Orientation.Up,
			_ => throw new InvalidOperationException("Invalid orientation")
		};
	}

	public void Move(TrackSymbol[,] map)
	{
		TrackSymbol nextSymbol = map[NextPosition.X, NextPosition.Y];
		MoveOnto(nextSymbol);
	}

	private void MoveOnto(TrackSymbol trackSymbol)
	{
		SymbolUnderneath = trackSymbol;
		Position = NextPosition;
		if (SymbolUnderneath is TrackSymbol.TrackUpSlopeConnection)
		{
			switch (Orientation)
			{
				case Orientation.Up:
					Orientation = Orientation.Right;
					break;
				case Orientation.Down:
					Orientation = Orientation.Left;
					break;
				case Orientation.Left:
					Orientation = Orientation.Down;
					break;
				case Orientation.Right:
					Orientation = Orientation.Up;
					break;
			}
		}
		else if (SymbolUnderneath is TrackSymbol.TrackDownSlopeConnection)
		{
			switch (Orientation)
			{
				case Orientation.Up:
					Orientation = Orientation.Left;
					break;
				case Orientation.Down:
					Orientation = Orientation.Right;
					break;
				case Orientation.Left:
					Orientation = Orientation.Up;
					break;
				case Orientation.Right:
					Orientation = Orientation.Down;
					break;
			}
		}
		else if (SymbolUnderneath is TrackSymbol.TrackIntersection)
		{
			switch (_intersectionsCrossed % 3)
			{
				case 0:
					TurnLeft();
					break;
				case 1:
					// Go straight
					break;
				case 2:
					TurnRight();
					break;
			}
			_intersectionsCrossed++;
		}
	}

	public Cart(Coordinate position, Orientation orientation)
	{
		_intersectionsCrossed = 0;
		Position = position;
		SymbolUnderneath = TrackSymbol.Unknown;
		_orientation = Orientation.Unknown;
		Orientation = orientation;
		if (Orientation is Orientation.Left or Orientation.Right)
		{
			SymbolUnderneath = TrackSymbol.TrackHorizontal;
		}
		else if (Orientation is Orientation.Up or Orientation.Down)
		{
			SymbolUnderneath = TrackSymbol.TrackVertical;
		}
		else
		{
			throw new ArgumentException($"Cart orientation cannot be {Orientation}, use a specific direction instead", nameof(orientation));
		}
	}

	public static bool TryCreateFromTrackSymbol(Coordinate position, TrackSymbol trackSymbol, out Cart cart)
	{
		cart = default;
		Orientation orientation = trackSymbol switch
		{
			TrackSymbol.CartUp => Orientation.Up,
			TrackSymbol.CartDown => Orientation.Down,
			TrackSymbol.CartLeft => Orientation.Left,
			TrackSymbol.CartRight => Orientation.Right,
			_ => Orientation.Unknown
		};
		if (orientation is Orientation.Unknown)
		{
			return false;
		}
		cart = new Cart(position, orientation);
		return true;
	}
}
