using System.Collections;

namespace AdventOfCode.Year2018.Day24;

sealed class Army : IReadOnlyCollection<Group>
{
	private readonly HashSet<Group> _activeGroups;
	private readonly HashSet<Group> _defeatedGroups;

	public string Name { get; }
	public IReadOnlyCollection<Group> ActiveGroups => _activeGroups;
	public IReadOnlyCollection<Group> DefeatedGroups => _defeatedGroups;
	public int Count => _activeGroups.Count + _defeatedGroups.Count;
	public bool IsDefeated => _activeGroups.Count == 0;

	public Army(string name)
	{
		ArgumentException.ThrowIfNullOrEmpty(name);
		Name = name;
		_activeGroups = new();
		_defeatedGroups = new();
	}

	public Group AddGroup(int unitCount, int hitPoints, int attackDamage, string attackType, int initiative, IEnumerable<string>? weaknesses = null, IEnumerable<string>? immunities = null)
	{
		ArmyGroup group = new(this, unitCount, hitPoints, attackDamage, attackType, initiative, weaknesses, immunities);
		if (group.IsDefeated)
		{
			_defeatedGroups.Add(group);
		}
		else
		{
			_activeGroups.Add(group);
			group.GroupDefeated += OnGroupDefeated;
		}
		return group;
	}

	private void OnGroupDefeated(object? sender, GroupDefeatedEventArgs e)
	{
		e.Defender.GroupDefeated -= OnGroupDefeated;
		_activeGroups.Remove(e.Defender);
		_defeatedGroups.Add(e.Defender);
	}

	public IEnumerator<Group> GetEnumerator() => _activeGroups.Concat(_defeatedGroups).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	sealed class ArmyGroup : Group
	{
		public ArmyGroup(Army army, int unitCount, int hitPoints, int attackDamage, string attackType, int initiative, IEnumerable<string>? weaknesses, IEnumerable<string>? immunities)
			: base(army, unitCount, hitPoints, attackDamage, attackType, initiative, weaknesses, immunities)
		{
		}
	}
}
