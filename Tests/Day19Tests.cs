using AdventOfCode.Year2018.Day19;
using AdventOfCode.Year2018.Day19.Device.CPUs;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "19")]
public class Day19Tests : BaseDayTests<Day19Solver, Day19SolverOptions>
{
	public override string DayInputsDirectory => "Day19";

	protected override Day19Solver CreateSolver(Day19SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "6")]
	[InlineData("my-input.txt", "1430")]
	public override void TestPart1(string inputFilename, string expectedResult, Day19SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt")]
	public void TestPart2FailsOnWrongInput(string inputFilename)
	{
		string inputFilepath = GetInputFilepath(inputFilename);
		Day19SolverOptions options = new() { InputFilepath = inputFilepath };
		Day19Solver solver = CreateSolver(options);
		Assert.Throws<RiggedCPUException>(() => solver.SolvePart2());
	}

	[Theory]
	[InlineData("my-input.txt", "14266944")]
	public override void TestPart2(string inputFilename, string expectedResult, Day19SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
