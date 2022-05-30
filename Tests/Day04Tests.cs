using AdventOfCode.Year2018.Day04;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "04")]
[Trait("Day", "4")]
public class Day04Tests : BaseDayTests<Day04Solver>
{
	public override string DayInputsDirectory => "Day04";

	protected override Day04Solver CreateSolver(string inputFilePath) => new(options => options.InputFilepath = inputFilePath);

	[Theory]
	[InlineData("example-input.txt", "240")]
	[InlineData("my-input.txt", "118599")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "4455")]
	[InlineData("my-input.txt", "33949")]
	public override void TestPart2(string inputFilename, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult);
}
