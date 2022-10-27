using System.Collections;

namespace AdventOfCode.Year2018.Day05;

class Polymer : IReadOnlyList<char>
{
	private readonly string _polymerUnits;

	public Polymer(IEnumerable<char> polymerUnits)
	{
		if (!polymerUnits.All(c => char.IsAscii(c) && char.IsLetter(c)))
		{
			throw new ArgumentException("Polymer units must be ASCII letters.", nameof(polymerUnits));
		}
		_polymerUnits = string.Concat(polymerUnits);
	}

	public char this[int index] => _polymerUnits[index];

	public int Count => _polymerUnits.Length;

	public IEnumerator<char> GetEnumerator() => _polymerUnits.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public Polymer GetReactionResult()
	{
		Stack<char> unitStack = new();
		foreach (char unit in this)
		{
			if (unitStack.Count == 0)
			{
				unitStack.Push(unit);
				continue;
			}
			char topUnit = unitStack.Peek();
			PolymerUnitPolarity polarity = GetUnitPolarity(topUnit, unit);
			if (polarity is PolymerUnitPolarity.Opposite)
			{
				unitStack.Pop();
			}
			else
			{
				unitStack.Push(unit);
			}
		}
		Polymer result = new(unitStack.Reverse());
		return result;
	}

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
