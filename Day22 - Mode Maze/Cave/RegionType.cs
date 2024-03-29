namespace AdventOfCode.Year2018.Day22.Cave;

enum RegionType : byte
{
	Rocky = 1,
	Wet = 2,
	Narrow = 3,
}

static class RegionTypeExtensions
{
	public static char ToChar(this RegionType regionType) => regionType switch
	{
		RegionType.Rocky => '.',
		RegionType.Wet => '=',
		RegionType.Narrow => '|',
		_ => throw new InvalidOperationException($"Unexpected region type '{regionType}'."),
	};
}
