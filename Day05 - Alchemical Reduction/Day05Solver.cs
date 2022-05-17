using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day05;

public class Day05Solver : DaySolver
{
	private readonly Polymer _polymer;

	public Day05Solver(string inputFilePath) : base(inputFilePath)
	{
		_polymer = new Polymer(InputLines.Single());
	}

	public override string SolvePart1()
	{
		Stack<char> stack = new();
		foreach (char unit in _polymer)
		{
			if (stack.Count == 0)
			{
				stack.Push(unit);
				continue;
			}
			char topUnit = stack.Peek();
			PolymerUnitPolarity polarity = Polymer.GetUnitPolarity(topUnit, unit);
			if (polarity is PolymerUnitPolarity.Opposite)
			{
				stack.Pop();
			}
			else
			{
				stack.Push(unit);
			}
		}
		int result = stack.Count;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
