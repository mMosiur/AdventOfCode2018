using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

sealed class NanobotFormationAnalyzer
{
	private readonly Nanobot[] _nanobots;
	private readonly Point _origin;

	public NanobotFormationAnalyzer(IEnumerable<Nanobot> nanobots, Point origin)
	{
		_origin = origin;
		_nanobots = nanobots.ToArray();
	}

	public Nanobot FindStrongestNanobot()
	{
		Nanobot? strongestNanobot = _nanobots.MaxBy(n => n.Radius);
		return strongestNanobot ?? throw new InvalidOperationException("No nanobots in formation.");
	}

	public IEnumerable<Nanobot> NanobotsInRangeOf(Nanobot nanobot)
	{
		ArgumentNullException.ThrowIfNull(nanobot);
		return _nanobots.Where(n => nanobot.IsInRange(n));
	}

	public static bool CanNanobotReachCuboid(Nanobot nanobot, Cuboid cuboid)
	{
		Point closestPoint = cuboid.GetPointClosestToExternalPoint(nanobot.Position);
		int closestPointDistance = MathG.ManhattanDistance(nanobot.Position, closestPoint);
		return closestPointDistance <= nanobot.Radius;
	}

	public IEnumerable<Nanobot> NanobotsThatCanReachCuboid(Cuboid cuboid)
	{
		return _nanobots.Where(n => CanNanobotReachCuboid(n, cuboid));
	}

	public Cuboid GenerateBoundingCuboid()
	{
		Range xRange = new(_nanobots.Min(n => n.Position.X), _nanobots.Max(n => n.Position.X));
		Range yRange = new(_nanobots.Min(n => n.Position.Y), _nanobots.Max(n => n.Position.Y));
		Range zRange = new(_nanobots.Min(n => n.Position.Z), _nanobots.Max(n => n.Position.Z));
		return new Cuboid(xRange, yRange, zRange);
	}

	public (int NanobotsInRangeCount, Point Point) FindPointInRangeOfMostNanobots()
	{
		CuboidPriorityQueue queue = new(_origin);
		Cuboid boundingCuboid = GenerateBoundingCuboid();
		queue.Enqueue(NanobotsThatCanReachCuboid(boundingCuboid).Count(), boundingCuboid);
		while (queue.TryDequeue(out int nanobotsInRangeCount, out Cuboid cuboid))
		{
			if (cuboid.Volume == 1)
			{
				return (nanobotsInRangeCount, cuboid.Points.Single());
			}
			foreach (Cuboid newCuboid in CuboidSplitter.Split(cuboid))
			{
				int newNanobotsInRangeCount = NanobotsThatCanReachCuboid(newCuboid).Count();
				queue.Enqueue(newNanobotsInRangeCount, newCuboid);
			}
		}
		throw new DaySolverException("No single point found in range of most nanobots.");
	}
}
