namespace AdventOfCode.Year2018.Day16.Device.CPUs;

class CPU : BaseCPU
{
	private readonly Dictionary<byte, Action<byte, byte, byte>> _operations;

	public CPU(Registers startingRegisters, OpcodeDictionary opcodeDictionary) : base(startingRegisters)
	{
		_operations = new()
		{
			{ opcodeDictionary.OpcodeNameToNumber["addr"], ExecuteAddRegister },
			{ opcodeDictionary.OpcodeNameToNumber["addi"], ExecuteAddImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["mulr"], ExecuteMultiplyRegister },
			{ opcodeDictionary.OpcodeNameToNumber["muli"], ExecuteMultiplyImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["banr"], ExecuteBitwiseAndRegister },
			{ opcodeDictionary.OpcodeNameToNumber["bani"], ExecuteBitwiseAndImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["borr"], ExecuteBitwiseOrRegister },
			{ opcodeDictionary.OpcodeNameToNumber["bori"], ExecuteBitwiseOrImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["setr"], ExecuteSetRegister },
			{ opcodeDictionary.OpcodeNameToNumber["seti"], ExecuteSetImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["gtir"], ExecuteGreaterThanImmediateRegister },
			{ opcodeDictionary.OpcodeNameToNumber["gtri"], ExecuteGreaterThanRegisterImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["gtrr"], ExecuteGreaterThanRegisterRegister },
			{ opcodeDictionary.OpcodeNameToNumber["eqir"], ExecuteEqualImmediateRegister },
			{ opcodeDictionary.OpcodeNameToNumber["eqri"], ExecuteEqualRegisterImmediate },
			{ opcodeDictionary.OpcodeNameToNumber["eqrr"], ExecuteEqualRegisterRegister },
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
