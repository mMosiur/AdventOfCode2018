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
