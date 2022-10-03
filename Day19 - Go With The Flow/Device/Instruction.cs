using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day19.Device;

public readonly record struct Instruction(Opcode Opcode, byte A, byte B, byte C)
{
	private static readonly Regex _regex = new(@"^[ \t]*([a-z]+) (\d+) (\d+) (\d+)[ \t]*$", RegexOptions.Compiled);

	public static Instruction Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		try
		{
			Match match = _regex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid instruction format.");
			}
			if (!Opcodes.TryParse(match.Groups[1].Value, out Opcode opcode))
			{
				throw new FormatException($"Invalid opcode: '{match.Groups[1].ValueSpan}'.");
			}
			if (!byte.TryParse(match.Groups[2].ValueSpan, out byte a))
			{
				throw new FormatException($"Invalid instruction value A: '{match.Groups[2].ValueSpan}'.");
			}
			if (!byte.TryParse(match.Groups[3].ValueSpan, out byte b))
			{
				throw new FormatException($"Invalid instruction value B: '{match.Groups[3].ValueSpan}'.");
			}
			if (!byte.TryParse(match.Groups[4].ValueSpan, out byte c))
			{
				throw new FormatException($"Invalid instruction value C: '{match.Groups[4].ValueSpan}'.");
			}
			return new Instruction(opcode, a, b, c);
		}
		catch (FormatException e)
		{
			throw new FormatException($"Invalid instruction: '{s}'", e);
		}
	}
}
