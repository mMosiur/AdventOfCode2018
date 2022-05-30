using AdventOfCode.Year2018.Day07;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "07")]
[Trait("Day", "7")]
public class Day07Tests : BaseDayTests<Day07Solver, Day07SolverOptions>
{
	public override string DayInputsDirectory => "Day07";

	protected override Day07Solver CreateSolver(Day07SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "CABDFE")]
	[InlineData("my-input.txt", "BGJCNLQUYIFMOEZTADKSPVXRHW")]
	public override void TestPart1(string inputFilename, string expectedResult, Day07SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("my-input.txt", "1017")]
	public override void TestPart2(string inputFilename, string expectedResult, Day07SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", 2, 0, "15")]
	[InlineData("my-input.txt", 5, 60, "1017")]
	public void TestPart2WithCustomOptions(string inputFilename, int workersCount, int stepOverheadDuration, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult, new Day07SolverOptions()
		{
			WorkersCount = workersCount,
			StepOverheadDuration = stepOverheadDuration
		});
}
