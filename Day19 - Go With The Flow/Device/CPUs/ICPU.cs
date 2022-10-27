namespace AdventOfCode.Year2018.Day19.Device.CPUs;

interface ICPU
{
	IReadOnlyRegisters Registers { get; }

	void Execute(Program program);
}
