namespace AdventOfCode.Year2018.Day15.Map;

public class Wall : MapSpot
{
	public override MapSpotType Type => MapSpotType.Wall;

	public override string ToString() => "Wall";
}
