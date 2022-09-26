namespace AdventOfCode.Year2018.Day16.Device;

public class Cpu
{
	private readonly Dictionary<string, Action<Instruction>> _operations;
	private readonly Registers _startingRegisters;

	public Registers Registers { get; private set; }

	private uint Register(byte index) => Registers[index];
	private static uint Value(byte value) => value;

	public Cpu(Registers startingRegisters)
	{
		_startingRegisters = startingRegisters.Clone();
		Registers = _startingRegisters.Clone();
		_operations = new()
		{
			{ "addr", (i) => Registers[i.C] = Register(i.A) + Register(i.B) },
			{ "addi", (i) => Registers[i.C] = Register(i.A) + Value(i.B) },
			{ "mulr", (i) => Registers[i.C] = Register(i.A) * Register(i.B) },
			{ "muli", (i) => Registers[i.C] = Register(i.A) * Value(i.B) },
			{ "banr", (i) => Registers[i.C] = Register(i.A) & Register(i.B) },
			{ "bani", (i) => Registers[i.C] = Register(i.A) & Value(i.B) },
			{ "borr", (i) => Registers[i.C] = Register(i.A) | Register(i.B) },
			{ "bori", (i) => Registers[i.C] = Register(i.A) | Value(i.B) },
			{ "setr", (i) => Registers[i.C] = Register(i.A) },
			{ "seti", (i) => Registers[i.C] = Value(i.A) },
			{ "gtir", (i) => Registers[i.C] = Value(i.A) > Register(i.B) ? 1U : 0U },
			{ "gtri", (i) => Registers[i.C] = Register(i.A) > Value(i.B) ? 1U : 0U },
			{ "gtrr", (i) => Registers[i.C] = Register(i.A) > Register(i.B) ? 1U : 0U },
			{ "eqir", (i) => Registers[i.C] = Value(i.A) == Register(i.B) ? 1U : 0U },
			{ "eqri", (i) => Registers[i.C] = Register(i.A) == Value(i.B) ? 1U : 0U },
			{ "eqrr", (i) => Registers[i.C] = Register(i.A) == Register(i.B) ? 1U : 0U },
		};
	}

	public void Execute(Instruction instruction)
	{
		throw new NotImplementedException();
	}

	public void ForceExecuteOperation(string operation, Instruction instruction)
	{
		if (!_operations.TryGetValue(operation, out Action<Instruction>? action))
		{
			throw new ArgumentException($"Invalid operation: '{operation}'.", nameof(operation));
		}
		action.Invoke(instruction);
	}

	public void Reset()
	{
		Registers = _startingRegisters.Clone();
	}
}
