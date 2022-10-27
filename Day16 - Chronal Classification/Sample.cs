using System.Text.RegularExpressions;
using AdventOfCode.Year2018.Day16.Device;

namespace AdventOfCode.Year2018.Day16;

class Sample
{
	private static readonly Regex _regex = new(@"[ \t]*Before:[ \t]*(.+)\n[ \t]*(.+)\nAfter:[ \t]*(.+)[ \t]*\n?", RegexOptions.Compiled);

	public Registers RegistersBeforeOperation { get; }
	public Instruction Operation { get; }
	public Registers RegistersAfterOperation { get; }

	public Sample(Registers registersBeforeOperation, Instruction operation, Registers registersAfterOperation)
	{
		ArgumentNullException.ThrowIfNull(registersBeforeOperation);
		ArgumentNullException.ThrowIfNull(registersAfterOperation);
		if (registersBeforeOperation.Count != registersAfterOperation.Count)
		{
			throw new ArgumentException($"The number of registers before the operation must be equal to the number of registers after the operation (got {registersBeforeOperation.Count} and {registersAfterOperation.Count} respectively).");
		}
		RegistersBeforeOperation = registersBeforeOperation;
		Operation = operation;
		RegistersAfterOperation = registersAfterOperation;
	}

	public static Sample Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		try
		{
			Match match = _regex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid sample format.");
			}
			Registers registersBeforeOperation = Registers.Parse(match.Groups[1].Value);
			Instruction operation = Instruction.Parse(match.Groups[2].Value);
			Registers registersAfterOperation = Registers.Parse(match.Groups[3].Value);
			return new Sample(registersBeforeOperation, operation, registersAfterOperation);
		}
		catch (FormatException e)
		{
			throw new FormatException($"Invalid sample: '{s}'", e);
		}
	}
}
