using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day21.Device.CPUs;

namespace AdventOfCode.Year2018.Day21;

public class Day21Solver : DaySolver
{
	private readonly Device.Program _program;
	private readonly Lazy<DisassembledActivationSystemInfo> _systemInfo;
	private readonly int _numberOfRegisters;

	public DisassembledActivationSystemInfo SystemInfo => _systemInfo.Value;

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

	public Day21Solver(Action<Day21SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		StoppingPointCPU cpu = new(_numberOfRegisters, SystemInfo.StoppingPoint);
		cpu.Execute(_program);
		ulong result = cpu.Registers[SystemInfo.TargetRegisterNumber];
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
