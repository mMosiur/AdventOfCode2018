namespace AdventOfCode.Year2018.Day22.Geometry;

static class AreaExtensions
{
	public static int GetWidth(this Area area)
	{
		return area.XRange.Count;
	}

	public static int GetHeight(this Area area)
	{
		return area.YRange.Count;
	}
}
