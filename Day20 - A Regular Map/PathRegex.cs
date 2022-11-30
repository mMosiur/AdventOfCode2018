namespace AdventOfCode.Year2018.Day20;

class PathRegex
{
	private readonly string _regex;

	private ReadOnlySpan<char> MeaningfulRegexPart => _regex.AsSpan()[1..^1];

	public PathRegex(string regex)
	{
		ReadOnlyMemory<char> c = regex.AsMemory();
		ArgumentNullException.ThrowIfNull(regex);
		_regex = regex.Trim();
		try
		{
			AssertRegexFormat(_regex);
		}
		catch (SystemException e)
		{
			throw new ArgumentException("Invalid regex syntax.", nameof(regex), e);
		}
	}

	private static void AssertRegexFormat(ReadOnlySpan<char> regex)
	{
		if (regex.Length < 2 || regex[0] != '^' || regex[^1] != '$')
		{
			throw new ArgumentException("Regex must start with '^' and end with '$'.", nameof(regex));
		}
		int openedGroups = 0;
		foreach (char c in regex[1..^1])
		{
			if (!PathCharacters.IsDefined(c))
			{
				throw new FormatException($"Invalid character '{c}'.");
			}
			PathChar pathChar = (PathChar)c;
			switch (pathChar)
			{
				case PathChar.GroupStart:
					openedGroups++;
					break;
				case PathChar.GroupEnd:
					if (openedGroups <= 0)
					{
						throw new FormatException($"Unmatched '{(char)PathChar.GroupEnd}'.");
					}
					openedGroups--;
					break;
				case PathChar.BranchSeparator:
					break;
				case PathChar.North or PathChar.South or PathChar.West or PathChar.East:
					break;
				default:
					throw new FormatException($"Invalid character '{c}'.");
			}
		}
		if (openedGroups != 0)
		{
			throw new FormatException($"Unmatched '{(char)PathChar.GroupStart}'.");
		}
	}

	public RoomDistances BuildRoomDistances()
	{
		Position currentRoomPosition = Position.Origin;
		Dictionary<Position, int> distances = new()
		{
			[currentRoomPosition] = 0
		};
		Stack<Position> activePositions = new();
		foreach (char c in MeaningfulRegexPart)
		{
			if (DirectionHelpers.TryParse(c, out Direction direction))
			{
				Position previousRoomPosition = currentRoomPosition;
				currentRoomPosition += direction.ToVector();
				int newDistance = distances[previousRoomPosition] + 1;
				if (distances.TryGetValue(currentRoomPosition, out int prevDistance))
				{
					distances[currentRoomPosition] = Math.Min(prevDistance, newDistance);
				}
				else
				{
					distances[currentRoomPosition] = distances[previousRoomPosition] + 1;
				}
				continue;
			}
			switch ((PathChar)c)
			{
				case PathChar.GroupStart:
					activePositions.Push(currentRoomPosition);
					break;
				case PathChar.GroupEnd:
					currentRoomPosition = activePositions.Pop();
					break;
				case PathChar.BranchSeparator:
					currentRoomPosition = activePositions.Peek();
					break;
				default: throw new FormatException($"Invalid character '{c}'.");
			}
		}
		if (activePositions.Count != 0)
		{
			throw new FormatException("Unmatched group start.");
		}
		return new RoomDistances(distances);
	}
}

enum PathChar : ushort
{
	North = 'N',
	South = 'S',
	West = 'W',
	East = 'E',
	BranchSeparator = '|',
	GroupStart = '(',
	GroupEnd = ')',
}

static class PathCharacters
{
	public static bool IsDefined(char c) => (PathChar)c is PathChar.North or PathChar.South or PathChar.West or PathChar.East or PathChar.BranchSeparator or PathChar.GroupStart or PathChar.GroupEnd;
	public static bool IsDirection(PathChar c) => c is PathChar.North or PathChar.South or PathChar.West or PathChar.East;
}
