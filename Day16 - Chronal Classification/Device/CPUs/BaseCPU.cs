namespace AdventOfCode.Year2018.Day16.Device.CPUs;

public class BaseCPU
{
	private readonly Registers _startingRegisters;

	protected readonly Registers _registers;

	public BaseCPU(Registers startingRegisters)
	{
		_startingRegisters = startingRegisters;
		_registers = new(startingRegisters);
	}

	public IReadOnlyRegisters Registers => _registers;

	#region Input helpers
	protected uint InputAsRegister(byte input) => _registers[input];
#pragma warning disable CA1822 // We don't mark this as static to match it with InputAsRegister method.
	protected uint InputAsValue(byte input) => input;
#pragma warning restore CA1822
	#endregion Input helpers

	#region Operations
	protected void ExecuteAddRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) + InputAsRegister(B);
	protected void ExecuteAddImmediate(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) + InputAsValue(B);
	protected void ExecuteMultiplyRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) * InputAsRegister(B);
	protected void ExecuteMultiplyImmediate(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) * InputAsValue(B);
	protected void ExecuteBitwiseAndRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) & InputAsRegister(B);
	protected void ExecuteBitwiseAndImmediate(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) & InputAsValue(B);
	protected void ExecuteBitwiseOrRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) | InputAsRegister(B);
	protected void ExecuteBitwiseOrImmediate(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) | InputAsValue(B);
#pragma warning disable IDE0060 // In assignment operations ignore input B, but want to keep it for consistency with other operations.
	protected void ExecuteSetRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A);
	protected void ExecuteSetImmediate(byte A, byte B, byte C) => _registers[C] = InputAsValue(A);
#pragma warning restore IDE0060
	protected void ExecuteGreaterThanImmediateRegister(byte A, byte B, byte C) => _registers[C] = InputAsValue(A) > InputAsRegister(B) ? 1U : 0U;
	protected void ExecuteGreaterThanRegisterImmediate(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) > InputAsValue(B) ? 1U : 0U;
	protected void ExecuteGreaterThanRegisterRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) > InputAsRegister(B) ? 1U : 0U;
	protected void ExecuteEqualImmediateRegister(byte A, byte B, byte C) => _registers[C] = InputAsValue(A) == InputAsRegister(B) ? 1U : 0U;
	protected void ExecuteEqualRegisterImmediate(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) == InputAsValue(B) ? 1U : 0U;
	protected void ExecuteEqualRegisterRegister(byte A, byte B, byte C) => _registers[C] = InputAsRegister(A) == InputAsRegister(B) ? 1U : 0U;
	#endregion Operations

	public bool CheckRegistersEquality(Registers registers)
	{
		return _registers == registers;
	}

	public void Reset()
	{
		_registers.SetFrom(_startingRegisters);
	}
}
