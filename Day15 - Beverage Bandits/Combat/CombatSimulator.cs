using AdventOfCode.Year2018.Day15.Exceptions;
using AdventOfCode.Year2018.Day15.Map;
using AdventOfCode.Year2018.Day15.Map.Units;
using AdventOfCode.Year2018.Day15.Path;

namespace AdventOfCode.Year2018.Day15.Combat;

class CombatSimulator
{
	private readonly MapSpotType[,] _rawMap;
	private CombatMap _combatMap;

	public bool ThrowOnElfDeath { get; } = false;
	public int? CustomElfAttackPower { get; } = null;

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

	public CombatSimulator(MapSpotType[,] rawMap, bool throwOnElfDeath = false, int? customElfAttackPower = null)
	{
		_rawMap = rawMap;
		FullRoundsSimulated = 0;
		ThrowOnElfDeath = throwOnElfDeath;
		CustomElfAttackPower = customElfAttackPower;
		_combatMap = CombatMap.FromRawMap(_rawMap, CustomElfAttackPower);
	}

	public int SimulateCombat()
	{
		if (FullRoundsSimulated > 0)
		{
			throw new InvalidOperationException("Cannot simulate battle after a round has already been simulated.");
		}
		while (SimulateSingleRound()) { }
		int hitPointSum = _combatMap.EnumerateUnits().Sum(u => u.HitPoints);
		int combatOutcome = FullRoundsSimulated * hitPointSum;
		return combatOutcome;
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
				try
				{
					PathSignature path = ChoosePath(_combatMap, unit.Position, moveTargetPositions);
					_combatMap.MoveUnit(unit, path.FirstStep);
				}
				catch (InvalidOperationException)
				{
					// No path to any enemy found, do nothing.
					continue;
				}
			}
			inRangeEnemyPositions = _combatMap.AdjacentOfType(unit.Position, enemyType);
			// Check for enemies in range again, in case we moved.
			if (inRangeEnemyPositions.Any())
			{
				// Enemies in range found, attack
				int minHitPoints = inRangeEnemyPositions.Select(p => (Unit)_combatMap[p]!).Min(u => u.HitPoints);
				Coordinate attackTargetPosition = inRangeEnemyPositions
					.Where(p => ((Unit)_combatMap[p]!).HitPoints == minHitPoints)
					.OrderBy(p => p, new CoordinateComparer())
					.First();
				Unit targetUnit = _combatMap[attackTargetPosition] as Unit ?? throw new InvalidOperationException("No unit at attack target position.");
				AttackResult attackResult = unit.Attack(targetUnit);
				if (attackResult.TargetKilled)
				{
					if (ThrowOnElfDeath && targetUnit.Type is MapSpotType.Elf)
					{
						throw new ElfKilledException((GoblinUnit)unit, (ElfUnit)targetUnit);
					}
					_combatMap.DeleteUnit(targetUnit);
				}
			}
		}
		FullRoundsSimulated++;
		return true;
	}

	private static PathSignature ChoosePath(CombatMap map, Coordinate position, IEnumerable<Coordinate> targets)
	{
		PathGenerator pathGenerator = new(map);
		try
		{
			return pathGenerator.GenerateShortestPaths(position)
				.IntersectBy(targets, p => p.Key)
				.Select(kvp => kvp.Value)
				.OrderBy(path => path)
				.First();
		}
		catch (InvalidOperationException e)
		{
			throw new InvalidOperationException("No path found.", e);
		}
	}

	public void ResetCombat()
	{
		_combatMap = CombatMap.FromRawMap(_rawMap, CustomElfAttackPower);
		FullRoundsSimulated = 0;
	}
}
