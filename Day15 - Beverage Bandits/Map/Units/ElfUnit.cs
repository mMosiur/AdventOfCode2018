namespace AdventOfCode.Year2018.Day15.Map.Units;

public class ElfUnit : Unit
{
	public override MapSpotType Type => MapSpotType.Elf;

	public ElfUnit() : base() { }

	public ElfUnit(int attackPower) : base(attackPower) { }
}
