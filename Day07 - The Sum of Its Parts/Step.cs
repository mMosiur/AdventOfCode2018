namespace AdventOfCode.Year2018.Day07;

abstract class Step
{
	public char Letter { get; }

	public abstract IReadOnlySet<Step> Requirements { get; }

	public bool Finished { get; set; }

	public Step(char letter)
	{
		Letter = letter;
	}
}
