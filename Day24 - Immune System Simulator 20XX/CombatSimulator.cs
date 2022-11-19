namespace AdventOfCode.Year2018.Day24;

class CombatSimulator
{
	private IEnumerable<Group> ActiveGroups => Army1.ActiveGroups.Concat(Army2.ActiveGroups);

	public Army Army1 { get; }
	public Army Army2 { get; }

	public CombatSimulator(Army army1, Army army2)
	{
		Army1 = army1;
		Army2 = army2;
	}

	public void Simulate()
	{
		while (Army1.ActiveGroups.Any() && Army2.ActiveGroups.Any())
		{
			Fight();
		}
	}

	private void Fight()
	{
		Dictionary<Group, Group> targets = TargetSelectionPhase();
		AttackingPhase(targets);
	}

	private Dictionary<Group, Group> TargetSelectionPhase()
	{
		Dictionary<Group, Group> selectedTargets = new(ActiveGroups.Count());
		HashSet<Group> availableTargets = new(ActiveGroups);
		GroupTargetChooserOrderComparer orderComparer = new();
		IEnumerable<Group> orderedGroups = ActiveGroups.Order(orderComparer);
		foreach (Group group in orderedGroups)
		{
			GroupTargetingOrderComparer targetComparer = new(group);
			Group target = availableTargets.Order(targetComparer).First();
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
		IEnumerable<Group> orderedGroups = ActiveGroups.Order(attackOrderComparer);
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
			bool defeated = group.Attack(target);
		}
	}
}

class GroupTargetChooserOrderComparer : IComparer<Group>
{
	public int Compare(Group? a, Group? b)
	{
		ArgumentNullException.ThrowIfNull(a);
		ArgumentNullException.ThrowIfNull(b);
		int result = b.EffectivePower.CompareTo(a.EffectivePower);
		if (result != 0)
		{
			return -result; // Descending order of effective power.
		}
		result = b.Initiative.CompareTo(a.Initiative);
		return -result; // Descending order of initiative.
	}
}

class GroupTargetingOrderComparer : IComparer<Group>
{
	private readonly Group _attacker;

	public GroupTargetingOrderComparer(Group attacker)
	{
		ArgumentNullException.ThrowIfNull(attacker);
		_attacker = attacker;
	}

	public int Compare(Group? a, Group? b)
	{
		ArgumentNullException.ThrowIfNull(a);
		ArgumentNullException.ThrowIfNull(b);
		int damageToA = _attacker.CalculateDamageTowards(a);
		int damageToB = _attacker.CalculateDamageTowards(b);
		int result = damageToA.CompareTo(damageToB);
		if (result != 0)
		{
			return -result; // Descending order of damage.
		}
		result = a.EffectivePower.CompareTo(b.EffectivePower);
		if (result != 0)
		{
			return -result; // Descending order of effective power.
		}
		result = a.Initiative.CompareTo(b.Initiative);
		return -result; // Descending order of initiative.
	}
}

class GroupAttackOrderComparer : IComparer<Group>
{
	public int Compare(Group? a, Group? b)
	{
		ArgumentNullException.ThrowIfNull(a);
		ArgumentNullException.ThrowIfNull(b);
		int result = b.Initiative.CompareTo(a.Initiative);
		return -result; // Descending order of initiative.
	}
}
