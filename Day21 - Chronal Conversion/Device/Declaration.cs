using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day21.Device;

public readonly struct Declaration
{
	private static readonly Regex _regex = new(@"^[ \t]*#[ \t]*([a-z]+)[ \t]+(\d+)[ \t]*$", RegexOptions.Compiled);

	public Declaration(DeclarationType type, byte value)
	{
		Type = type;
		Value = value;
	}

	public DeclarationType Type { get; }
	public byte Value { get; }

	public static Declaration Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		Match match = _regex.Match(s);
		if (!match.Success)
		{
			throw new FormatException($"Invalid declaration format.");
		}
		string keyword = match.Groups[1].Value;
		DeclarationType type = DeclarationKeyword.Parse(keyword);
		if (!byte.TryParse(match.Groups[2].ValueSpan, out byte value))
		{
			throw new FormatException($"Invalid declaration value: '{match.Groups[2].ValueSpan}'.");
		}
		return new Declaration(type, value);
	}
}
