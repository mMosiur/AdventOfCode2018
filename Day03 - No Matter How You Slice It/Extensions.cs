namespace AdventOfCode.Year2018.Day03;

public static class Extensions
{
	public static IEnumerable<Point> GetPoints(this ElfClaim claim)
	{
		return Enumerable.Range(claim.Left, claim.Width)
			.SelectMany(x =>
				Enumerable
					.Range(claim.Top, claim.Height)
					.Select(y => new Point(x, y))
			);
	}
}
