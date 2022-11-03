using AdventOfCode.Year2018.Day21.Device;

namespace AdventOfCode.Year2018.Day21;

class ActivationSystemProgramDisassembler
{
	private readonly int _controlRegisterNumber;

	public ActivationSystemProgramDisassembler(int controlRegisterNumber)
	{
		_controlRegisterNumber = controlRegisterNumber;
	}

	public DisassembledActivationSystemInfo Disassemble(Device.Program program)
	{
		try
		{
			IReadOnlyList<Instruction> instructions = program.Instructions;
			int instructionPointerRegister = AssertDeclarationsAndGetInstructionPointerRegister(program);
			AssertInstruction(instructions[3], Opcode.AddRegister, c: instructionPointerRegister);
			AssertInstruction(instructions[4], Opcode.SetImmediate, c: instructionPointerRegister);
			AssertInstruction(instructions[5], Opcode.SetImmediate, a: 0);
			byte targetRegisterNumber = instructions[5].C;
			AssertInstruction(instructions[6], Opcode.BitwiseOrImmediate, a: targetRegisterNumber, b: 65536);
			byte temp1RegisterNumber = instructions[6].C;
			AssertInstruction(instructions[7], Opcode.SetImmediate, c: targetRegisterNumber);
			uint targetRegisterResetValue = instructions[7].A;
			AssertInstruction(instructions[8], Opcode.BitwiseAndImmediate, a: temp1RegisterNumber, b: 255);
			byte temp2RegisterNumber = instructions[8].C;
			AssertInstruction(instructions[9], Opcode.AddRegister, a: targetRegisterNumber, b: temp2RegisterNumber, c: targetRegisterNumber);
			AssertInstruction(instructions[10], Opcode.BitwiseAndImmediate, a: targetRegisterNumber, b: 16777215, c: targetRegisterNumber);
			AssertInstruction(instructions[11], Opcode.MultiplyImmediate, a: targetRegisterNumber, b: 65899, c: targetRegisterNumber);
			AssertInstruction(instructions[12], Opcode.BitwiseAndImmediate, a: targetRegisterNumber, b: 16777215, c: targetRegisterNumber);
			AssertInstruction(instructions[13], Opcode.GreaterThanImmediateRegister, a: 256, b: temp1RegisterNumber, c: temp2RegisterNumber);
			AssertInstruction(instructions[14], Opcode.AddRegister, a: temp2RegisterNumber, b: instructionPointerRegister, c: instructionPointerRegister);
			AssertInstruction(instructions[15], Opcode.AddImmediate, a: instructionPointerRegister, b: temp2RegisterNumber, c: instructionPointerRegister);
			AssertInstruction(instructions[16], Opcode.SetImmediate, a: 27, c: instructionPointerRegister);
			(int targetInstructionIndex, int targetInstructionRegister) = RetrieveTargetInstructionIndexAndRegister(program);
			return new DisassembledActivationSystemInfo(
				targetInstructionIndex,
				targetInstructionRegister,
				targetRegisterResetValue
			);
		}
		catch (InvalidOperationException e)
		{
			throw new InvalidOperationException("Failed to disassemble program.", e);
		}
	}

	private static int AssertDeclarationsAndGetInstructionPointerRegister(Device.Program program)
	{
		try
		{
			Declaration declaration = program.Declarations.Single();
			if (declaration.Type is not DeclarationType.BindInstructionPointer)
			{
				throw new InvalidOperationException("Expected a single bind instruction pointer declaration.");
			}
			return declaration.Value;
		}
		catch (InvalidOperationException e)
		{
			throw new InvalidOperationException("Unsupported program declarations.", e);
		}
	}

	private static void AssertInstruction(Instruction instruction, Opcode opcode, int? a = null, int? b = null, int? c = null)
	{
		if (instruction.Opcode != opcode)
		{
			throw new InvalidOperationException($"Expected opcode '{opcode}'.");
		}
		if (a is not null && instruction.A != a)
		{
			throw new InvalidOperationException($"Expected A to be '{a}'.");
		}
		if (b is not null && instruction.B != b)
		{
			throw new InvalidOperationException($"Expected B to be '{b}'.");
		}
		if (c is not null && instruction.C != c)
		{
			throw new InvalidOperationException($"Expected C to be '{c}'.");
		}
	}

	private (int Index, int Register) RetrieveTargetInstructionIndexAndRegister(Device.Program program)
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
		return (targetIndex, targetRegister);
	}
}

readonly struct DisassembledActivationSystemInfo
{
	public int TargetInstructionIndex { get; }
	public int TargetRegisterNumber { get; }
	public uint TargetRegisterResetValue { get; }

	public DisassembledActivationSystemInfo(int stoppingPoint, int targetRegisterNumber, uint targetRegisterResetValue)
	{
		TargetInstructionIndex = stoppingPoint;
		TargetRegisterNumber = targetRegisterNumber;
		TargetRegisterResetValue = targetRegisterResetValue;
	}
}
