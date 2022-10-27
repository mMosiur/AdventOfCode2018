namespace AdventOfCode.Year2018.Day19.Device.CPUs;

class CPU : BaseCPU
{
	public CPU(int numberOfRegisters) : base(numberOfRegisters)
	{
	}

	public CPU(Registers registers) : base(registers)
	{
	}

	protected override void ExecuteMemberProgram()
	{
		ExecuteDeclarations();
		while (ExecuteNextInstruction()) { }
	}
}
