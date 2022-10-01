namespace AdventOfCode.Year2018.Day18.LumberCollection;

public enum AcreContent : byte
{
	Unknown,

	OpenGround,
	Trees,
	Lumberyard,
}

public static class AcreContents
{
	public static AcreContent Parse(char c) => c switch
	{
		'.' => AcreContent.OpenGround,
		'|' => AcreContent.Trees,
		'#' => AcreContent.Lumberyard,
		_ => throw new ArgumentException($"Unknown acre content: '{c}'."),
	};

	public static char ToChar(this AcreContent content) => content switch
	{
		AcreContent.OpenGround => '.',
		AcreContent.Trees => '|',
		AcreContent.Lumberyard => '#',
		_ => throw new ArgumentException($"Unknown acre content: '{content}'."),
	};
}
