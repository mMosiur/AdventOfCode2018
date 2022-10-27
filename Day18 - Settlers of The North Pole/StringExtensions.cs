namespace AdventOfCode.Year2018.Day18.StringExtensions;

static class StringExtensions
{
	public static IEnumerable<string> EnumerateLines(this string text)
	{
		using StringReader reader = new(text);
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			yield return line;
		}
	}
}
