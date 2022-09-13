using System.Text;
using AdventOfCode.Year2018.Day15.Map;
using AdventOfCode.Year2018.Day15.Map.Units;

namespace AdventOfCode.Year2018.Day15;

public class CombatMap
{
	private readonly MapSpot?[,] _map;

	public int Height => _map.GetLength(0);
	public int Width => _map.GetLength(1);

	public CombatMap(MapSpot?[,] map)
	{
		_map = map;
	}

	public static CombatMap FromRawMap(MapSpotType[,] map)
	{
		MapSpot?[,] spots = new MapSpot[map.GetLength(0), map.GetLength(1)];
		for (int x = 0; x < map.GetLength(0); x++)
		{
			for (int y = 0; y < map.GetLength(1); y++)
			{
				spots[x, y] = MapSpot.NewFromType(map[x, y], new Coordinate(x, y));
			}
		}
		return new CombatMap(spots);
	}

	public MapSpot? this[int x, int y]
	{
		get => _map[x, y];
		private set => _map[x, y] = value;
	}

	public MapSpot? this[Coordinate coord]
	{
		get => this[coord.X, coord.Y];
		private set => this[coord.X, coord.Y] = value;
	}

	public IEnumerable<Unit> EnumerateUnits()
	{
		return EnumerateCoordinates().Select(c => this[c]).OfType<Unit>();
	}

	public IEnumerable<Coordinate> PositionsOfType(MapSpotType type)
	{
		return EnumerateCoordinates().Where(c => this[c]?.Type == type);
	}

	public IEnumerable<Coordinate> Adjacent(Coordinate coordinate)
	{
		return coordinate.AdjacentCoordinates().Where(c => c.X >= 0 && c.X < Height && c.Y >= 0 && c.Y < Width);
	}

	public IEnumerable<Coordinate> AdjacentOfType(Coordinate coordinate, MapSpotType type)
	{
		if (!Enum.IsDefined(type))
		{
			throw new ArgumentException($"Invalid map spot type '{type}'.", nameof(type));
		}
		if (type is MapSpotType.OpenCavern)
		{
			return AdjacentEmpty(coordinate);
		}
		return Adjacent(coordinate).Where(c => this[c]?.Type == type);
	}

	public IEnumerable<Coordinate> AdjacentEmpty(Coordinate coordinate)
	{
		return Adjacent(coordinate).Where(c => this[c] is null);
	}

	public void DeleteUnit(Coordinate unitPosition)
	{
		if (this[unitPosition] is not Unit)
		{
			throw new ArgumentException($"No unit at position {unitPosition}.", nameof(unitPosition));
		}
		this[unitPosition] = null;
	}

	public void MoveUnit(Unit unit, Coordinate targetPosition)
	{
		if (!unit.Position.IsAdjacent(targetPosition))
		{
			throw new ArgumentException($"Cannot make a move from {unit.Position} to {targetPosition}: they are not adjacent.", nameof(targetPosition));
		}
		if (this[targetPosition] is not null)
		{
			throw new ArgumentException($"Cannot make a move to {targetPosition} to {targetPosition}: target position is not empty.", nameof(targetPosition));
		}
		this[unit.Position] = null;
		this[targetPosition] = unit;
		unit.Position = targetPosition;
	}

	public IEnumerable<Coordinate> EnumerateCoordinates()
	{
		for (int x = 0; x < Height; ++x)
		{
			for (int y = 0; y < Width; ++y)
			{
				yield return new Coordinate(x, y);
			}
		}
	}

	public void Print(Coordinate? highlightPosition = null)
	{
		StringBuilder builder = new();
		for (int x = 0; x < Height; x++)
		{
			for (int y = 0; y < Width; y++)
			{
				Coordinate coordinate = new(x, y);
				if (coordinate == highlightPosition)
				{
					builder.Append('@');
				}
				else
				{
					var v = this[x, y];
					char c = v is null ? '.' : (char)v.Type;
					builder.Append(c);
				}
			}
			builder.AppendLine();
		}
		Console.Write(builder.ToString());
	}
}
