using AdventOfCode.Year2018.Day07;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "07")]
[Trait("Day", "7")]
public class Day07Tests : BaseDayTests<Day07Solver>
{
	public override string DayInputsDirectory => "Day07";

	protected override Day07Solver CreateSolver(string inputFilePath) => new(inputFilePath);

	[Theory]
	[InlineData("example-input.txt", "CABDFE")]
	[InlineData("my-input.txt", "BGJCNLQUYIFMOEZTADKSPVXRHW")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);
}
