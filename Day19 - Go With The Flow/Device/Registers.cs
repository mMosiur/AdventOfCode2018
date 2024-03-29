using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day19.Device;

class Registers : IReadOnlyRegisters, IEquatable<Registers>
{
	private const uint DEFAULT_REGISTER_VALUE = 0;
	private static readonly Regex _regex = new(@"\[((?>\d+)(?>, (?>\d+))*)\]", RegexOptions.Compiled);


	private readonly uint[] _registers;

	public int Count => _registers.Length;

	public Registers(int registerCount) : this(registerCount, DEFAULT_REGISTER_VALUE) { }

	public Registers(int registerCount, uint defaultValue)
	{
		_registers = new uint[registerCount];
		Array.Fill(_registers, defaultValue);
	}

	public Registers(uint[] registers)
	{
		_registers = (uint[])registers.Clone();
	}

	public Registers(Registers registers) : this(registers._registers)
	{
	}

	public uint this[int index]
	{
		get => _registers[index];
		set => _registers[index] = value;
	}

	public void Clear()
	{
		Array.Fill(_registers, DEFAULT_REGISTER_VALUE);
	}

	public override string ToString()
	{
		return $"[{string.Join(", ", _registers)}]";
	}

	public static Registers Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		try
		{
			Match match = _regex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid register format.");
			}
			string[] values = match.Groups[1].Value.Split(",");
			uint[] registers = new uint[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				if (!uint.TryParse(values[i], out uint value))
				{
					throw new FormatException($"Invalid register value: '{values[i]}'.");
				}
				registers[i] = value;
			}
			return new Registers(registers);
		}
		catch (FormatException e)
		{
			throw new FormatException($"Invalid register: '{s}'", e);
		}
	}

	public IEnumerator<uint> GetEnumerator() => ((IEnumerable<uint>)_registers).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => _registers.GetEnumerator();

	public bool Equals(Registers? other)
	{
		if (other is null)
		{
			return false;
		}
		if (ReferenceEquals(this, other))
		{
			return true;
		}
		if (Count != other.Count)
		{
			return false;
		}
		return _registers.SequenceEqual(other._registers);
	}

	public override bool Equals(object? obj) => Equals(obj as Registers);

	public static bool operator ==(Registers? left, Registers? right) => Equals(left, right);

	public static bool operator !=(Registers? left, Registers? right) => !Equals(left, right);

	public override int GetHashCode()
	{
		uint hashCode = (uint)HashCode.Combine(Count);
		foreach (uint register in _registers)
		{
			hashCode |= register;
		}
		return (int)hashCode;
	}
}
