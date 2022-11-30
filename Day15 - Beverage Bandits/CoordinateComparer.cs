namespace AdventOfCode.Year2018.Day15;

readonly struct CoordinateComparer : IComparer<Coordinate>
{
	public int Compare(Coordinate coord1, Coordinate coord2)
	{
		int result = coord1.X.CompareTo(coord2.X);
		if (result != 0) return result;
		return coord1.Y.CompareTo(coord2.Y);
	}
}
