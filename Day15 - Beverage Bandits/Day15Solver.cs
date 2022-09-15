using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day15.Combat;
using AdventOfCode.Year2018.Day15.Exceptions;
using AdventOfCode.Year2018.Day15.Map;

namespace AdventOfCode.Year2018.Day15;

public class Day15Solver : DaySolver
{
	private readonly MapSpotType[,] _rawMap;

	public Day15Solver(Day15SolverOptions options) : base(options)
	{
		string[] lines = InputLines.ToArray();
		if (!lines.All(l => l.Length == lines[0].Length))
		{
			throw new ArgumentException("Input map is not rectangular.");
		}
		MapSpotType[,] rawMap = new MapSpotType[lines.Length, lines[0].Length];
		for (int x = 0; x < lines.Length; x++)
		{
			for (int y = 0; y < lines[0].Length; y++)
			{
				MapSpotType mapSpotType = (MapSpotType)lines[x][y];
				if (!Enum.IsDefined(typeof(MapSpotType), mapSpotType))
				{
					throw new ArgumentException($"Invalid map spot type '{mapSpotType}' at position ({x}, {y}).");
				}
				rawMap[x, y] = mapSpotType;
			}
		}
		_rawMap = rawMap;
	}

	public Day15Solver(Action<Day15SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		CombatSimulator simulator = new(_rawMap);
		int outcome = simulator.SimulateCombat();
		return outcome.ToString();
	}

	public override string SolvePart2()
	{
		int elfAttackPower = 4;
		bool elvesWonCombat = false;
		int outcome = default;
		while (!elvesWonCombat)
		{
			CombatSimulator simulator = new(_rawMap, true, elfAttackPower);
			try
			{
				outcome = simulator.SimulateCombat();
				elvesWonCombat = true;
			}
			catch (ElfKilledException)
			{
				elvesWonCombat = false;
				elfAttackPower++;
				continue;
			}
		}
		return outcome.ToString();
	}
}
