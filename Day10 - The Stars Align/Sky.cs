using System.Text;
using AdventOfCode.Common.Numerics;

namespace AdventOfCode.Year2018.Day10;

class Sky
{
	private readonly ICollection<SkyPoint> _points;
	private readonly char _emptySkyRepresentation;
	private readonly char _starInSkyRepresentation;
	public int SecondsPassed { get; private set; }

	public Sky(ICollection<SkyPoint> points, Day10SolverOptions options)
	{
		_points = points;
		_emptySkyRepresentation = options.EmptySkyRepresentation;
		_starInSkyRepresentation = options.StarInSkyRepresentation;
		SecondsPassed = 0;
	}

	public void SimulateSecond()
	{
		foreach (SkyPoint point in _points)
		{
			point.Move();
		}
		SecondsPassed++;
	}

	public Rectangle GetBoundingBox()
	{
		int minX = int.MaxValue;
		int minY = int.MaxValue;
		int maxX = int.MinValue;
		int maxY = int.MinValue;
		foreach (SkyPoint point in _points)
		{
			minX = Math.Min(minX, point.Position.X);
			minY = Math.Min(minY, point.Position.Y);
			maxX = Math.Max(maxX, point.Position.X);
			maxY = Math.Max(maxY, point.Position.Y);
		}
		Interval<int> xRange = new(minX, maxX);
		Interval<int> yRange = new(minY, maxY);
		return new(xRange, yRange);
	}

	public string? GetRepresentation(int maxArea)
	{
		Rectangle boundingBox = GetBoundingBox();
		long boundingBoxArea = Math.BigMul(boundingBox.GetWidth(), boundingBox.GetHeight());
		if (boundingBoxArea > maxArea)
		{
			return null;
		}
		bool[,] stars = new bool[boundingBox.GetWidth(), boundingBox.GetHeight()];
		foreach (SkyPoint point in _points)
		{
			int x = point.Position.X - boundingBox.XRange.Start;
			int y = point.Position.Y - boundingBox.YRange.Start;
			stars[x, y] = true;
		}
		StringBuilder builder = new(boundingBox.GetHeight() * (boundingBox.GetWidth() + Environment.NewLine.Length));
		for (int y = 0; y < boundingBox.GetHeight(); y++)
		{
			for (int x = 0; x < boundingBox.GetWidth(); x++)
			{
				bool hasStart = stars[x, y];
				builder.Append(hasStart ? _starInSkyRepresentation : _emptySkyRepresentation);
			}
			builder.AppendLine();
		}
		builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
		return builder.ToString();
	}
}
