namespace AdventOfCode.Year2018.Day21.Device.CPUs;

public class CPU : BaseCPU
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
