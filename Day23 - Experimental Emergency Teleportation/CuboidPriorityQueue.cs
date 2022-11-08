using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

sealed class CuboidPriorityQueue
{
	private readonly PriorityQueue<byte, State> _queue;

	public CuboidPriorityQueue()
	{
		_queue = new(
			comparer: new StateQueuePriorityComparer()
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
			return 0;
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
