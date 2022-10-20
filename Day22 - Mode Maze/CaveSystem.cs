using System.Text;
using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22;

public class CaveSystem
{
	private readonly uint _depth;
	private readonly Coordinate _targetCoordinate;
	private readonly RegionType[,] _regions;

	public int Width { get; }
	public int Height { get; }

	public uint Depth => _depth;

	public CaveSystem(uint depth, Coordinate targetCoordinate, RegionType[,] regions)
	{
		_depth = depth;
		_targetCoordinate = targetCoordinate;
		_regions = regions;
		Width = targetCoordinate.X + 1;
		if (Width != regions.GetLength(0))
		{
			throw new ArgumentException("The width of the regions array does not match the width of the cave system.", nameof(regions));
		}
		Height = targetCoordinate.Y + 1;
		if (Height != regions.GetLength(1))
		{
			throw new ArgumentException("The height of the regions array does not match the height of the cave system.", nameof(regions));
		}
	}

	public RegionType this[int x, int y] => _regions[x, y];

	public RegionType this[Coordinate coordinate] => this[coordinate.X, coordinate.Y];

	public IEnumerable<RegionType> EnumerateRegions() => _regions.Cast<RegionType>();

	public override string ToString()
	{
		StringBuilder builder = new();
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				char regionCharacter = new Coordinate(x, y) switch
				{
					{ X: 0, Y: 0 } => 'M',
					Coordinate targetCoordinate when targetCoordinate == _targetCoordinate => 'T',
					_ => this[x, y].ToChar()
				};
				builder.Append(regionCharacter);
			}
			builder.AppendLine();
		}
		builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
		return builder.ToString();
	}
}
