using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22.Cave;

class CaveTravelSimulator
{
	private readonly CaveSystem _caveSystem;
	private readonly ushort _timeToSwapTools;
	private readonly ushort _timeToCrossRegion;

	public CaveTravelSimulator(CaveSystem caveSystem, ushort timeToSwapTools, ushort timeToCrossRegion)
	{
		ArgumentNullException.ThrowIfNull(caveSystem);
		_caveSystem = caveSystem;
		_timeToSwapTools = timeToSwapTools;
		_timeToCrossRegion = timeToCrossRegion;
	}

	private IEnumerable<(CaveTravelState, uint)> GeneratePossibleNextStates(CaveTravelState state, uint time)
	{
		RegionType currentRegionType = _caveSystem[state.Coordinate];
		foreach (Coordinate adjacentCoordinate in _caveSystem.EnumerateNeighbors(state.Coordinate))
		{
			RegionType adjacentRegionType = _caveSystem[adjacentCoordinate];
			// If the adjacent region is not in the cave system, we can't move there
			if (state.SelectedTool.CanBeUsedInRegion(adjacentRegionType))
			{
				yield return (state with { Coordinate = adjacentCoordinate }, time + _timeToCrossRegion);
			}
		}
		foreach (Tool tool in ToolHelpers.AllTools)
		{
			if (tool != state.SelectedTool && tool.CanBeUsedInRegion(currentRegionType))
			{
				yield return (state with { SelectedTool = tool }, time + _timeToSwapTools);
			}
		}
	}

	public uint CalculateShortestTimeToReachTarget(Tool startingTool, Tool finishingTool)
	{
		CaveTravelState startingState = new(new Coordinate(0, 0), startingTool);
		CaveTravelState finishingState = new(_caveSystem.TargetCoordinate, finishingTool);
		PriorityQueue<CaveTravelState, uint> queue = new();
		queue.Enqueue(startingState, 0);
		Dictionary<CaveTravelState, uint> seenStates = new() { [startingState] = 0 };
		while (queue.TryDequeue(out CaveTravelState state, out uint time))
		{
			if (seenStates.TryGetValue(state, out uint seenTime1) && seenTime1 < time)
			{
				continue;
			}
			if (state == finishingState)
			{
				return time;
			}
			foreach ((CaveTravelState possibleNextState, uint possibleNextTime) in GeneratePossibleNextStates(state, time))
			{
				if (seenStates.TryGetValue(possibleNextState, out uint seenTime) && seenTime <= possibleNextTime)
				{
					continue;
				}
				seenStates[possibleNextState] = possibleNextTime;
				queue.Enqueue(possibleNextState, possibleNextTime);
			}
		}
		throw new DaySolverException("No path to target found.");
	}
}
