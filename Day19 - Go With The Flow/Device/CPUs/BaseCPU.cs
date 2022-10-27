namespace AdventOfCode.Year2018.Day19.Device.CPUs;

abstract class BaseCPU : ICPU
{
	protected readonly Registers _registers;
	protected Program? _program;
	protected int _instructionPointer;
	protected int? _instructionPointerBoundRegisterNumber;

	private readonly Dictionary<Opcode, Action<Instruction>> _instructionOperations;
	private readonly Dictionary<DeclarationType, Action<Declaration>> _declarationOperations;

	#region Input helpers
	private uint InputAsRegister(byte input) => _registers[input];
#pragma warning disable CA1822 // We don't mark this as static to match it with InputAsRegister method.
	private uint InputAsValue(byte input) => input;
#pragma warning restore CA1822
	#endregion Input helpers

	#region Declaration operations
	private void ExecuteBindInstructionPointer(Declaration declaration)
	{
		_instructionPointerBoundRegisterNumber = declaration.Value;
	}
	#endregion Declaration operations

	#region Instruction operations
	private void ExecuteAddRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A) + InputAsRegister(i.B);
	private void ExecuteAddImmediate(Instruction i) => _registers[i.C] = InputAsRegister(i.A) + InputAsValue(i.B);
	private void ExecuteMultiplyRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A) * InputAsRegister(i.B);
	private void ExecuteMultiplyImmediate(Instruction i) => _registers[i.C] = InputAsRegister(i.A) * InputAsValue(i.B);
	private void ExecuteBitwiseAndRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A) & InputAsRegister(i.B);
	private void ExecuteBitwiseAndImmediate(Instruction i) => _registers[i.C] = InputAsRegister(i.A) & InputAsValue(i.B);
	private void ExecuteBitwiseOrRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A) | InputAsRegister(i.B);
	private void ExecuteBitwiseOrImmediate(Instruction i) => _registers[i.C] = InputAsRegister(i.A) | InputAsValue(i.B);
	private void ExecuteSetRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A);
	private void ExecuteSetImmediate(Instruction i) => _registers[i.C] = InputAsValue(i.A);
	private void ExecuteGreaterThanImmediateRegister(Instruction i) => _registers[i.C] = InputAsValue(i.A) > InputAsRegister(i.B) ? 1U : 0U;
	private void ExecuteGreaterThanRegisterImmediate(Instruction i) => _registers[i.C] = InputAsRegister(i.A) > InputAsValue(i.B) ? 1U : 0U;
	private void ExecuteGreaterThanRegisterRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A) > InputAsRegister(i.B) ? 1U : 0U;
	private void ExecuteEqualImmediateRegister(Instruction i) => _registers[i.C] = InputAsValue(i.A) == InputAsRegister(i.B) ? 1U : 0U;
	private void ExecuteEqualRegisterImmediate(Instruction i) => _registers[i.C] = InputAsRegister(i.A) == InputAsValue(i.B) ? 1U : 0U;
	private void ExecuteEqualRegisterRegister(Instruction i) => _registers[i.C] = InputAsRegister(i.A) == InputAsRegister(i.B) ? 1U : 0U;
	#endregion Instruction operations

	public IReadOnlyRegisters Registers => _registers;

	public BaseCPU(int numberOfRegisters) : this(new Registers(numberOfRegisters))
	{
	}

	public BaseCPU(Registers registers)
	{
		_registers = registers;
		_instructionPointer = 0;
		_instructionPointerBoundRegisterNumber = null;
		_instructionOperations = new()
		{
			[Opcode.AddRegister] = ExecuteAddRegister,
			[Opcode.AddImmediate] = ExecuteAddImmediate,
			[Opcode.MultiplyRegister] = ExecuteMultiplyRegister,
			[Opcode.MultiplyImmediate] = ExecuteMultiplyImmediate,
			[Opcode.BitwiseAndRegister] = ExecuteBitwiseAndRegister,
			[Opcode.BitwiseAndImmediate] = ExecuteBitwiseAndImmediate,
			[Opcode.BitwiseOrRegister] = ExecuteBitwiseOrRegister,
			[Opcode.BitwiseOrImmediate] = ExecuteBitwiseOrImmediate,
			[Opcode.SetRegister] = ExecuteSetRegister,
			[Opcode.SetImmediate] = ExecuteSetImmediate,
			[Opcode.GreaterThanImmediateRegister] = ExecuteGreaterThanImmediateRegister,
			[Opcode.GreaterThanRegisterImmediate] = ExecuteGreaterThanRegisterImmediate,
			[Opcode.GreaterThanRegisterRegister] = ExecuteGreaterThanRegisterRegister,
			[Opcode.EqualImmediateRegister] = ExecuteEqualImmediateRegister,
			[Opcode.EqualRegisterImmediate] = ExecuteEqualRegisterImmediate,
			[Opcode.EqualRegisterRegister] = ExecuteEqualRegisterRegister,
		};
		_declarationOperations = new Dictionary<DeclarationType, Action<Declaration>>()
		{
			[DeclarationType.BindInstructionPointer] = d => ExecuteBindInstructionPointer(d),
		};
	}

	protected void ExecuteDeclarations()
	{
		if (_program is null)
		{
			throw new InvalidOperationException("Program is not set.");
		}
		foreach (Declaration declaration in _program.Declarations)
		{
			if (!_declarationOperations.TryGetValue(declaration.Type, out Action<Declaration>? operation))
			{
				throw new InvalidOperationException($"Unknown declaration type '{declaration.Type}'.");
			}
			operation.Invoke(declaration);
		}
	}

	protected bool ExecuteNextInstruction()
	{
		if (_program is null)
		{
			throw new InvalidOperationException("Program is not set.");
		}
		if (_instructionPointer < 0 || _instructionPointer >= _program.Instructions.Count)
		{
			return false;
		}
		Instruction instruction = _program.Instructions[_instructionPointer];
		if (_instructionPointerBoundRegisterNumber.HasValue)
		{
			_registers[_instructionPointerBoundRegisterNumber.Value] = (uint)_instructionPointer;
		}
		if (!_instructionOperations.TryGetValue(instruction.Opcode, out Action<Instruction>? operation))
		{
			throw new InvalidOperationException($"Unknown opcode '{instruction.Opcode}'.");
		}
		operation.Invoke(instruction);
		if (_instructionPointerBoundRegisterNumber.HasValue)
		{
			_instructionPointer = (int)_registers[_instructionPointerBoundRegisterNumber.Value];
		}
		_instructionPointer++;
		return true;
	}

	public void Execute(Program program)
	{
		_program = program;
		_instructionPointer = 0;
		ExecuteMemberProgram();
	}

	protected abstract void ExecuteMemberProgram();
}
