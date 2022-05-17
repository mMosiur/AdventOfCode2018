using AdventOfCode.Year2018.Day05;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "05")]
[Trait("Day", "5")]
public class Day05Tests : BaseDayTests<Day05Solver>
{
	public override string DayInputsDirectory => "Day05";

	protected override Day05Solver CreateSolver(string inputFilePath) => new(inputFilePath);

	[Theory]
	[InlineData("example-input.txt", "10")]
	[InlineData("my-input.txt", "10584")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "4")]
	public override void TestPart2(string inputFilename, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult);
}
