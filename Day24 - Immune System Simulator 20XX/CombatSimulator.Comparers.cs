namespace AdventOfCode.Year2018.Day24;

partial class CombatSimulator
{
	/// <summary>
	/// Comparer that determines order of groups in the target selection phase.
	/// </summary>
	class GroupTargetChooserOrderComparer : IComparer<Group>
	{
		public int Compare(Group? a, Group? b)
		{
			ArgumentNullException.ThrowIfNull(a);
			ArgumentNullException.ThrowIfNull(b);
			int result = a.EffectivePower.CompareTo(b.EffectivePower);
			if (result != 0)
			{
				return -result; // Descending order of effective power.
			}
			result = a.Initiative.CompareTo(b.Initiative);
			return -result; // Descending order of initiative.
		}
	}

	/// <summary>
	/// Comparer that determines order how a group chooses its target int the target selection phase.
	/// </summary>
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

	/// <summary>
	/// Comparer that determines order of groups in the attacking phase of the fight.
	/// </summary>
	class GroupAttackOrderComparer : IComparer<Group>
	{
		public int Compare(Group? a, Group? b)
		{
			ArgumentNullException.ThrowIfNull(a);
			ArgumentNullException.ThrowIfNull(b);
			int result = a.Initiative.CompareTo(b.Initiative);
			return -result; // Descending order of initiative.
		}
	}
}
