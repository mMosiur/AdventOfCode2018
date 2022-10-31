using AdventOfCode;
using AdventOfCode.Year2018.Day07;

try
{
	string? filepath;
	int? workersCount;
	int? stepOverheadDuration;
	switch (args.Length)
	{
		case 0:
			filepath = null;
			workersCount = null;
			stepOverheadDuration = null;
			break;
		case 1:
			filepath = args[0];
			workersCount = null;
			stepOverheadDuration = null;
			break;
		case 3:
			filepath = args[0];
			try
			{
				workersCount = (int)uint.Parse(args[1]);
				stepOverheadDuration = (int)uint.Parse(args[2]);
			}
			catch (FormatException e)
			{
				throw new CommandLineException(
					$"Invalid arguments: second and third argument should be non-negative integers, and were \"{args[1]}\" and \"{args[2]}\".",
					innerException: e
				);
			}
			break;
		default:
			throw new CommandLineException(
				$"Program was improperly called. Proper usage: \"dotnet run [<input filepath>]\" or \"dotnet run <input filepath> <workers count> <step overhead>\"."
			);

	}

	Day07Solver solver = new(options =>
	{
		options.InputFilepath = filepath ?? options.InputFilepath;
		options.WorkersCount = workersCount ?? options.WorkersCount;
		options.StepOverheadDuration = stepOverheadDuration ?? options.StepOverheadDuration;
	});

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
