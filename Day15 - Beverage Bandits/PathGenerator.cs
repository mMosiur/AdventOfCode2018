namespace AdventOfCode.Year2018.Day15;

public class PathGenerator
{
	private readonly CombatMap _combatMap;

	public PathGenerator(CombatMap combatMap)
	{
		_combatMap = combatMap;
	}

	public Dictionary<Coordinate, Path> GenerateShortestPaths(Coordinate start)
	{
		Path origin = new(start);
		Dictionary<Coordinate, Path> shortestPaths = new() { [start] = origin };

		PathQueue queue = new() { origin };
		while (queue.TryDequeue(out Path? currentPath))
		{
			foreach (Coordinate nextPosition in _combatMap.AdjacentEmpty(currentPath.End))
			{
				Path newPath = currentPath.NewExtendedTo(nextPosition);
				if (shortestPaths.TryGetValue(nextPosition, out Path? existingPath))
				{
					if (existingPath <= newPath) continue;
				}
				shortestPaths[nextPosition] = newPath;
				queue.Enqueue(newPath);
			}
		}

		return shortestPaths;
	}
}
