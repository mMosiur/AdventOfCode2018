using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day24;

static partial class InputParser
{
	[GeneratedRegex(@"^\s*(?<units>\d+) units each with (?<hitPoints>\d+) hit points (?:\((?<modifiers>.+)\) )?with an attack that does (?<attackDamage>\d+) (?<attackType>\w+) damage at initiative (?<initiative>\d+)\s*$", RegexOptions.Compiled)]
	private static partial Regex GroupRegex();
	[GeneratedRegex(@"^\s*(?<name>.+):\s*$", RegexOptions.Compiled)]
	private static partial Regex ArmyHeaderRegex();

	public static (Army Army1, Army Army2) Parse(IEnumerable<string> inputLines)
	{
		IEnumerator<string> it = inputLines.GetEnumerator();
		EnsureMoveNext(it, "Empty input.");
		while (string.IsNullOrWhiteSpace(it.Current))
		{
			EnsureMoveNext(it, "Empty input.");
		}
		Match match = ArmyHeaderRegex().Match(it.Current);
		if (!match.Success)
		{
			throw new FormatException($"Expected army header, got '{it.Current}'.");
		}
		string armyName1 = match.Groups["name"].Value;
		Army army1 = new(armyName1);
		SkipToNextNonEmptyLine(it, "No first army info in input.");
		AddArmyGroups(it, army1);
		SkipToNextNonEmptyLine(it, "No second army info in input.");
		match = ArmyHeaderRegex().Match(it.Current);
		if (!match.Success)
		{
			throw new FormatException($"Expected army header, got '{it.Current}'.");
		}
		string armyName2 = match.Groups["name"].Value;
		Army army2 = new(armyName2);
		SkipToNextNonEmptyLine(it, "No second army info in input.");
		AddArmyGroups(it, army2);
		return (army1, army2);
	}

	private static void EnsureMoveNext<T>(IEnumerator<T> it, string? message = null)
	{
		if (!it.MoveNext())
		{
			message ??= "Unexpected end of input.";
			throw new FormatException(message);
		}
	}

	private static void SkipToNextNonEmptyLine(IEnumerator<string> it, string? message = null)
	{
		EnsureMoveNext(it, message);
		while (string.IsNullOrWhiteSpace(it.Current))
		{
			EnsureMoveNext(it, message);
		}
	}

	private static bool AddArmyGroups(IEnumerator<string> it, Army army)
	{
		while (!string.IsNullOrWhiteSpace(it.Current))
		{
			Match match = GroupRegex().Match(it.Current);
			if (!match.Success)
			{
				throw new FormatException("Input line was not in the expected group format.");
			}
			(string[]? weaknesses, string[]? immunities) = ParseModifiers(match.Groups["modifiers"].Value);
			army.AddGroup(
				int.Parse(match.Groups["units"].ValueSpan),
				int.Parse(match.Groups["hitPoints"].ValueSpan),
				int.Parse(match.Groups["attackDamage"].ValueSpan),
				match.Groups["attackType"].Value,
				int.Parse(match.Groups["initiative"].ValueSpan),
				weaknesses,
				immunities
			);
			if (!it.MoveNext())
			{
				return false;
			}
		}
		return true;
	}

	private static (string[]? Weaknesses, string[]? Immunities) ParseModifiers(string s)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			return (null, null);
		}
		const string WEAKNESSES_START = "weak to ";
		string[]? weaknesses = null;
		const string IMMUNITIES_START = "immune to ";
		string[]? immunities = null;
		foreach (string part in s.Split(';', StringSplitOptions.TrimEntries))
		{
			if (part.StartsWith(WEAKNESSES_START))
			{
				if (weaknesses is not null)
				{
					throw new FormatException("Weaknesses defined multiple times.");
				}
				weaknesses = part[WEAKNESSES_START.Length..].Split(',', StringSplitOptions.TrimEntries);
			}
			else if (part.StartsWith(IMMUNITIES_START))
			{
				if (immunities is not null)
				{
					throw new FormatException("Immunities defined multiple times.");
				}
				immunities = part[IMMUNITIES_START.Length..].Split(',', StringSplitOptions.TrimEntries);
			}
			else
			{
				throw new FormatException($"Unknown modifiers: \"{part}\".");
			}
		}
		return (weaknesses, immunities);
	}
}
