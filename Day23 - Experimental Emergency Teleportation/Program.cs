using AdventOfCode;
using AdventOfCode.Year2018.Day23;

try
{
	string? filepath = args.Length switch
	{
		0 => null,
		1 => args[0],
		_ => throw new CommandLineException(
			$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath>]\"."
		)
	};

	Day23Solver solver = new(options =>
	{
		options.InputFilepath = filepath ?? options.InputFilepath;
	});

	Console.WriteLine($"Advent of Code {solver.Year}");
	Console.WriteLine($"--- Day {solver.Day}: {solver.Title} ---");

	Console.Write("Part one: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);

	Console.Write("Part two: ");
	string part2 = solver.SolvePart2();
	Console.WriteLine(part2);
}
catch (Exception e)
{
	string? errorPrefix = e switch
	{
		CommandLineException => "Command line error",
		InputException => "Input error",
		DaySolverException => "Day solver error",
		_ => null
	};
	if (errorPrefix is null)
	{
		throw;
	}
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"{errorPrefix}: {e.Message}");
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
