using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day19.Device;

namespace AdventOfCode.Year2018.Day19;

public class Day19Solver : DaySolver
{
	private readonly Device.Program _program;

	public Day19Solver(Day19SolverOptions options) : base(options)
	{
		_program = Device.Program.Parse(Input);
	}

	public Day19Solver(Action<Day19SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		const int numberOfRegisters = 6;
		const int resultRegisterNumber = 0;

		CPU cpu = new(_program, numberOfRegisters);
		cpu.ExecuteProgram();
		uint result = cpu.Registers[resultRegisterNumber];
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
