using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day10;

public sealed class Day10SolverOptions : DaySolverOptions
{
	public char EmptySkyRepresentation { get; set; } = '.';
	public char StarInSkyRepresentation { get; set; } = '#';
	public int MaxSkyAreToDisplay { get; set; } = 2000;
}
