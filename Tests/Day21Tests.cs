using AdventOfCode.Year2018.Day21;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "21")]
public class Day21Tests : BaseDayTests<Day21Solver, Day21SolverOptions>
{
	public override string DayInputsDirectory => "Day21";

	protected override Day21Solver CreateSolver(Day21SolverOptions options) => new(options);

	[Theory]
	[InlineData("my-input.txt", "212115")]
	public override void TestPart1(string inputFilename, string expectedResult, Day21SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("my-input.txt", "9258470")]
	public override void TestPart2(string inputFilename, string expectedResult, Day21SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
