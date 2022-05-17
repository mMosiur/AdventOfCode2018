namespace AdventOfCode.Year2018.Day05;

public static class Helpers
{
	public static IEnumerable<(char Lower, char Upper)> GetAsciiAlphabetCasePair()
	{
		for(char lower = 'a'; lower <= 'z'; lower++)
		{
			char upper = (char)(lower + 32);
			yield return (lower, upper);
		}
	}
}
