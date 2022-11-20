using System.Diagnostics;

namespace AdventOfCode.Year2018.Day24;

partial class CombatSimulator
{
	private IEnumerable<Group> AllActiveGroups => Army1.ActiveGroups.Concat(Army2.ActiveGroups);

	public Army Army1 { get; }
	public Army Army2 { get; }

	public CombatSimulator(Army army1, Army army2)
	{
		Army1 = army1;
		Army2 = army2;
	}

	public Army? Simulate()
	{
		Reset();
		while (true)
		{
			switch ((Army1.IsDefeated, Army2.IsDefeated))
			{
				case (false, true):
					return Army1;
				case (true, false):
					return Army2;
				case (true, true):
					return null;
			}
			bool fightContinues = Fight();
			if (!fightContinues)
			{
				return null;
			}
		}
	}

	public int FindAndSetSmallestBootValueForArmyToWin(string armyNameToBoost, in int lowerBoostBound, in int upperBoostBound)
	{
		ArgumentException.ThrowIfNullOrEmpty(armyNameToBoost);
		Army armyToBoost = armyNameToBoost switch
		{
			string armyName1 when Army1.Name.Equals(armyName1, StringComparison.OrdinalIgnoreCase) => Army1,
			string armyName2 when Army2.Name.Equals(armyName2, StringComparison.OrdinalIgnoreCase) => Army2,
			_ => throw new DaySolverException($"Army with name '{armyNameToBoost}' not found.")
		};
		int minBoostSearchValue = lowerBoostBound;
		int maxBoostSearchValue = upperBoostBound;
		while (minBoostSearchValue <= maxBoostSearchValue)
		{
			int boost = (minBoostSearchValue + maxBoostSearchValue) / 2;
			armyToBoost.AttackBoost = boost;
			Army? winner = Simulate();
			if (armyToBoost == winner)
			{
				maxBoostSearchValue = boost - 1;
			}
			else
			{
				minBoostSearchValue = boost + 1;
			}
		}
		if (minBoostSearchValue > upperBoostBound)
		{
			throw new DaySolverException($"No boost value found that results in a win for \"{armyNameToBoost}\".");
		}
		armyToBoost.AttackBoost = minBoostSearchValue;
		return minBoostSearchValue;
	}

	public void Reset()
	{
		Army1.Reset();
		Army2.Reset();
	}

	private bool Fight()
	{
		Dictionary<Group, Group> targets = TargetSelectionPhase();
		if (targets.Count == 0)
		{
			return false;
		}
		AttackingPhase(targets);
		return true;
	}

	private Dictionary<Group, Group> TargetSelectionPhase()
	{
		Dictionary<Group, Group> selectedTargets = new(AllActiveGroups.Count());
		HashSet<Group> availableTargets1 = new(Army1.ActiveGroups);
		HashSet<Group> availableTargets2 = new(Army2.ActiveGroups);
		IEnumerable<Group> orderedGroups = AllActiveGroups.Order(comparer: new GroupTargetChooserOrderComparer());
		foreach (Group group in orderedGroups)
		{
			HashSet<Group> availableTargets = group.Army switch
			{
				Army army1 when ReferenceEquals(army1, Army1) => availableTargets2,
				Army army2 when ReferenceEquals(army2, Army2) => availableTargets1,
				_ => throw new UnreachableException("Unexpected third army."),
			};
			if (availableTargets.Count == 0)
			{
				continue;
			}
			Group target = availableTargets
				.Order(comparer: new GroupTargetingOrderComparer(group))
				.First();
			if (group.CalculateDamageTowards(target) == 0)
			{
				continue;
			}
			selectedTargets.Add(group, target);
			availableTargets.Remove(target);
		}
		return selectedTargets;
	}

	private void AttackingPhase(Dictionary<Group, Group> targets)
	{
		GroupAttackOrderComparer attackOrderComparer = new();
		IEnumerable<Group> orderedGroups = AllActiveGroups.Order(attackOrderComparer);
		foreach (Group group in orderedGroups)
		{
			if (group.UnitCount == 0)
			{
				continue;
			}
			if (!targets.TryGetValue(group, out Group? target))
			{
				continue;
			}
			group.Attack(target);
		}
	}
}
