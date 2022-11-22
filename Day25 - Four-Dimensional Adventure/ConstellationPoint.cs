using AdventOfCode.Year2018.Day25.Geometry;

namespace AdventOfCode.Year2018.Day25;

class ConstellationPoint
{
	public Point Point { get; }
	public ConstellationPoint? Previous { get; set; }
	public Constellation Constellation { get; set; }
	public ConstellationPoint? Next { get; set; }

	public ConstellationPoint(Point point)
	{
		Point = point;
		Constellation = new(this);
	}
}
