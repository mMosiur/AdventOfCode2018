namespace AdventOfCode.Year2018.Day21.Device.CPUs;

interface ICPU
{
	IReadOnlyRegisters Registers { get; }

	void Execute(Program program);
}
