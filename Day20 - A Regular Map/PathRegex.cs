namespace AdventOfCode.Year2018.Day20;

public class PathRegex
{
	private readonly string _regex;

	public PathRegex(string regex)
	{
		_regex = regex;
		CharEnumerator it = regex.GetEnumerator();
		if (!it.MoveNext())
		{
			throw new ArgumentException("Empty regex.");
		}
		if (it.Current != '^')
		{
			throw new ArgumentException("Only `^` bound regex supported.");
		}
		Stack<IPathPart> startedPaths = new();
		startedPaths.Push(new ComplexPath());
		while (it.MoveNext())
		{
			char c = it.Current;
			if (DirectionHelpers.TryParse(c, out Direction nextDirection))
			{
				if (startedPaths.Peek() is SimplePath simplePath)
				{
					simplePath.Add(nextDirection);
				}
				else
				{
					startedPaths.Push(new SimplePath(nextDirection));
				}
				continue;
			}
			else if (c is (char)PathChars.GroupStart)
			{
				startedPaths.Push(new ComplexPath());
			}
			else if (c is (char)PathChars.GroupEnd)
			{
				Stack<IPathPart> subPaths = new();
				while (startedPaths.TryPop(out IPathPart? pathPart))
				{
					if (pathPart is ComplexPath complexPath)
					{
						complexPath.Add(subPaths);
						break;
					}
					subPaths.Push(pathPart);
				}
			}
			else if (c is (char)PathChars.BranchSeparator)
			{
				IPathPart path = startedPaths.Pop();
				if (startedPaths.Count == 0)
				{
					throw new ArgumentException("Unbalanced `|`.");
				}
				if (startedPaths.Peek() is ComplexPath complexPath)
				{
					complexPath.Add(path);
				}
				else
				{
					throw new ArgumentException("Unexpected `|`.");
				}
				startedPaths.Push(new ComplexPath());
			}
			else
			{
				throw new ArgumentException($"Unexpected character: '{c}'");
			}
		}
	}
}

public enum PathChars
{
	GroupStart = '(',
	GroupEnd = ')',
	BranchSeparator = '|',
}

public interface IPathPart
{
	public bool Started { get; }
	public bool Finished { get; }
	public void Finish();
}

public class ComplexPath : IPathPart
{
	private readonly List<IPathPart> _parts = new();

	public IReadOnlyList<IPathPart> Parts => _parts;
	public int PartCount => _parts.Count;
	public IPathPart LastPart => _parts.Count > 0 ? _parts[^1] : throw new InvalidOperationException("There is no last part.");
	public bool Started => _parts.Count > 0 && _parts[0].Started;
	public bool Finished { get; private set; } = false;

	public void Add(IPathPart path)
	{
		ArgumentNullException.ThrowIfNull(path);
		if (Finished)
		{
			throw new InvalidOperationException("This path is already marked as finished.");
		}
		if (!path.Finished)
		{
			throw new ArgumentException("Added path part is not finished.");
		}
		_parts.Add(path);
	}

	public void Add(IEnumerable<IPathPart> paths)
	{
		ArgumentNullException.ThrowIfNull(paths);
		foreach (IPathPart path in paths)
		{
			Add(path);
		}
	}

	public void Finish()
	{
		Finished = true;
	}

	public override string ToString() => string.Concat(_parts);
}

public class BranchingPath : IPathPart
{
	private readonly List<IPathPart> _possiblePaths = new();

	public IReadOnlyList<IPathPart> PossiblePaths => _possiblePaths;
	public bool Started => _possiblePaths.Count > 0;
	public bool Finished { get; private set; } = false;

	public void Add(IPathPart path)
	{
		ArgumentNullException.ThrowIfNull(path);
		if (Finished)
		{
			throw new InvalidOperationException("This path is already marked as finished.");
		}
		if (!path.Finished)
		{
			throw new ArgumentException("Added path is not finished.");
		}
		_possiblePaths.Add(path);
	}

	public void Finish()
	{
		_possiblePaths.TrimExcess();
		Finished = true;
	}

	public override string ToString() => $"({string.Join('|', _possiblePaths)})";
}

public class SimplePath : IPathPart
{
	private readonly List<Direction> _directions = new();

	public IReadOnlyList<Direction> Directions => _directions;
	public bool Started => _directions.Count > 0;
	public bool Finished { get; private set; } = false;

	public SimplePath()
	{
	}

	public SimplePath(Direction startingDirection)
	{
		_directions.Add(startingDirection);
	}

	public void Add(Direction direction)
	{
		if (!DirectionHelpers.IsDefined(direction))
		{
			throw new ArgumentException($"Invalid direction: '{direction}'");
		}
		if (Finished)
		{
			throw new InvalidOperationException("Cannot add to finished path.");
		}
		_directions.Add(direction);
	}

	public void Finish()
	{
		_directions.TrimExcess();
		Finished = true;
	}

	public override string ToString() => string.Concat(_directions.Select(d => d.ToChar()));
}
// ^ENWWW(NEEE|SSE(EE|N))$
// ENWWW
// NEEE
// SEE
// EE
// N

