using AdventOfCode;
using AdventOfCode.Year2018.Day06;

try
{
	string? filepath;
	int? maxTotalDistance;
	switch (args.Length)
	{
		case 0:
			filepath = null;
			maxTotalDistance = null;
			break;
		case 1:
			filepath = args[0];
			maxTotalDistance = null;
			break;
		case 2:
			filepath = args[0];
			try
			{
				maxTotalDistance = (int)uint.Parse(args[1]);
			}
			catch (FormatException e)
			{
				throw new CommandLineException(
					$"Invalid argument: second argument should be non-negative integers, and was \"{args[1]}\".",
					innerException: e
				);
			}
			break;
		default:
			throw new CommandLineException(
				$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath> [<max total distance>]]\"."
			);
	}

	Day06Solver solver = new(options =>
	{
		options.InputFilepath = filepath ?? options.InputFilepath;
		options.MaxTotalDistance = maxTotalDistance ?? options.MaxTotalDistance;
	});

	Console.WriteLine($"Advent of Code {solver.Year}");
	Console.WriteLine($"--- Day {solver.Day}: {solver.Title} ---");

	Console.Write("Part 1: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);

	Console.Write("Part 2: ");
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
