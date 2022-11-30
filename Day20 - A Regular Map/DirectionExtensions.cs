namespace AdventOfCode.Year2018.Day20;

static class DirectionExtensions
{
	public static Vector ToVector(this Direction direction)
	{
		return direction switch
		{
			Direction.North => new(0, -1),
			Direction.South => new(0, 1),
			Direction.West => new(-1, 0),
			Direction.East => new(1, 0),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unrecognized direction '{direction}'.")
		};
	}
}
