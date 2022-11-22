using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Year2018.Day25.SpanExtensions;

namespace AdventOfCode.Year2018.Day25.Geometry;

readonly partial struct Point : IParsable<Point>
{
	public static Point Parse(string s, IFormatProvider? provider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), provider);
	}

	public static Point Parse(ReadOnlySpan<char> span, IFormatProvider? provider = null)
	{
		try
		{
			ReadOnlySpan<char> trimmedSpan = span.TrimStart().TrimStart('(').TrimEnd(')').TrimEnd();
			Span<int> coords = stackalloc int[4];
			SpanSplitEnumerator<char> splitter = trimmedSpan.Split(',');
			int i = 0;
			while (splitter.MoveNext())
			{
				if (i >= 4) throw new FormatException($"Too many coordinates, needed 4, found at least {i}.");
				coords[i++] = int.Parse(splitter.Current, provider);
			}
			if (i < 4) throw new FormatException("Too few coordinates, needed 4, found {i}.");
			return new Point(coords[0], coords[1], coords[2], coords[3]);
		}
		catch (Exception e) when (e is FormatException or OverflowException)
		{
			throw new FormatException($"Could not parse \"{span}\" as a Point.", e);
		}
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Point result)
	{
		ArgumentNullException.ThrowIfNull(s);
		return TryParse(s.AsSpan(), provider, out result);
	}

	public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Point result)
		=> TryParse(s, null, out result);

	public static bool TryParse(ReadOnlySpan<char> span, IFormatProvider? provider, [MaybeNullWhen(false)] out Point result)
	{
		ReadOnlySpan<char> trimmedSpan = span.TrimStart().TrimStart('(').TrimEnd(')').TrimEnd();
		Span<int> coords = stackalloc int[4];
		SpanSplitEnumerator<char> splitter = trimmedSpan.Split(',');
		int i = 0;
		while (splitter.MoveNext())
		{
			if (i >= 4)
			{
				result = default;
				return false;
			}
			if (!int.TryParse(splitter.Current, provider, out int parsed))
			{
				result = default;
				return false;
			}
			coords[i++] = parsed;
		}
		if (i < 4)
		{
			result = default;
			return false;
		}
		result = new Point(coords[0], coords[1], coords[2], coords[3]);
		return true;
	}

	public static bool TryParse(ReadOnlySpan<char> span, [MaybeNullWhen(false)] out Point result)
		=> TryParse(span, null, out result);
}
