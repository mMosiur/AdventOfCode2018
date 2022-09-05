namespace AdventOfCode.Year2018.Day13;

public static class TrackMapParser
{
	public static TrackSymbol[,] Parse(string[] input)
	{
		if (input.Length == 0)
		{
			throw new FormatException("Input is empty");
		}
		int height = input.Length;
		int width = input[0].Length;
		if (!input.All(row => row.Length == width))
		{
			throw new FormatException("Input is not rectangular");
		}
		TrackSymbol[,] map = new TrackSymbol[width, height];
		for (int y = 0; y < input.Length; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				TrackSymbol symbol = (TrackSymbol)input[y][x];
				if (!Enum.IsDefined(symbol))
				{
					throw new FormatException($"Invalid symbol '{symbol}' at {x},{y}");
				}
				map[x, y] = symbol;
			}
		}
		return map;
	}
}
