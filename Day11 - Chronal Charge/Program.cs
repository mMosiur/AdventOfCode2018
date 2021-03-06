using AdventOfCode.Year2018.Day11;

try
{
	string? argument = args.Length switch
	{
		0 => null,
		1 => args[0],
		_ => throw new ApplicationException(
			$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath>]\"."
		)
	};

	var solver = new Day11Solver(options =>
	{
		if (argument is not null && int.TryParse(argument, out int gridSerialNumber))
		{
			options.GridSerialNumber = gridSerialNumber;
		}
		else
		{
			options.InputFilepath = argument ?? options.InputFilepath;
		}
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
