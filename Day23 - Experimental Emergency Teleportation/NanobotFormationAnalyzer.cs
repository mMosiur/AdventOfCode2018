namespace AdventOfCode.Year2018.Day23;

sealed class NanobotFormationAnalyzer
{
	private readonly Nanobot[] _nanobots;

	public IReadOnlyCollection<Nanobot> Nanobots => _nanobots;

	public NanobotFormationAnalyzer(IEnumerable<Nanobot> nanobots)
	{
		_nanobots = nanobots.ToArray();
	}

	public Nanobot GetStrongestNanobot()
		=> _nanobots.MaxBy(n => n.Radius) ?? throw new DaySolverException("No nanobots.");

	public IEnumerable<Nanobot> NanobotsInRangeOfStrongestNanobot()
	{
		Nanobot strongestNanobot = GetStrongestNanobot();
		return _nanobots.Where(n => strongestNanobot.IsInRange(n));
	}
}
