namespace AdventOfCode.Year2018.Day20;

/// <remarks>
/// Value based on two bit signed numbers:
/// <code>
/// 00 =>  0;
/// 01 => +1;
/// 10 => -2 (unused);
/// 11 => -1;
/// </code>
/// Later on stacked to form 4 bit number: xOffset yOffset
/// <code>
/// N = (-1, 0);
/// S = (+1, 0);
/// E = (0, +1);
/// W = (0, -1);
/// </code>
/// </remarks>
public enum Direction : byte
{
	East = 1, // 0b0001
	West = 3, // 0b0011
	South = 4, // 0b0100
	North = 12, // 0b1100
}

public static class DirectionHelpers
{
	public static bool IsDefined(Direction direction) => direction is Direction.East or Direction.West or Direction.South or Direction.North;

	public static Direction Parse(char c) => c switch
	{
		'N' => Direction.North,
		'S' => Direction.South,
		'E' => Direction.East,
		'W' => Direction.West,
		_ => throw new ArgumentException($"Invalid direction: '{c}'"),
	};

	public static bool TryParse(char c, out Direction direction)
	{
		direction = c switch
		{
			'N' => Direction.North,
			'S' => Direction.South,
			'E' => Direction.East,
			'W' => Direction.West,
			_ => default,
		};
		return direction != default;
	}

	public static char ToChar(this Direction d) => d switch
	{
		Direction.North => 'N',
		Direction.South => 'S',
		Direction.East => 'E',
		Direction.West => 'W',
		_ => throw new ArgumentException($"Invalid direction: '{d}'"),
	};
}
