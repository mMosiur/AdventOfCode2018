namespace AdventOfCode.Year2018.Day17.Geometry;

interface ILine
{
	public IEnumerable<Point> Points { get; }
	public bool Contains(Point point);
}
