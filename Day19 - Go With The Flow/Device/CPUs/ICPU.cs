namespace AdventOfCode.Year2018.Day19.Device.CPUs;

public interface ICPU
{
	IReadOnlyRegisters Registers { get; }

	void Execute(Program program);
}
