using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

sealed class NanobotFormationAnalyzer
{
	private readonly Nanobot[] _nanobots;

	public IReadOnlyCollection<Nanobot> Nanobots => _nanobots;

	public NanobotFormationAnalyzer(IEnumerable<Nanobot> nanobots)
	{
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
		int x = nanobot.Position.X;
		if (x < cuboid.XRange.Start) x = cuboid.XRange.Start;
		else if (x > cuboid.XRange.End) x = cuboid.XRange.End;

		int y = nanobot.Position.Y;
		if (y < cuboid.YRange.Start) y = cuboid.YRange.Start;
		else if (y > cuboid.YRange.End) y = cuboid.YRange.End;

		int z = nanobot.Position.Z;
		if (z < cuboid.ZRange.Start) z = cuboid.ZRange.Start;
		else if (z > cuboid.ZRange.End) z = cuboid.ZRange.End;

		Point cuboidClosestPoint = new(x, y, z);
		int cuboidClosestPointDistance = ExtendedMath.ManhattanDistance(nanobot.Position, cuboidClosestPoint);
		return cuboidClosestPointDistance <= nanobot.Radius;
	}

	public int CountNanobotsThatCanReach(Cuboid cuboid)
	{
		return _nanobots.Count(n => CanNanobotReachCuboid(n, cuboid));
	}

	public Cuboid GenerateBoundingCuboid()
	{
		Geometry.Range xRange = new(_nanobots.Min(n => n.Position.X), _nanobots.Max(n => n.Position.X));
		Geometry.Range yRange = new(_nanobots.Min(n => n.Position.Y), _nanobots.Max(n => n.Position.Y));
		Geometry.Range zRange = new(_nanobots.Min(n => n.Position.Z), _nanobots.Max(n => n.Position.Z));
		return new Cuboid(xRange, yRange, zRange);
	}

	public (int NanobotsInRangeCount, Point Point) FindPointInRangeOfMostNanobots()
	{
		CuboidPriorityQueue queue = new();
		Cuboid boundingCuboid = GenerateBoundingCuboid();
		queue.Enqueue(CountNanobotsThatCanReach(boundingCuboid), boundingCuboid);
		while (queue.TryDequeue(out int nanobotsInRangeCount, out Cuboid cuboid))
		{
			if (cuboid.Volume == 1)
			{
				return (nanobotsInRangeCount, cuboid.Points.Single());
			}
			foreach (Cuboid newCuboid in CuboidSplitter.Split(cuboid))
			{
				int newNanobotsInRangeCount = CountNanobotsThatCanReach(newCuboid);
				queue.Enqueue(newNanobotsInRangeCount, newCuboid);
			}
		}
		throw new DaySolverException("No single point found in range of most nanobots.");
	}
}
