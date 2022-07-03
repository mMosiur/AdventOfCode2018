using AdventOfCode.Year2018.Day11;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "11")]
public class Day11Tests : BaseDayTests<Day11Solver, Day11SolverOptions>
{
	public override string DayInputsDirectory => "Day11";

	protected override Day11Solver CreateSolver(Day11SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "33,45")]
	[InlineData("example-input-2.txt", "21,61")]
	[InlineData("my-input.txt", "235,20")]
	public override void TestPart1(string inputFilename, string expectedResult, Day11SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData(3, 5, 8, 4)]
	[InlineData(122, 79, 57, -5)]
	[InlineData(217, 196, 39, 0)]
	[InlineData(101, 153, 71, 4)]
	public void TestPowerLevelCalculation(int x, int y, int gridSerialNumber, int expectedPowerLevel)
	{
		FuelCellGrid grid = new(gridSerialNumber);
		int cellPower = grid.CalculateFuelCellPowerLevel(x, y);
		Assert.Equal(expectedPowerLevel, cellPower);
	}
}
