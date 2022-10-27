namespace AdventOfCode.Year2018.Day16;

abstract class OpcodeDictionary
{
	public static readonly IReadOnlyCollection<string> OpcodeNames = new string[]
	{
		"addr", "addi",         // Addition
		"mulr", "muli",         // Multiplication
		"banr", "bani",         // Bitwise AND
		"borr", "bori",         // Bitwise OR
		"setr", "seti",         // Assignment
		"gtir", "gtri", "gtrr", // Greater-than testing
		"eqir", "eqri", "eqrr", // Equality testing
	};

	public abstract IReadOnlyDictionary<byte, string> OpcodeNumberToName { get; }

	public abstract IReadOnlyDictionary<string, byte> OpcodeNameToNumber { get; }
}
