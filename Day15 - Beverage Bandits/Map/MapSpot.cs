using AdventOfCode.Year2018.Day15.Map.Units;

namespace AdventOfCode.Year2018.Day15.Map;

public abstract class MapSpot
{
	public abstract MapSpotType Type { get; }

	public static MapSpot? NewFromType(MapSpotType type, Coordinate coordinate) => type switch
	{
		MapSpotType.OpenCavern => null,
		MapSpotType.Wall => new Wall(),
		MapSpotType.Goblin => new GoblinUnit() { Position = coordinate },
		MapSpotType.Elf => new ElfUnit() { Position = coordinate },
		_ => throw new ArgumentException($"Unknown map spot type: '{type}'.", nameof(type)),
	};
}