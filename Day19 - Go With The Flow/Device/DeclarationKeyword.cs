namespace AdventOfCode.Year2018.Day19.Device;

static class DeclarationKeyword
{
	public const string BindInstructionPointerKeyword = "ip";

	private static readonly Dictionary<string, DeclarationType> _keywords = new()
	{
		[BindInstructionPointerKeyword] = DeclarationType.BindInstructionPointer,
	};

	public static DeclarationType Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		if (_keywords.TryGetValue(s, out DeclarationType type))
		{
			return type;
		}
		throw new FormatException($"Invalid declaration keyword: '{s}'.");
	}
}
