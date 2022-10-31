using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day21.Device.CPUs;

namespace AdventOfCode.Year2018.Day21;

public sealed class Day21Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 21;
	public override string Title => "XD";

	private readonly Device.Program _program;
	private readonly Lazy<DisassembledActivationSystemInfo> _systemInfo;
	private readonly int _numberOfRegisters;

	private DisassembledActivationSystemInfo SystemInfo => _systemInfo.Value;

	public Day21Solver(Day21SolverOptions options) : base(options)
	{
		_program = Device.Program.Parse(Input);
		_systemInfo = new(() =>
		{
			ActivationSystemProgramDisassembler disassembler = new(options.ControlRegisterNumber);
			return disassembler.Disassemble(_program);
		});
		_numberOfRegisters = options.NumberOfRegisters;
	}

	public Day21Solver(Action<Day21SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day21Solver() : this(Day21SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		StoppingPointCPU cpu = new(_numberOfRegisters, SystemInfo.TargetInstructionIndex);
		cpu.Execute(_program);
		ulong result = cpu.Registers[SystemInfo.TargetRegisterNumber];
		return result.ToString();
	}

	public override string SolvePart2()
	{
		RiggedCPU cpu = new(SystemInfo.TargetRegisterResetValue);
		ulong result = cpu.FindNonHaltingValues().Last();
		return result.ToString();
	}
}
