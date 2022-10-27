using System.Collections;
using AdventOfCode.Year2018.Day20.Geometry;

namespace AdventOfCode.Year2018.Day20;

class RoomDistances : IReadOnlyDictionary<Position, int>
{
	private readonly IReadOnlyDictionary<Position, int> _distances;

	public RoomDistances(IReadOnlyDictionary<Position, int> distances)
	{
		_distances = distances;
	}

	public int this[Position position] => _distances[position];

	public IEnumerable<Position> Keys => _distances.Keys;

	public IEnumerable<int> Values => _distances.Values;

	public int Count => _distances.Count;

	public bool ContainsKey(Position position) => _distances.ContainsKey(position);

	public IEnumerator<KeyValuePair<Position, int>> GetEnumerator() => _distances.GetEnumerator();

	public bool TryGetValue(Position position, out int distance) => _distances.TryGetValue(position, out distance);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
