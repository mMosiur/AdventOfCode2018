using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day12;

struct PotTransformNote
{
	private readonly PotState[] _from;
	private readonly PotState _to;

	public ReadOnlySpan<PotState> From => _from;
	public PotState To => _to;

	public PotTransformNote(PotState[] from, PotState to)
	{
		_from = from;
		_to = to;
	}

	private static readonly Regex Regex = new(@"^\s*(?<from>[#\.]+)\s*=>\s*(?<to>[#\.])\s*$", RegexOptions.Compiled);

	public static PotTransformNote Parse(string note)
	{
		Match match = Regex.Match(note);
		if (!match.Success)
		{
			throw new ArgumentException($"Could not parse note \"{note}\"");
		}
		ReadOnlySpan<char> fromSpan = match.Groups["from"].ValueSpan;
		PotState[] from = new PotState[fromSpan.Length];
		for (int i = 0; i < fromSpan.Length; i++)
		{
			from[i] = fromSpan[i] switch
			{
				'#' => PotState.Plant,
				'.' => PotState.Empty,
				_ => throw new ArgumentException($"Invalid pot state: {fromSpan[i]}")
			};
		}
		ReadOnlySpan<char> toSpan = match.Groups["to"].ValueSpan;
		PotState to = toSpan[0] switch
		{
			'#' => PotState.Plant,
			'.' => PotState.Empty,
			_ => throw new ArgumentException($"Invalid pot state: {toSpan[0]}")
		};
		return new PotTransformNote(from, to);
	}
}
