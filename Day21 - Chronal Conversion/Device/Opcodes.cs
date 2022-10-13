using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2018.Day21.Device;

public static class Opcodes
{
	private static readonly Dictionary<string, Opcode> _opcodes = new()
	{
		["addr"] = Opcode.AddRegister,
		["addi"] = Opcode.AddImmediate,
		["mulr"] = Opcode.MultiplyRegister,
		["muli"] = Opcode.MultiplyImmediate,
		["banr"] = Opcode.BitwiseAndRegister,
		["bani"] = Opcode.BitwiseAndImmediate,
		["borr"] = Opcode.BitwiseOrRegister,
		["bori"] = Opcode.BitwiseOrImmediate,
		["setr"] = Opcode.SetRegister,
		["seti"] = Opcode.SetImmediate,
		["gtir"] = Opcode.GreaterThanImmediateRegister,
		["gtri"] = Opcode.GreaterThanRegisterImmediate,
		["gtrr"] = Opcode.GreaterThanRegisterRegister,
		["eqir"] = Opcode.EqualImmediateRegister,
		["eqri"] = Opcode.EqualRegisterImmediate,
		["eqrr"] = Opcode.EqualRegisterRegister,
	};

	public static Opcode Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		if (!_opcodes.TryGetValue(s, out Opcode opcode))
		{
			throw new FormatException($"Invalid opcode: '{s}'.");
		}
		return opcode;
	}

	public static bool TryParse(string s, [NotNullWhen(true)] out Opcode opcode)
	{
		ArgumentNullException.ThrowIfNull(s);
		return _opcodes.TryGetValue(s, out opcode);
	}
}
