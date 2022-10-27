using AdventOfCode.Year2018.Day18.Geometry;

namespace AdventOfCode.Year2018.Day18.LumberCollection;

abstract class LumberCollectionAreaSimulator
{
	private LumberCollectionArea _nextArea;
	public LumberCollectionArea Area { get; private set; }

	public LumberCollectionAreaSimulator(LumberCollectionArea area)
	{
		Area = (LumberCollectionArea)area.Clone();
		_nextArea = (LumberCollectionArea)area.Clone();
	}

	private static AcreContent NextAcreContentFor(AcreContent content, IEnumerable<AcreContent> neighborContents)
	{
		return content switch
		{
			AcreContent.OpenGround => neighborContents.Count(c => c is AcreContent.Trees) >= 3 ? AcreContent.Trees : AcreContent.OpenGround,
			AcreContent.Trees => neighborContents.Count(c => c is AcreContent.Lumberyard) >= 3 ? AcreContent.Lumberyard : AcreContent.Trees,
			AcreContent.Lumberyard => neighborContents.Any(c => c is AcreContent.Lumberyard) && neighborContents.Any(c => c == AcreContent.Trees) ? AcreContent.Lumberyard : AcreContent.OpenGround,
			_ => throw new ArgumentOutOfRangeException(nameof(content), "Invalid acre content."),
		};
	}

	public void SimulateMinute()
	{
		foreach (Point point in Area.EnumeratePoints())
		{
			_nextArea[point.Y, point.X] = NextAcreContentFor(
				Area[point.Y, point.X],
				Area.EnumerateNeighborPoints(point).Select(p => Area[p])
			);
		}
		(_nextArea, Area) = (Area, _nextArea);
	}

	public abstract void Simulate(int minutes);
}
