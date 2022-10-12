namespace AdventOfCode.Year2018.Day21.Device.CPUs;

public class RiggedCPU : BaseCPU
{
	private readonly uint initialR0;

	public RiggedCPU(int numberOfRegisters) : base(numberOfRegisters)
	{
	}

	public RiggedCPU(Registers registers) : base(registers)
	{
		initialR0 = registers[0];
	}

	private void ResetRegisters()
	{
		_registers.Clear();
		_registers[0] = initialR0;
	}

	private void AssertRegistersSupportDisassembledProgram()
	{
		uint r0 = Registers[0];
		if (r0 != 0 && r0 != 1)
		{
			throw new RiggedCPUException("Register 0 must start with either 0 or 1.");
		}
	}

	private void AssertDeclarationsSupportDisassembledProgram()
	{
		if (_program is null)
		{
			throw new InvalidOperationException("Program has not been loaded.");
		}
		if (!_instructionPointerBoundRegisterNumber.HasValue)
		{
			throw new RiggedCPUException("Instruction pointer has not been set.");
		}
		if (_instructionPointerBoundRegisterNumber.Value != 3)
		{
			throw new RiggedCPUException("Instruction pointer must be bound to register 3.");
		}
	}

	private void AssertProgramLength()
	{
		Program program = _program!;
		if (program!.Instructions.Count != 36)
		{
			throw new RiggedCPUException("Disassembled program must have exactly 35 instructions.");
		}
	}

	private void AssertJumpToSetupBlock()
	{
		Program program = _program!;
		if (program!.Instructions[0] != new Instruction(Opcode.AddImmediate, 3, 16, 3))
		{
			throw new RiggedCPUException("Instruction 0 must jump into setup block ('addi 3 16 3').");
		}
	}

	private uint AssertBaseSetupBlockAndReturnR5()
	{
		ResetRegisters();
		_instructionPointer = 17;
		for (int i = 0; i < 8; i++)
		{
			if (!ExecuteNextInstruction())
			{
				throw new RiggedCPUException("Program unexpectedly finished during base setup block.");
			}
		}
		if (_instructionPointer != 25)
		{
			throw new RiggedCPUException("Program did not end up where it was expected after base setup block.");
		}
		if (_program!.Instructions[25] != new Instruction(Opcode.AddRegister, 3, 0, 3))
		{
			throw new RiggedCPUException("Instruction 25 must jump into extended setup if r0 is 1 ('addr 3 0 3').");
		}
		Instruction instruction26 = _program!.Instructions[26];
		Instruction expectedInstruction26 = new Instruction(Opcode.SetImmediate, 0, 6, 3) with { B = instruction26.B };
		if (instruction26 != expectedInstruction26)
		{
			throw new RiggedCPUException("Instruction 26 must set r3 to 0 ('seti 0 x 3').");
		}
		return Registers[5];
	}

	private uint AssertExtendedSetupBlockAndReturnR5(uint baseSetupR5)
	{
		ResetRegisters();
		_instructionPointer = 27;
		for (int i = 0; i < 6; i++)
		{
			if (!ExecuteNextInstruction())
			{
				throw new RiggedCPUException("Program unexpectedly finished during extended setup block.");
			}
		}
		if (_instructionPointer != 33)
		{
			throw new RiggedCPUException("Program did not end up where it was expected after base setup block.");
		}
		if (_program!.Instructions[33] != new Instruction(Opcode.AddRegister, 5, 1, 5))
		{
			throw new RiggedCPUException("Instruction 33 must add r1 content into r5 ('addr 5 1 5').");
		}
		uint extendedSetupR5 = baseSetupR5 + Registers[1];
		Instruction instruction34 = _program!.Instructions[34];
		Instruction expectedInstruction34 = new Instruction(Opcode.SetImmediate, 0, 0, 0) with { B = instruction34.B };
		if (instruction34 != expectedInstruction34)
		{
			throw new RiggedCPUException("Instruction 34 must set r0 to 0 ('seti 0 x 0').");
		}
		Instruction instruction35 = _program!.Instructions[35];
		Instruction expectedInstruction35 = new Instruction(Opcode.SetImmediate, 0, 3, 3) with { B = instruction35.B };
		if (instruction35 != expectedInstruction35)
		{
			throw new RiggedCPUException("Instruction 35 must set r3 to 0 ('seti 0 x 3').");
		}
		return extendedSetupR5;
	}

	/// <summary>
	/// The methods checks if the program is in a state where it can be optimized,
	/// as described in InputDisassembly.md.
	/// </summary>
	private uint AssertInstructionsSupportDisassembledProgramAndReturnR5()
	{
		if (_program is null)
		{
			throw new InvalidOperationException("Program has not been loaded.");
		}
		AssertProgramLength();
		AssertJumpToSetupBlock();
		uint baseR5 = AssertBaseSetupBlockAndReturnR5();
		uint extendedR5 = AssertExtendedSetupBlockAndReturnR5(baseR5);
		return initialR0 switch
		{
			0 => baseR5,
			1 => extendedR5,
			_ => throw new RiggedCPUException("Register 0 must start with either 0 or 1.")
		};
	}

	/// <summary>
	/// As per disassembled program, the main loop calculates the sum of all factors of the number from register 5.
	/// </summary>
	/// <param name="r5">The number from register 5.</param>
	private void RunDisassembledMainLoop(uint r5)
	{
		if (r5 == 0)
		{
			_registers[0] = 0;
			return;
		}
		uint r0 = 1;
		for (uint i = 2; i <= Math.Sqrt(r5); i++)
		{
			if (r5 % i == 0)
			{
				if (i == (r5 / i))
					r0 += i;
				else
					r0 += i + r5 / i;
			}
		}
		_registers[0] = r0 + r5;
	}

	protected override void ExecuteMemberProgram()
	{
		try
		{
			AssertRegistersSupportDisassembledProgram();
			ExecuteDeclarations();
			AssertDeclarationsSupportDisassembledProgram();
			// As explained in InputDisassembly.md, the program is really a loop
			// that calculates the sum of all factors of the number in register 5.
			uint r5 = AssertInstructionsSupportDisassembledProgramAndReturnR5();
			RunDisassembledMainLoop(r5);
		}
		catch (RiggedCPUException e)
		{
			throw new RiggedCPUException("Unsupported program.", e);
		}
	}
}

[Serializable]
public class RiggedCPUException : InvalidOperationException
{
	public RiggedCPUException() { }
	public RiggedCPUException(string message) : base(message) { }
	public RiggedCPUException(string message, Exception inner) : base(message, inner) { }
	protected RiggedCPUException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
