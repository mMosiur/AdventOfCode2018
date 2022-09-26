using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day16.Device;
using AdventOfCode.Year2018.Day16.Device.CPUs;

namespace AdventOfCode.Year2018.Day16;

public class Day16Solver : DaySolver
{
	private static readonly Lazy<Regex> _inputFileRegexLazy = new(
		() => new Regex(@"\n{2,}", RegexOptions.Compiled)
	);

	private readonly Day16SolverOptions _options;
	private readonly IReadOnlyCollection<Sample> _samples;
	private readonly IReadOnlyList<Device.Instruction> _instructions;

	public Day16Solver(Day16SolverOptions options) : base(options)
	{
		_options = options;
		string[] parts = _inputFileRegexLazy.Value.Split(Input);
		_samples = parts
			.Take(parts.Length - 1)
			.Select(Sample.Parse)
			.ToList();
		_instructions = parts
			.Last()
			.Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(Instruction.Parse)
			.ToList();
	}

	public Day16Solver(Action<Day16SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		int result = 0;
		foreach (Sample sample in _samples)
		{
			int matches = 0;
			NamedOpcodeCPU cpu = new(sample.RegistersBeforeOperation);
			foreach (string operation in OpcodeDictionary.OpcodeNames)
			{
				cpu.ForceExecuteOperation(operation, sample.Operation);
				if (cpu.CheckRegistersEquality(sample.RegistersAfterOperation))
				{
					matches++;
				}
				cpu.Reset();
			}
			if (matches >= _options.PartOneMinimumBehaviorMatches)
			{
				result++;
			}
		}
		return result.ToString(); ;
	}

	public override string SolvePart2()
	{
		OpcodeDictionaryResolver opcodeDictionaryResolver = new();
		OpcodeDictionary opcodeDictionary = opcodeDictionaryResolver.ResolveFromSamples(_samples);
		CPU cpu = new(new Registers(_options.RegisterSize), opcodeDictionary);
		cpu.Execute(_instructions);
		return cpu.Registers[_options.PartTwoResultValueRegister].ToString();
	}
}
