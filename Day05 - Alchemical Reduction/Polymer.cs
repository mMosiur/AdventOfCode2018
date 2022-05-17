using System.Collections;

namespace AdventOfCode.Year2018.Day05;

public class Polymer : IReadOnlyList<char>
{
	private readonly string _polymerUnits;

	public Polymer(string polymerUnits)
	{
		if (string.IsNullOrEmpty(polymerUnits))
		{
			throw new ArgumentException("Polymer units cannot be null or empty.", nameof(polymerUnits));
		}
		if (!polymerUnits.All(c => char.IsAscii(c) && char.IsLetter(c)))
		{
			throw new ArgumentException("Polymer units must be ASCII letters.", nameof(polymerUnits));
		}
		_polymerUnits = polymerUnits;
	}

	public char this[int index] => _polymerUnits[index];

	public int Count => _polymerUnits.Length;

	public IEnumerator<char> GetEnumerator() => _polymerUnits.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public static PolymerUnitPolarity GetUnitPolarity(char unitA, char unitB)
	{
		if (!char.IsAscii(unitA) || !char.IsLetter(unitA))
		{
			throw new ArgumentException("A polymer unit a must be an ASCII letter.", nameof(unitA));
		}
		if (!char.IsAscii(unitB) || !char.IsLetter(unitB))
		{
			throw new ArgumentException("A polymer unit b must be an ASCII letter.", nameof(unitB));
		}
		int diff = Math.Abs(unitA - unitB);
		return diff switch
		{
			0 => PolymerUnitPolarity.Same,
			32 => PolymerUnitPolarity.Opposite,
			_ => PolymerUnitPolarity.None,
		};
	}
}
