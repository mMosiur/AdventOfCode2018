using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day07;

record struct Instruction(char StepLetter, char RequiredStepLetter)
{
	private static readonly Regex Regex = new(@"^\s*Step (?<requirement>\w) must be finished before step (?<step>\w) can begin.\s*$", RegexOptions.IgnoreCase);

	public static Instruction Parse(string line)
	{
		Match match = Regex.Match(line);
		if (!match.Success)
		{
			throw new ApplicationException($"Invalid input line: \"{line}\"");
		}
		char requirement = char.ToUpperInvariant(match.Groups["requirement"].ValueSpan[0]);
		char step = char.ToUpperInvariant(match.Groups["step"].ValueSpan[0]);
		return new(step, requirement);
	}
}
