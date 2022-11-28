namespace AdventOfCode.Year2018.Day10;

static class RectangleExtensions
{
	public static int GetWidth(this Rectangle rectangle)
	{
		return rectangle.XRange.Count;
	}

	public static int GetHeight(this Rectangle rectangle)
	{
		return rectangle.YRange.Count;
	}
}
