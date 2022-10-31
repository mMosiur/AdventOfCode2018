using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day19.Device;
using AdventOfCode.Year2018.Day19.Device.CPUs;

namespace AdventOfCode.Year2018.Day19;

public sealed class Day19Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 19;
	public override string Title => "XD";

	private readonly Device.Program _program;
	private readonly Day19SolverOptions _options;

	public Day19Solver(Day19SolverOptions options) : base(options)
	{
		_options = options;
		_program = Device.Program.Parse(Input);
	}

	public Day19Solver(Action<Day19SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day19Solver() : this(Day19SolverOptions.Default)
	{
	}

	private uint GetResultAfterExecution(ICPU cpu, Device.Program program)
	{
		cpu.Execute(program);
		return cpu.Registers[_options.ResultRegisterNumber];
	}

	private bool CheckBaseProgramOnRiggedCPU()
	{
		ICPU cpuProper = new CPU(6);
		ICPU cpuRigged = new RiggedCPU(6);
		uint resultProper = GetResultAfterExecution(cpuProper, _program);
		uint resultRigged = GetResultAfterExecution(cpuRigged, _program);
		return resultProper == resultRigged;
	}

	public override string SolvePart1()
	{
		ICPU cpu = new CPU(_options.NumberOfRegisters);
		uint result = GetResultAfterExecution(cpu, _program);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		if (!CheckBaseProgramOnRiggedCPU())
		{
			throw new ApplicationException("Base program does not work on rigged CPU.");
		}
		Registers registers = new(_options.NumberOfRegisters)
		{
			[_options.PartTwoChangedRegisterNumber] = _options.PartTwoChangedRegisterValue
		};
		ICPU cpu = new RiggedCPU(registers);
		try
		{
			uint result = GetResultAfterExecution(cpu, _program);
			return result.ToString();
		}
		catch (RiggedCPUException e)
		{
			throw new ApplicationException("The disassembled program is not compatible with the rigged CPU.", e);
		}
	}
}
