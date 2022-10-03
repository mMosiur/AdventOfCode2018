namespace AdventOfCode.Year2018.Day19.Device;

public class Program
{
	public IReadOnlyList<Declaration> Declarations { get; }
	public IReadOnlyList<Instruction> Instructions { get; }

	private Program(List<Declaration> declarations, List<Instruction> instructions)
	{
		declarations.TrimExcess();
		Declarations = declarations;
		instructions.TrimExcess();
		Instructions = instructions;
	}

	public Program(IEnumerable<Declaration> declarations, IEnumerable<Instruction> instructions)
	{
		Declarations = declarations.ToArray();
		Instructions = instructions.ToArray();
	}

	public Program(IEnumerable<Instruction> instructions)
	{
		Declarations = Array.Empty<Declaration>();
		Instructions = instructions.ToArray();
	}

	public static Program Parse(string s)
	{
		StringReader reader = new(s);
		string? line = reader.ReadLine();
		if (line is null)
		{
			throw new FormatException("Invalid input: no instructions.");
		}
		while (line == string.Empty)
		{
			line = reader.ReadLine();
			if (line is null)
			{
				throw new FormatException("Invalid input: no instructions.");
			}
		}
		List<Declaration> declarations = new();
		while (line.TrimStart().StartsWith('#'))
		{
			declarations.Add(Declaration.Parse(line));
			line = reader.ReadLine();
			if (line is null)
			{
				throw new FormatException("Invalid input: no instructions.");
			}
		}
		while (line == string.Empty)
		{
			line = reader.ReadLine();
			if (line is null)
			{
				throw new FormatException("Invalid input: no instructions.");
			}
		}
		List<Instruction> instructions = new();
		while (!string.IsNullOrEmpty(line))
		{
			instructions.Add(Instruction.Parse(line));
			line = reader.ReadLine();
		}
		return new Program(declarations, instructions);
	}
}
