using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day03;

public class Day03Solver : DaySolver
{
	private readonly IReadOnlyList<ElfClaim> _claims;

	public Day03Solver(Day03SolverOptions options) : base(options)
	{
		_claims = InputLines.Select(ElfClaim.Parse).ToList();
	}

	public Day03Solver(Action<Day03SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		Dictionary<Point, int> claimMap = new();
		foreach (ElfClaim claim in _claims)
		{
			foreach (Point point in claim.GetPoints())
			{
				int claimCountAtThatPoint = 0;
				claimMap.TryGetValue(point, out claimCountAtThatPoint);
				claimMap[point] = claimCountAtThatPoint + 1;
			}
		}
		int pointsWithOverlapCount = claimMap.Values.Count(v => v > 1);
		return pointsWithOverlapCount.ToString();
	}

	public override string SolvePart2()
	{
		Dictionary<Point, int> claimMap = new();
		HashSet<int> nonOverlapingClaimIds = _claims.Select(c => c.Id).ToHashSet();
		foreach (ElfClaim claim in _claims)
		{
			foreach (Point point in claim.GetPoints())
			{
				if (claimMap.TryAdd(point, claim.Id))
				{
					continue;
				}
				// If we get here, the point is already claimed by another claim, so we have an overlap.
				nonOverlapingClaimIds.Remove(claim.Id);
				int otherClaimId = claimMap[point];
				if (otherClaimId >= 0) // if otherClaimId is -1, it's already been marked as overlapping
				{
					nonOverlapingClaimIds.Remove(otherClaimId);
					claimMap[point] = -1; // mark as overlapping
				}
			}
		}
		try
		{
			int nonOverlapingClaimId = nonOverlapingClaimIds.Single();
			return nonOverlapingClaimId.ToString();
		}
		catch (InvalidOperationException exception)
		{
			throw new ApplicationException(
				$"More than one ({nonOverlapingClaimIds.Count}) non-overlapping claims found",
				exception
			);
		}
	}
}
