using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

sealed class CuboidPriorityQueue
{
	private readonly PriorityQueue<byte, State> _queue;

	public CuboidPriorityQueue(Point origin)
	{
		_queue = new(
			comparer: new StateQueuePriorityComparer(origin)
		);
	}

	public void Enqueue(int nanobotsInRangeCount, Cuboid cuboid)
	{
		_queue.Enqueue(default, new State(nanobotsInRangeCount, cuboid));
	}

	public bool TryDequeue(out int nanobotsInRangeCount, out Cuboid cuboid)
	{
		if (_queue.TryDequeue(out _, out State state))
		{
			nanobotsInRangeCount = state.NanobotsInRangeCount;
			cuboid = state.Cuboid;
			return true;
		}
		nanobotsInRangeCount = default;
		cuboid = default;
		return false;
	}

	private sealed class StateQueuePriorityComparer : IComparer<State>
	{
		private readonly Point _origin;

		public StateQueuePriorityComparer(Point origin)
		{
			_origin = origin;
		}

		public int Compare(State s1, State s2)
		{
			// Negated result because we want the bigger value to be first.
			int result = -s1.NanobotsInRangeCount.CompareTo(s2.NanobotsInRangeCount);
			if (result != 0)
			{
				return result;
			}
			// Negated result because we want the bigger value to be first.
			// We want to process the biggest cuboids first as their
			// sub-cuboids could potentially be better.
			result = -s1.Cuboid.Volume.CompareTo(s2.Cuboid.Volume);
			if (result != 0)
			{
				return result;
			}
			// In case of a tie we want to process the cuboid that is
			// closest to the origin first, as per puzzle description:
			// "if there are multiple, choose one closest to your position".
			Point p1 = ExtendedMath.GetCuboidPointClosestToPoint(s1.Cuboid, _origin);
			Point p2 = ExtendedMath.GetCuboidPointClosestToPoint(s2.Cuboid, _origin);
			int distanceToOrigin1 = ExtendedMath.ManhattanDistance(_origin, p1);
			int distanceToOrigin2 = ExtendedMath.ManhattanDistance(_origin, p2);
			// NON-negated result because we want the smaller value to be first.
			result = distanceToOrigin1.CompareTo(distanceToOrigin2);
			return result;
		}
	}

	readonly struct State
	{
		public int NanobotsInRangeCount { get; }
		public Cuboid Cuboid { get; }

		public State(int nanobotsInRangeCount, Cuboid cuboid)
		{
			NanobotsInRangeCount = nanobotsInRangeCount;
			Cuboid = cuboid;
		}
	}
}
