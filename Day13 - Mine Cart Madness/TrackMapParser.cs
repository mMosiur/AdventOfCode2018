using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2018.Day13;

static class TrackMapParser
{
	public static TrackSymbol[,] Parse(string[] input)
	{
		if (input.Length == 0)
		{
			throw new FormatException("Input is empty");
		}
		int height = input.Length;
		int width = input.Max(line => line.Length);
		for (int i = 0; i < input.Length; i++)
		{
			int lineLengthDiff = width - input[i].Length;
			if (lineLengthDiff > 0)
			{
				input[i] += new string(' ', lineLengthDiff);
			}
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

	public static bool ExtractCartsFromMap(TrackSymbol[,] map, [NotNullWhen(true)] out List<Cart>? carts)
	{
		carts = default;
		for (int y = 0; y < map.GetLength(1); y++)
		{
			for (int x = 0; x < map.GetLength(0); x++)
			{
				Coordinate position = new(x, y);
				TrackSymbol symbol = map[x, y];
				bool isCart = Cart.TryCreateFromTrackSymbol(position, symbol, out Cart cart);
				if (isCart)
				{
					if (carts is null)
					{
						carts = new List<Cart>();
					}
					carts.Add(cart);
					map[x, y] = cart.SymbolUnderneath;
				}
			}
		}
		return carts is not null;
	}
}
