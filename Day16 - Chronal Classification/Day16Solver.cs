using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day16;

public class Day16Solver : DaySolver
{
	private static readonly Lazy<Regex> _inputFileRegexLazy = new(
		() => new Regex(@"\n{2,}", RegexOptions.Compiled)
	);

	private readonly IReadOnlyCollection<Sample> _samples;
	private readonly IReadOnlyList<Device.Instruction> _instructions;

	public Day16Solver(Day16SolverOptions options) : base(options)
	{
		string[] parts = _inputFileRegexLazy.Value.Split(Input);
		_samples = parts
			.Take(parts.Length - 1)
			.Select(Sample.Parse)
			.ToList();
		_instructions = parts
			.Last()
			.Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(Device.Instruction.Parse)
			.ToList();
	}

	public Day16Solver(Action<Day16SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		IReadOnlyList<string> operations = new List<string>() { "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr" };
		int result = 0;
		foreach (Sample sample in _samples)
		{
			int matches = 0;
			Device.Cpu cpu = new(sample.RegistersBeforeOperation);
			foreach (string operation in operations)
			{
				cpu.ForceExecuteOperation(operation, sample.Operation);
				if (cpu.Registers == sample.RegistersAfterOperation)
				{
					matches++;
				}
				cpu.Reset();
			}
			if (matches >= 3)
			{
				result++;
			}
		}
		return result.ToString(); ;
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
