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
	{
		Nanobot? strongestNanobot = _nanobots.MaxBy(n => n.Radius);
		if (strongestNanobot is null)
		{
			throw new InvalidOperationException("No nanobots in formation.");
		}
		return strongestNanobot;
	}

	public IEnumerable<Nanobot> NanobotsInRangeOf(Nanobot nanobot)
	{
		ArgumentNullException.ThrowIfNull(nanobot);
		return _nanobots.Where(n => nanobot.IsInRange(n));
	}
}
