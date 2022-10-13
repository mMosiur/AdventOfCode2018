using AdventOfCode.Year2018.Day21.Device;

namespace AdventOfCode.Year2018.Day21;

public class ActivationSystemProgramDisassembler
{
	private readonly int _controlRegisterNumber;

	public ActivationSystemProgramDisassembler(int controlRegisterNumber)
	{
		_controlRegisterNumber = controlRegisterNumber;
	}

	public DisassembledActivationSystemInfo Disassemble(Device.Program program)
	{
		(Instruction targetInstruction, int targetIndex) = program.Instructions
			.Select((instruction, index) => (instruction, index))
			.Where(x => x.instruction.Opcode.IsComparison() && x.instruction.Opcode.HasRegisterOperand())
			.Where(x => x.instruction.A == _controlRegisterNumber || x.instruction.B == _controlRegisterNumber)
			.Single();
		if (!targetInstruction.Opcode.HasBothRegisterOperands())
		{
			throw new InvalidOperationException($"Unexpected target instruction: '{targetInstruction}'.");
		}
		int targetRegister;
		if (targetInstruction.A != _controlRegisterNumber && targetInstruction.B == _controlRegisterNumber)
		{
			targetRegister = (int)targetInstruction.A;
		}
		else if (targetInstruction.A == _controlRegisterNumber && targetInstruction.B != _controlRegisterNumber)
		{
			targetRegister = (int)targetInstruction.B;
		}
		else
		{
			throw new InvalidOperationException($"Unexpected target instruction: '{targetInstruction}'.");
		}
		return new DisassembledActivationSystemInfo(targetIndex, targetRegister);
	}
}

public readonly struct DisassembledActivationSystemInfo
{
	public int StoppingPoint { get; }
	public int TargetRegisterNumber { get; }

	public DisassembledActivationSystemInfo(int stoppingPoint, int targetRegisterNumber)
	{
		StoppingPoint = stoppingPoint;
		TargetRegisterNumber = targetRegisterNumber;
	}
}
