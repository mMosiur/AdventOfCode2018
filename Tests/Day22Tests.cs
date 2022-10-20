using AdventOfCode.Year2018.Day22;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "22")]
public class Day22Tests : BaseDayTests<Day22Solver, Day22SolverOptions>
{
	public override string DayInputsDirectory => "Day22";

	protected override Day22Solver CreateSolver(Day22SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "114")]
	[InlineData("my-input.txt", "4479")]
	public override void TestPart1(string inputFilename, string expectedResult, Day22SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
