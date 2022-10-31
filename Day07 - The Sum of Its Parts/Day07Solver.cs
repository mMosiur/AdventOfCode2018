using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day07;

public sealed class Day07Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 7;
	public override string Title => "XD";

	private readonly IReadOnlyList<Instruction> _instructions;
	private readonly Lazy<StepMap> _stepMap;

	private StepMap StepMap => _stepMap.Value;

	public int WorkersCount { get; }
	public int StepOverheadDuration { get; }

	public Day07Solver(Day07SolverOptions options) : base(options)
	{
		_instructions = InputLines.Select(Instruction.Parse).ToList();
		_stepMap = new(GenerateStepMap);
		WorkersCount = options.WorkersCount;
		StepOverheadDuration = options.StepOverheadDuration;
	}

	public Day07Solver(Action<Day07SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day07Solver() : this(Day07SolverOptions.Default)
	{
	}

	private StepMap GenerateStepMap()
	{
		StepMapBuilder builder = new();
		foreach ((char step, char requirement) in _instructions)
		{
			builder.AddStepRequirement(step, requirement);
		}
		return builder.Build();
	}

	public override string SolvePart1()
	{
		IEnumerable<Step> stepsInOrder = StepMap.GetAlphabeticalStepCompletionOrder();
		IEnumerable<char> stepLettersInOrder = stepsInOrder.Select(step => step.Letter);
		string result = string.Concat(stepLettersInOrder);
		return result;
	}

	public override string SolvePart2()
	{
		IEnumerable<StepWithFinishTime> stepsInOrder = StepMap.GetSimultaneousStepCompletionOrder(WorkersCount, StepOverheadDuration);
		StepWithFinishTime lastStep = stepsInOrder.Last();
		int result = lastStep.FinishTime;
		return result.ToString();
	}
}
