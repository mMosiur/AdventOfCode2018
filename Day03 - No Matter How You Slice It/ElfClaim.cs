using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day03;

record struct ElfClaim(int Id, int Left, int Top, int Width, int Height)
{
	private static readonly Regex Regex = new Regex(@"\s*\#(\d+)\s*\@\s*(\d+)\s*,\s*(\d+)\s*:\s*(\d+)\s*x\s*(\d+)\s*", RegexOptions.Compiled);

	public static ElfClaim Parse(string s)
	{
		var match = Regex.Match(s);
		if (!match.Success)
		{
			throw new FormatException($"Failed to parse {s}");
		}
		int id = int.Parse(match.Groups[1].ValueSpan);
		int left = int.Parse(match.Groups[2].ValueSpan);
		int top = int.Parse(match.Groups[3].ValueSpan);
		int width = int.Parse(match.Groups[4].ValueSpan);
		int height = int.Parse(match.Groups[5].ValueSpan);
		return new ElfClaim(id, left, top, width, height);
	}
}
