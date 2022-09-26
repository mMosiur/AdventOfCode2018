namespace AdventOfCode.Year2018.Day16.Device.CPUs;

public class CPU : BaseCPU
{
	private readonly Dictionary<byte, Action<byte, byte, byte>> _operations;

	public CPU(Registers startingRegisters, OpcodeDictionary opcodeDictionary) : base(startingRegisters)
	{
		_operations = new()
		{
			{ opcodeDictionary.OpcodeNameFromNumber["addr"], ExecuteAddRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["addi"], ExecuteAddImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["mulr"], ExecuteMultiplyRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["muli"], ExecuteMultiplyImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["banr"], ExecuteBitwiseAndRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["bani"], ExecuteBitwiseAndImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["borr"], ExecuteBitwiseOrRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["bori"], ExecuteBitwiseOrImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["setr"], ExecuteSetRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["seti"], ExecuteSetImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["gtir"], ExecuteGreaterThanImmediateRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["gtri"], ExecuteGreaterThanRegisterImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["gtrr"], ExecuteGreaterThanRegisterRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["eqir"], ExecuteEqualImmediateRegister },
			{ opcodeDictionary.OpcodeNameFromNumber["eqri"], ExecuteEqualRegisterImmediate },
			{ opcodeDictionary.OpcodeNameFromNumber["eqrr"], ExecuteEqualRegisterRegister },
		};
	}

	public void Execute(Instruction instruction)
	{
		if (!_operations.TryGetValue(instruction.Opcode, out Action<byte, byte, byte>? action))
		{
			throw new ArgumentException($"Invalid operation: '{instruction.Opcode}'.", nameof(instruction));
		}
		action.Invoke(instruction.A, instruction.B, instruction.C);
	}

	public void Execute(IEnumerable<Instruction> instruction)
	{
		foreach (Instruction i in instruction)
		{
			Execute(i);
		}
	}
}
