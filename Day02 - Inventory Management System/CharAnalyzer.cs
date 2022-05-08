namespace AdventOfCode.Year2018.Day02;

public class CharAnalyzer
{
	private IEnumerable<char>? _chars;
	private Dictionary<int, IEnumerable<char>>? _charsByCount;

	public IEnumerable<char> Chars
	{
		get => _chars ?? throw new InvalidOperationException("Chars not set");
		set
		{
			_chars = value ?? throw new ArgumentNullException(nameof(value));
			_charsByCount = null;
		}
	}

	public Dictionary<int, IEnumerable<char>> CharsByCount
		=> _charsByCount
			??= Chars
				.GroupBy(c => c)
				.GroupBy(g => g.Count(), g => g.Key)
				.ToDictionary(g => g.Key, g => g.AsEnumerable());

	public CharAnalyzer() { }

	public CharAnalyzer(IEnumerable<char> chars)
	{
		Chars = chars;
	}

	public IEnumerable<char> GetLettersThatAppearNTimes(int n)
		=> CharsByCount.GetValueOrDefault(n) ?? Enumerable.Empty<char>();

	public IEnumerable<int> GetPositionsOfDifferences(IEnumerable<char> other)
	{
		var result = Enumerable.Zip(Chars, other)
			.Select((pair, index) => (pair.First, pair.Second, Index: index))
			.Where(pair => pair.First != pair.Second)
			.Select(pair => pair.Index);
		var thisCharsAfterCount = Chars.Select((c, i) => i).Skip(other.Count());
		if (thisCharsAfterCount.Any())
		{
			result = result.Concat(thisCharsAfterCount);
		}
		var otherCharsAfterCount = other.Select((c, i) => i).Skip(Chars.Count());
		if (otherCharsAfterCount.Any())
		{
			result = result.Concat(otherCharsAfterCount);
		}
		return result;
	}
}
