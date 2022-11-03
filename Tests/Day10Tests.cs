using AdventOfCode.Year2018.Day10;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "10")]
public class Day10Tests : BaseDayTests<Day10Solver, Day10SolverOptions>
{
	public override string DayInputsDirectory => "Day10";

	protected override Day10Solver CreateSolver(Day10SolverOptions options) => new(options);

	public static IEnumerable<object[]> TestPart1Data =>
		new List<object[]>
		{
			new string[]
			{
				"example-input.txt",
				string.Join(Environment.NewLine,
					"#...#..###",
					"#...#...#.",
					"#...#...#.",
					"#####...#.",
					"#...#...#.",
					"#...#...#.",
					"#...#...#.",
					"#...#..###"
				),
			},
			new string[]
			{
				"my-input.txt",
				string.Join(Environment.NewLine,
					"..##....#....#..######...####...#####...#....#..######..######",
					".#..#...#....#..#.......#....#..#....#..#...#...#.......#.....",
					"#....#..#....#..#.......#.......#....#..#..#....#.......#.....",
					"#....#..#....#..#.......#.......#....#..#.#.....#.......#.....",
					"#....#..######..#####...#.......#####...##......#####...#####.",
					"######..#....#..#.......#..###..#..#....##......#.......#.....",
					"#....#..#....#..#.......#....#..#...#...#.#.....#.......#.....",
					"#....#..#....#..#.......#....#..#...#...#..#....#.......#.....",
					"#....#..#....#..#.......#...##..#....#..#...#...#.......#.....",
					"#....#..#....#..#........###.#..#....#..#....#..######..######"
				),
			},
		};

	[Theory]
	[MemberData(nameof(TestPart1Data))]
	public override void TestPart1(string inputFilename, string expectedResult, Day10SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "3")]
	[InlineData("my-input.txt", "10243")]
	public override void TestPart2(string inputFilename, string expectedResult, Day10SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
