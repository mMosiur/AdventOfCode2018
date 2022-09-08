using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2018.Day15;

public class PathQueue : ICollection<Path>
{
	private readonly SortedSet<Path> _queue;

	public int Count => _queue.Count;

	public bool IsReadOnly => false;

	public PathQueue()
	{
		_queue = new();
	}

	public void Enqueue(Path path)
	{
		_queue.Add(path);
	}

	public Path Dequeue()
	{
		Path path = _queue.First();
		_queue.Remove(path);
		return path;
	}

	public bool TryDequeue([NotNullWhen(true)] out Path? path)
	{
		path = default;
		if (Count == 0)
		{
			return false;
		}
		path = Dequeue();
		return true;
	}

	public IEnumerator<Path> GetEnumerator() => _queue.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public void Add(Path path) => Enqueue(path);

	public void Clear() => _queue.Clear();

	public bool Contains(Path path) => _queue.Contains(path);

	public void CopyTo(Path[] array, int arrayIndex) => _queue.CopyTo(array, arrayIndex);

	public bool Remove(Path path) => _queue.Remove(path);
}
