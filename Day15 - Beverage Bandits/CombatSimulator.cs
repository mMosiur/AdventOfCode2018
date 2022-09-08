using AdventOfCode.Year2018.Day15.Map;
using AdventOfCode.Year2018.Day15.Map.Units;

namespace AdventOfCode.Year2018.Day15;

public class CombatSimulator
{
	private readonly MapSpotType[,] _rawMap;
	private CombatMap _combatMap;

	public int FullRoundsSimulated { get; private set; }

	public bool CombatFinished
	{
		get
		{
			int goblinCount = _combatMap.PositionsOfType(MapSpotType.Goblin).Count();
			int elfCount = _combatMap.PositionsOfType(MapSpotType.Elf).Count();
			return goblinCount == 0 || elfCount == 0;
		}
	}

	public CombatSimulator(MapSpotType[,] rawMap)
	{
		_rawMap = rawMap;
		_combatMap = CombatMap.FromRawMap(_rawMap);
		FullRoundsSimulated = 0;
	}

	public int SimulateCombat()
	{
		if (FullRoundsSimulated > 0)
		{
			throw new InvalidOperationException("Cannot simulate battle after a round has already been simulated.");
		}
		while (SimulateSingleRound()) { }
		checked
		{
			int hitPointSum = _combatMap.EnumerateUnits()
				.Sum(u => u.HitPoints);
			int outcome = FullRoundsSimulated * hitPointSum;
			return outcome;
		}
	}

	private bool SimulateSingleRound()
	{
		IReadOnlyCollection<Unit> units = _combatMap.EnumerateUnits().ToList();
		foreach (Unit unit in units)
		{
			if (unit.IsDead) continue; // Unit may have been killed during the round
			MapSpotType enemyType = unit.Type switch
			{
				MapSpotType.Elf => MapSpotType.Goblin,
				MapSpotType.Goblin => MapSpotType.Elf,
				_ => throw new InvalidOperationException($"Invalid unit type '{unit.Type}'.")
			};
			IEnumerable<Coordinate> inRangeEnemyPositions = _combatMap.AdjacentOfType(unit.Position, enemyType);
			if (!inRangeEnemyPositions.Any())
			{
				// No enemies in range, try to move towards one.
				IEnumerable<Coordinate> enemyPositions = _combatMap.PositionsOfType(enemyType);
				if (!enemyPositions.Any())
				{
					return false; // No enemies left, combat is over.
				}
				IEnumerable<Coordinate> moveTargetPositions = enemyPositions.SelectMany(p => _combatMap.AdjacentEmpty(p));
				Path? path = ChoosePath(_combatMap, unit.Position, moveTargetPositions);
				if (path is not null)
				{
					_combatMap.MoveUnit(unit, path.Trail[1]);
				}
			}
			inRangeEnemyPositions = _combatMap.AdjacentOfType(unit.Position, enemyType);
			// Check for enemies in range again, in case we moved.
			if (inRangeEnemyPositions.Any())
			{
				// Enemies in range found, attack
				int minHitPoints = inRangeEnemyPositions.Select(p => (Unit)_combatMap[p]!).Min(u => u.HitPoints);
				Coordinate attackTargetPosition = inRangeEnemyPositions.Where(p => ((Unit)_combatMap[p]!).HitPoints == minHitPoints).OrderBy(p => p).First();
				Unit targetUnit = _combatMap[attackTargetPosition] as Unit ?? throw new InvalidOperationException("No unit at attack target position.");
				AttackResult attackResult = unit.Attack(targetUnit);
				if (attackResult.TargetKilled)
				{
					_combatMap.DeleteUnit(attackTargetPosition);
				}
			}
		}
		FullRoundsSimulated++;
		return true;
	}

	private Path? ChoosePath(CombatMap map, Coordinate position, IEnumerable<Coordinate> targets)
	{
		PathGenerator pathGenerator = new(map);
		return pathGenerator.GenerateShortestPaths(position)
			.Where(kvp => targets.Contains(kvp.Key))
			.Select(kvp => kvp.Value)
			.OrderBy(path => path)
			.FirstOrDefault();
	}

	public void ResetCombat()
	{
		_combatMap = CombatMap.FromRawMap(_rawMap);
		FullRoundsSimulated = 0;
	}

	public void PrintMap(Coordinate? highlightPosition = null)
	{
		_combatMap.Print(highlightPosition);
	}
}
