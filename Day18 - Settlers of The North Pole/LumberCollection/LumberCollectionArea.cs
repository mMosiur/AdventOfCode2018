using System.Text;
using AdventOfCode.Year2018.Day18.Geometry;
using AdventOfCode.Year2018.Day18.StringExtensions;

namespace AdventOfCode.Year2018.Day18.LumberCollection;

public class LumberCollectionArea : ICloneable
{
	private readonly AcreContent[,] _area;

	public int Height { get; }
	public int Width { get; }

	public int ResourceValue
	{
		get
		{
			int trees = 0;
			int lumberyards = 0;
			foreach (AcreContent content in _area)
			{
				switch (content)
				{
					case AcreContent.Trees:
						trees++;
						break;
					case AcreContent.Lumberyard:
						lumberyards++;
						break;
				}
			}
			return trees * lumberyards;
		}
	}

	private LumberCollectionArea(AcreContent[,] area)
	{
		_area = area;
		Height = area.GetLength(0);
		Width = area.GetLength(1);
	}

	public AcreContent this[int y, int x]
	{
		get => _area[y, x];
		set => _area[y, x] = value;
	}

	public AcreContent this[Point point]
	{
		get => this[point.Y, point.X];
		set => this[point.Y, point.X] = value;
	}

	public IEnumerable<Point> EnumerateNeighborPoints(Point point)
	{
		if (!Contains(point))
		{
			throw new ArgumentOutOfRangeException(nameof(point), "Point is outside the area.");
		}
		return point.EnumerateNeighborPoints().Where(Contains);
	}

	public bool Contains(int y, int x) => y >= 0 && y < Height && x >= 0 && x < Width;
	public bool Contains(Point point) => Contains(point.Y, point.X);

	public object Clone()
	{
		return new LumberCollectionArea((AcreContent[,])_area.Clone());
	}

	public IEnumerable<Point> EnumeratePoints()
	{
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				yield return new Point(y, x);
			}
		}
	}

	public static LumberCollectionArea Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		string[] lines = s.EnumerateLines().ToArray();
		int height = lines.Length;
		if (height == 0)
		{
			throw new FormatException("No lines in input.");
		}
		int width = lines[0].Length;
		if (!lines.All(l => l.Length == width))
		{
			throw new FormatException("Not all lines have the same length.");
		}
		AcreContent[,] area = new AcreContent[height, width];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				try
				{
					area[y, x] = AcreContents.Parse(lines[y][x]);
				}
				catch (ArgumentException e)
				{
					throw new FormatException($"Invalid acre content at row {y}, column {x}.", e);
				}
			}
		}
		return new(area);
	}

	public override string ToString()
	{
		StringBuilder builder = new();
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				builder.Append(_area[y, x].ToChar());
			}
			builder.Append('\n');
		}
		builder.Remove(builder.Length - 1, 1);
		return builder.ToString();
	}
}
