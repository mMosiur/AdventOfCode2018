using AdventOfCode.Year2018.Day02;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "02")]
[Trait("Day", "2")]
public class Day02Tests : BaseDayTests<Day02Solver>
{
	public override string DayInputsDirectory => "Day02";

	protected override Day02Solver CreateSolver(string inputFilePath) => new Day02Solver(inputFilePath);

	[Theory]
	[InlineData("example-input-1.txt", "12")]
	[InlineData("my-input.txt", "6474")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input-2.txt", "fgij")]
	[InlineData("my-input.txt", "mxhwoglxgeauywfkztndcvjqr")]
	public override void TestPart2(string inputFilename, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult);
}
