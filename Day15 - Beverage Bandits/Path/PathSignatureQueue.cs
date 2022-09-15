using System.Collections;

namespace AdventOfCode.Year2018.Day15.Path;

public class PathSignatureQueue : ICollection<PathSignature>
{
	private readonly SortedSet<PathSignature> _queue;

	public int Count => _queue.Count;

	public bool IsReadOnly => false;

	public PathSignatureQueue()
	{
		_queue = new();
	}

	public void Enqueue(PathSignature path)
	{
		_queue.Add(path);
	}

	public PathSignature Dequeue()
	{
		PathSignature path = _queue.First();
		_queue.Remove(path);
		return path;
	}

	public bool TryDequeue(out PathSignature path)
	{
		path = default;
		if (Count == 0)
		{
			return false;
		}
		path = Dequeue();
		return true;
	}

	public IEnumerator<PathSignature> GetEnumerator() => _queue.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public void Add(PathSignature path) => Enqueue(path);

	public void Clear() => _queue.Clear();

	public bool Contains(PathSignature path) => _queue.Contains(path);

	public void CopyTo(PathSignature[] array, int arrayIndex) => _queue.CopyTo(array, arrayIndex);

	public bool Remove(PathSignature path) => _queue.Remove(path);
}
