using AdventOfCode.Year2018.Day15.Map.Units;

namespace AdventOfCode.Year2018.Day15.Exceptions;

[Serializable]
public class ElfKilledException : Exception
{
	public ElfUnit KilledElf { get; }
	public GoblinUnit Killer { get; }

	public ElfKilledException(GoblinUnit killer, ElfUnit killedElf)
	{
		Killer = killer;
		KilledElf = killedElf;
	}
}
