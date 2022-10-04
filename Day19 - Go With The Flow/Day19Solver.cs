using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day19.Device;
using AdventOfCode.Year2018.Day19.Device.CPUs;

namespace AdventOfCode.Year2018.Day19;

public class Day19Solver : DaySolver
{
	private readonly Device.Program _program;
	private readonly Day19SolverOptions _options;

	public Day19Solver(Day19SolverOptions options) : base(options)
	{
		_options = options;
		_program = Device.Program.Parse(Input);
	}

	public Day19Solver(Action<Day19SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	private uint GetResultAfterExecution(ICPU cpu, Device.Program program)
	{
		cpu.Execute(program);
		return cpu.Registers[_options.ResultRegisterNumber];
	}

	public override string SolvePart1()
	{
		ICPU cpu = new CPU(_options.NumberOfRegisters);
		uint result = GetResultAfterExecution(cpu, _program);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
