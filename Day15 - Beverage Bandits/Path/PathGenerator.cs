using AdventOfCode.Year2018.Day15.Combat;

namespace AdventOfCode.Year2018.Day15.Path;

public class PathGenerator
{
	private readonly CombatMap _combatMap;

	public PathGenerator(CombatMap combatMap)
	{
		_combatMap = combatMap;
	}

	public Dictionary<Coordinate, PathSignature> GenerateShortestPaths(Coordinate start)
	{
		PathSignature origin = new(start);
		Dictionary<Coordinate, PathSignature> shortestPaths = new() { [start] = origin };

		PathSignatureQueue queue = new() { origin };
		while (queue.TryDequeue(out PathSignature currentPath))
		{
			foreach (Coordinate nextPosition in _combatMap.AdjacentEmpty(currentPath.End))
			{
				PathSignature newPath = currentPath.ExtendedTo(nextPosition);
				if (shortestPaths.TryGetValue(nextPosition, out PathSignature existingPath))
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
