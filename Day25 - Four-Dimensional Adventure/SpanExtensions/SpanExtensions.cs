namespace AdventOfCode.Year2018.Day25.SpanExtensions;

static class SpanExtensions
{
	public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> span, T separator) where T : IEquatable<T>
	{
		return new SpanSplitEnumerator<T>(span, separator);
	}
}

ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
{
	private readonly ReadOnlySpan<T> _span;
	private ReadOnlySpan<T> _current;
	private ReadOnlySpan<T> _rest;
	private bool _finished;
	private readonly T _separator;

	public SpanSplitEnumerator(ReadOnlySpan<T> span, T separator)
	{
		_span = span;
		_current = default;
		_rest = span;
		_finished = false;
		_separator = separator;
	}

	public bool MoveNext()
	{
		if (_finished) return false;
		int sepIndex = _rest.IndexOf(_separator);
		if (sepIndex == -1)
		{
			_current = _rest;
			_rest = default;
			_finished = true;
			return true;
		}
		_current = _rest[..sepIndex];
		_rest = _rest[(sepIndex + 1)..];
		return true;
	}

	public void Reset()
	{
		_current = default;
		_rest = _span;
		_finished = false;
	}

	public ReadOnlySpan<T> Current => _current;
}
