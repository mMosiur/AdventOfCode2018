namespace AdventOfCode.Year2018.Day21.Device.CPUs;

public interface ICPU
{
	IReadOnlyRegisters Registers { get; }

	void Execute(Program program);
}
