using AdventOfCode.Year2018.Day06;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "06")]
[Trait("Day", "6")]
public class Day06Tests : BaseDayTests<Day06Solver>
{
	public override string DayInputsDirectory => "Day06";

	protected override Day06Solver CreateSolver(string inputFilePath) => new(inputFilePath);

	[Theory]
	[InlineData("example-input.txt", "17")]
	[InlineData("my-input.txt", "2917")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);
}
