using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day07;

public class Day07Solver : DaySolver
{
	private readonly IReadOnlyList<Instruction> _instructions;

	public Day07Solver(string inputFilePath) : base(inputFilePath)
	{
		_instructions = InputLines.Select(Instruction.Parse).ToList();
	}

	public override string SolvePart1()
	{
		StepMapBuilder builder = new();
		foreach ((char step, char requirement) in _instructions)
		{
			builder.AddStepRequirement(step, requirement);
		}
		StepMap stepMap = builder.Build();
		IEnumerable<Step> stepsInOrder = stepMap.GetStepCompletionOrder();
		IEnumerable<char> stepLettersInOrder = stepsInOrder.Select(step => step.Letter);
		string result = string.Concat(stepLettersInOrder);
		return result;
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
