using System.Collections;

namespace AdventOfCode.Year2018.Day25;

class Constellation : IEnumerable<ConstellationPoint>
{
	public ConstellationPoint? First { get; private set; }
	public ConstellationPoint? Last { get; private set; }

	public Constellation(ConstellationPoint point)
	{
		ArgumentNullException.ThrowIfNull(point);
		if (point.Constellation is not null)
		{
			throw new InvalidOperationException("Point already belongs to constellation");
		}
		point.Constellation = this;
		First = point;
		Last = point;
	}

	public void MergeFrom(Constellation other)
	{
		ArgumentNullException.ThrowIfNull(other);
		if (other.First is null)
		{
			return;
		}
		foreach (ConstellationPoint point in other)
		{
			point.Constellation = this;
		}
		if (Last is null)
		{
			First = other.First;
			Last = other.Last;
		}
		else
		{
			Last.Next = other.First;
			other.First.Previous = Last;
			Last = other.Last;
		}
		other.First = null;
		other.Last = null;
	}

	public IEnumerator<ConstellationPoint> GetEnumerator()
	{
		ConstellationPoint? node = First;
		while (node is not null)
		{
			yield return node;
			node = node.Next;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
