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
				throw new ApplicationException(
					$"Invalid argument: second argument should be non-negative integers, and was \"{args[1]}\".",
					innerException: e
				);
			}
			break;
		default:
			throw new ApplicationException(
				$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath> [<max total distance>]]\"."
			);
	}

	var solver = new Day06Solver(options =>
	{
		options.InputFilepath = filepath ?? options.InputFilepath;
		options.MaxTotalDistance = maxTotalDistance ?? options.MaxTotalDistance;
	});

	Console.Write("Part 1: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);

	Console.Write("Part 2: ");
	string part2 = solver.SolvePart2();
	Console.WriteLine(part2);
}
catch (FileNotFoundException e)
{
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine(e.Message);
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
catch (ApplicationException e)
{
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"Error: {e.Message}");
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
