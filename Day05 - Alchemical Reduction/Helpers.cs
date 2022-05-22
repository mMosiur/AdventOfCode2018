namespace AdventOfCode.Year2018.Day05;

public static class Helpers
{
	public static IEnumerable<(char Lower, char Upper)> GetAsciiAlphabetCasePairs()
	{
		for (char upper = 'A'; upper <= 'Z'; upper++)
		{
			char lower = (char)(upper + 32);
			yield return (lower, upper);
		}
	}
}
