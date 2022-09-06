using AdventOfCode.Year2018.Day14;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "14")]
[Trait("Day", "9")]
public class Day14Tests : BaseDayTests<Day14Solver, Day14SolverOptions>
{
	public override string DayInputsDirectory => "Day14";

	protected override Day14Solver CreateSolver(Day14SolverOptions options) => new(options);

	[Theory]
	[InlineData("my-input.txt", "3841138812")]
	public override void TestPart1(string inputFilename, string expectedResult, Day14SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("9", "5158916779")]
	[InlineData("5", "0124515891")]
	[InlineData("18", "9251071085")]
	[InlineData("2018", "5941429882")]
	[InlineData("990941", "3841138812")]
	public void TestPart1Numeric(string input, string expectedResult)
	{
		Day14SolverOptions options = new()
		{
			InputReader = new StringReader(input)
		};
		Day14Solver solver = new(options);
		string actualResult = solver.SolvePart1();
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData("my-input.txt", "20200561")]
	public override void TestPart2(string inputFilename, string expectedResult, Day14SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("51589", "9")]
	[InlineData("01245", "5")]
	[InlineData("92510", "18")]
	[InlineData("59414", "2018")]
	[InlineData("990941", "20200561")]
	public void TestPart2Numeric(string input, string expectedResult)
	{
		Day14SolverOptions options = new()
		{
			InputReader = new StringReader(input)
		};
		Day14Solver solver = new(options);
		string actualResult = solver.SolvePart2();
		Assert.Equal(expectedResult, actualResult);
	}
}
