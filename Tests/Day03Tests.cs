using AdventOfCode.Year2018.Day03;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "03")]
[Trait("Day", "3")]
public class Day03Tests : BaseDayTests<Day03Solver>
{
	public override string DayInputsDirectory => "Day03";

	protected override Day03Solver CreateSolver(string inputFilePath) => new(inputFilePath);

	[Theory]
	[InlineData("example-input.txt", "4")]
	[InlineData("my-input.txt", "104439")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "3")]
	[InlineData("my-input.txt", "701")]
	public override void TestPart2(string inputFilename, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult);
}
