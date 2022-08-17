using AdventOfCode.Year2018.Day12;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "12")]
public class Day12Tests : BaseDayTests<Day12Solver, Day12SolverOptions>
{
	public override string DayInputsDirectory => "Day12";

	protected override Day12Solver CreateSolver(Day12SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "325", true)]
	[InlineData("my-input.txt", "2040", false)]
	public void TestPart1WithCustomOptions(string inputFilename, string expectedResult, bool assumeMissingNotesProduceEmpty)
		=> TestPart1(inputFilename, expectedResult, new Day12SolverOptions() { AssumeMissingNotesProduceEmpty = assumeMissingNotesProduceEmpty });

	[Theory]
	[InlineData("my-input.txt", "2040")]
	public override void TestPart1(string inputFilename, string expectedResult, Day12SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
