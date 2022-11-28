using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22.Cave;

class CaveSystem
{
	private readonly RegionType[,] _regions;

	public uint Depth { get; }
	public Coordinate TargetCoordinate { get; }
	public Area Area { get; }

	public CaveSystem(uint depth, Coordinate targetCoordinate, RegionType[,] regions)
	{
		Area = new Area(new(0, regions.GetLength(0) - 1), new(0, regions.GetLength(1) - 1));
		if (!Area.Contains(targetCoordinate))
		{
			throw new ArgumentException("The target coordinate is not within the area of regions array.", nameof(targetCoordinate));
		}
		Depth = depth;
		TargetCoordinate = targetCoordinate;
		_regions = (RegionType[,])regions.Clone();
	}

	public RegionType this[int x, int y] => _regions[x, y];
	public RegionType this[Coordinate coordinate] => this[coordinate.X, coordinate.Y];

	public IEnumerable<RegionType> EnumerateRegions()
		=> Area.Points.Select(coordinate => this[coordinate]);

	public IEnumerable<RegionType> EnumerateRegions(Area subArea)
	{
		if (!Area.Contains(subArea))
		{
			throw new ArgumentException("The area is not within the cave system.", nameof(subArea));
		}
		return subArea.Points.Select(coordinate => this[coordinate]);
	}

	public IEnumerable<Coordinate> EnumerateNeighbors(Coordinate coordinate)
		=> coordinate.EnumerateAdjacent().Where(c => Area.Contains(c));
}
