using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day03;

public sealed class Day03Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 3;
	public override string Title => "No Matter How You Slice It";

	private readonly IReadOnlyList<ElfClaim> _claims;

	public Day03Solver(Day03SolverOptions options) : base(options)
	{
		_claims = InputLines.Select(ElfClaim.Parse).ToList();
	}

	public Day03Solver(Action<Day03SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day03Solver() : this(new Day03SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		Dictionary<Point, int> claimMap = new();
		foreach (ElfClaim claim in _claims)
		{
			foreach (Point point in claim.GetPoints())
			{
				claimMap.TryGetValue(point, out int claimCountAtThatPoint);
				claimMap[point] = claimCountAtThatPoint + 1;
			}
		}
		int pointsWithOverlapCount = claimMap.Values.Count(v => v > 1);
		return pointsWithOverlapCount.ToString();
	}

	public override string SolvePart2()
	{
		Dictionary<Point, int> claimMap = new();
		HashSet<int> nonOverlappingClaimIds = _claims.Select(c => c.Id).ToHashSet();
		foreach (ElfClaim claim in _claims)
		{
			foreach (Point point in claim.GetPoints())
			{
				if (claimMap.TryAdd(point, claim.Id))
				{
					continue;
				}
				// If we get here, the point is already claimed by another claim, so we have an overlap.
				nonOverlappingClaimIds.Remove(claim.Id);
				int otherClaimId = claimMap[point];
				if (otherClaimId >= 0) // if otherClaimId is -1, it's already been marked as overlapping
				{
					nonOverlappingClaimIds.Remove(otherClaimId);
					claimMap[point] = -1; // mark as overlapping
				}
			}
		}
		try
		{
			int nonOverlappingClaimId = nonOverlappingClaimIds.Single();
			return nonOverlappingClaimId.ToString();
		}
		catch (InvalidOperationException exception)
		{
			throw new DaySolverException(
				$"More than one ({nonOverlappingClaimIds.Count}) non-overlapping claims found",
				exception
			);
		}
	}
}
