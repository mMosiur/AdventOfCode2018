namespace AdventOfCode.Year2018.Day16.Device.CPUs;

class NamedOpcodeCPU : BaseCPU
{
	private readonly Dictionary<string, Action<byte, byte, byte>> _operations;

	public NamedOpcodeCPU(Registers startingRegisters) : base(startingRegisters)
	{
		_operations = new()
		{
			{ "addr", ExecuteAddRegister },
			{ "addi", ExecuteAddImmediate },
			{ "mulr", ExecuteMultiplyRegister },
			{ "muli", ExecuteMultiplyImmediate },
			{ "banr", ExecuteBitwiseAndRegister },
			{ "bani", ExecuteBitwiseAndImmediate },
			{ "borr", ExecuteBitwiseOrRegister },
			{ "bori", ExecuteBitwiseOrImmediate },
			{ "setr", ExecuteSetRegister },
			{ "seti", ExecuteSetImmediate },
			{ "gtir", ExecuteGreaterThanImmediateRegister },
			{ "gtri", ExecuteGreaterThanRegisterImmediate },
			{ "gtrr", ExecuteGreaterThanRegisterRegister },
			{ "eqir", ExecuteEqualImmediateRegister },
			{ "eqri", ExecuteEqualRegisterImmediate },
			{ "eqrr", ExecuteEqualRegisterRegister },
		};
	}

	public void ForceExecuteOperation(string opcode, Instruction instruction)
	{
		if (!_operations.TryGetValue(opcode, out Action<byte, byte, byte>? action))
		{
			throw new ArgumentException($"Invalid operation: '{opcode}'.", nameof(opcode));
		}
		action.Invoke(instruction.A, instruction.B, instruction.C);
	}
}
