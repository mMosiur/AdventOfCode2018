namespace AdventOfCode.Year2018.Day24;

sealed class Army
{
	private readonly Group[] _groups;

	public IReadOnlyCollection<Group> AllGroups => _groups;
	public IEnumerable<Group> ActiveGroups => _groups.Where(g => !g.IsDefeated);

	public Army(string name, IEnumerable<Group> groups)
	{
		ArgumentException.ThrowIfNullOrEmpty(name);
		ArgumentNullException.ThrowIfNull(groups);
		_groups = groups.ToArray();
	}
}
