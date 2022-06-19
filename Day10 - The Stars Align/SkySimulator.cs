using AdventOfCode.Year2018.Day10.Geometry;

namespace AdventOfCode.Year2018.Day10;

public class SkySimulator
{
	private readonly Sky _sky;
	private readonly int _maxAreaToDisplay;
	private string? _smallestSkyRepresentation;

	public string SmallestSkyRepresentation => _smallestSkyRepresentation
			?? throw new ApplicationException("The simulation has not been run yet");

	public SkySimulator(ICollection<SkyPoint> points, Day10SolverOptions options)
	{
		_sky = new Sky(points, options);
		_maxAreaToDisplay = options.MaxSkyAreToDisplay;
		_smallestSkyRepresentation = null;
	}

	public void Simulate()
	{
		Rectangle boundingBox = _sky.GetBoundingBox();
		Rectangle nextBoundingBox = boundingBox;
		string? currentRepresentation = null;
		while (nextBoundingBox.Width <= boundingBox.Width || nextBoundingBox.Height <= boundingBox.Height)
		{
			currentRepresentation = _sky.GetRepresentation(_maxAreaToDisplay);
			_sky.SimulateSecond();
			boundingBox = nextBoundingBox;
			nextBoundingBox = _sky.GetBoundingBox();
		}
		_smallestSkyRepresentation = currentRepresentation
			?? throw new ApplicationException("The converged representation was too large to display");
	}
}
