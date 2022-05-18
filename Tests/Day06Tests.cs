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

	protected Day06Solver CreateSolver(string inputFilePath, int maxTotalDistance) => new(inputFilePath, maxTotalDistance);

	[Theory]
	[InlineData("example-input.txt", "17")]
	[InlineData("my-input.txt", "2917")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("my-input.txt", "44202")]
	public override void TestPart2(string inputFilename, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult);

	[Theory]
	[InlineData("my-input.txt", 9999, "44202")]
	[InlineData("example-input.txt", 31, "16")]
	public void TestPart2WithCustomMaxTotalDistance(string inputFilename, int maxTotalDistance, string expectedResult)
	{
		string filepath = GetInputFilePath(inputFilename);
		Day06Solver solver = CreateSolver(filepath, maxTotalDistance);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
