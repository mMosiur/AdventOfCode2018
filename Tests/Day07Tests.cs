using AdventOfCode.Year2018.Day07;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "07")]
[Trait("Day", "7")]
public class Day07Tests : BaseDayTests<Day07Solver>
{
	public override string DayInputsDirectory => "Day07";

	protected override Day07Solver CreateSolver(string inputFilePath) => new(options => options.InputFilepath = inputFilePath);

	[Theory]
	[InlineData("example-input.txt", "CABDFE")]
	[InlineData("my-input.txt", "BGJCNLQUYIFMOEZTADKSPVXRHW")]
	public override void TestPart1(string inputFilename, string expectedResult)
		=> base.TestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("my-input.txt", "1017")]
	public override void TestPart2(string inputFilename, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", 2, 0, "15")]
	[InlineData("my-input.txt", 5, 60, "1017")]
	public void TestPart2WithCustomData(string inputFilename, int workersCount, int stepOverheadDuration, string expectedResult)
	{
		string filepath = GetInputFilePath(inputFilename);
		Day07Solver solver = new(options =>
		{
			options.InputFilepath = filepath;
			options.WorkersCount = workersCount;
			options.StepOverheadDuration = stepOverheadDuration;
		});
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
