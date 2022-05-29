namespace AdventOfCode.Year2018.Day07;

public abstract class Step
{
	public char Letter { get; }

	public abstract IReadOnlySet<Step> Requirements { get; }

	public bool Finished { get; set; }

	public Step(char letter, bool finished = false)
	{
		Letter = letter;
		Finished = finished;
	}
}
