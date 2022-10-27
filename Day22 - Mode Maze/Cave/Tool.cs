namespace AdventOfCode.Year2018.Day22.Cave;

enum Tool : byte
{
	Neither = 0,
	Torch = 1,
	ClimbingGear = 2
}

static class ToolHelpers
{
	public static ICollection<Tool> AllTools { get; } = new[] { Tool.Neither, Tool.Torch, Tool.ClimbingGear };

	public static Tool FromChar(char c) => c switch
	{
		'N' => Tool.Neither,
		'T' => Tool.Torch,
		'C' => Tool.ClimbingGear,
		_ => throw new ArgumentException("Invalid tool character.", nameof(c))
	};

	public static bool CanBeUsedInRegion(this Tool tool, RegionType regionType) => regionType switch
	{
		RegionType.Rocky => tool is Tool.ClimbingGear or Tool.Torch,
		RegionType.Wet => tool is Tool.ClimbingGear or Tool.Neither,
		RegionType.Narrow => tool is Tool.Torch or Tool.Neither,
		_ => throw new ArgumentOutOfRangeException(nameof(regionType), regionType, null)
	};
}
