using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22.Cave;

readonly record struct CaveTravelState(Coordinate Coordinate, Tool SelectedTool) : IComparable<CaveTravelState>
{
	public int CompareTo(CaveTravelState other)
	{
		int result = Coordinate.X.CompareTo(other.Coordinate.X);
		if (result != 0)
		{
			return result;
		}
		result = Coordinate.Y.CompareTo(other.Coordinate.Y);
		if (result != 0)
		{
			return result;
		}
		return SelectedTool.ToString()[0].CompareTo(other.SelectedTool.ToString()[0]);
	}

	public string ToDebugString(uint time)
	{
		return $"{Coordinate.X} {Coordinate.Y} {SelectedTool.ToString()[0]} {time}";
	}
}
