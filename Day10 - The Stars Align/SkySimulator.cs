namespace AdventOfCode.Year2018.Day10;

class SkySimulator
{
	private readonly Sky _sky;
	private readonly int _maxAreaToDisplay;
	private string? _smallestSkyRepresentation;
	private int? _secondsToSmallestSky;

	public int SecondsPassed => _sky.SecondsPassed;
	public string SmallestSkyRepresentation => _smallestSkyRepresentation
		?? throw new DaySolverException("The simulation has not been run yet");
	public int SecondsToSmallestSky => _secondsToSmallestSky
		?? throw new DaySolverException("The simulation has not been run yet");

	public SkySimulator(ICollection<SkyPoint> points, Day10SolverOptions options)
	{
		_sky = new Sky(points, options);
		_maxAreaToDisplay = options.MaxSkyAreToDisplay;
		_smallestSkyRepresentation = null;
	}

	/// <summary>
	/// Simulates the sky until the smallest sky representation is found.
	/// </summary>
	/// <returns>The time in seconds it took to get to the smallest sky.</returns>
	public int SimulateTillSmallestSkyFound()
	{
		if (_smallestSkyRepresentation is not null)
		{
			return 0;
		}
		Rectangle boundingBox = _sky.GetBoundingBox();
		Rectangle nextBoundingBox = boundingBox;
		string? currentRepresentation = null;
		int secondsPassed = -1;
		while (nextBoundingBox.GetWidth() <= boundingBox.GetWidth() || nextBoundingBox.GetHeight() <= boundingBox.GetHeight())
		{
			secondsPassed++;
			currentRepresentation = _sky.GetRepresentation(_maxAreaToDisplay);
			_sky.SimulateSecond();
			boundingBox = nextBoundingBox;
			nextBoundingBox = _sky.GetBoundingBox();
		}
		_smallestSkyRepresentation = currentRepresentation
			?? throw new DaySolverException("The converged representation was too large to display");
		_secondsToSmallestSky = secondsPassed;
		return secondsPassed;
	}
}
