namespace AdventOfCode.Year2018.Day21.Device.CPUs;

public class StoppingPointCPU : BaseCPU
{
	private readonly int _stoppingPoint;

	public StoppingPointCPU(int numberOfRegisters, int stoppingPoint) : base(numberOfRegisters)
	{
		_stoppingPoint = stoppingPoint;
	}

	public StoppingPointCPU(Registers registers, int stoppingPoint) : base(registers)
	{
		_stoppingPoint = stoppingPoint;
	}

	protected override void ExecuteMemberProgram()
	{
		ExecuteDeclarations();
		while (ExecuteNextInstruction())
		{
			if(_instructionPointer == _stoppingPoint)
			{
				break;
			}
		}
	}
}
