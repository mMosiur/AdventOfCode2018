namespace AdventOfCode.Year2018.Day13;

class CartCrashAnalyzer
{
	private readonly TrackSymbol[,] _map;
	private readonly IReadOnlyCollection<Cart> _carts;

	public CartCrashAnalyzer(TrackSymbol[,] map)
	{
		ArgumentNullException.ThrowIfNull(map);
		_map = (TrackSymbol[,])map.Clone();
		if (!TrackMapParser.ExtractCartsFromMap(_map, out List<Cart>? carts))
		{
			throw new ApplicationException("No carts were found.");
		}
		_carts = carts;
	}

	private IEnumerable<Coordinate> EnumerateCrashPositions()
	{
		const int MaxIterationsCount = 100_000;

		CartMoveQueue moveQueue = new();
		moveQueue.EnqueueRange(_carts);
		Dictionary<Coordinate, Cart> currentCartPositions = _carts.ToDictionary(c => c.Position);

		for (int iteration = 0; iteration < MaxIterationsCount; ++iteration)
		{
			if (moveQueue.Count == 0)
			{
				yield break;
			}
			if (moveQueue.Count == 1)
			{
				Cart loneCart = moveQueue.Dequeue();
				throw new LoneCartLeftException(loneCart.Position);
			}
			CartMoveQueue nextMoveQueue = new();
			while (moveQueue.TryDequeue(out Cart cart))
			{
				currentCartPositions.Remove(cart.Position);
				cart.Move(_map);
				if (currentCartPositions.Remove(cart.Position))
				{
					// Exactly one of these queues will contain the cart that was crashed into.
					// The first one if the cart has not moved yet in this iteration,
					// the second one if it has already moved.
					moveQueue.Remove(cart.Position);
					nextMoveQueue.Remove(cart.Position);
					yield return cart.Position;
				}
				else
				{
					currentCartPositions.Add(cart.Position, cart);
					nextMoveQueue.Enqueue(cart);
				}
			}
			moveQueue = nextMoveQueue;
		}
	}

	public Coordinate FindFirstCrashPosition()
	{
		if (_carts.Count < 2)
		{
			throw new ApplicationException("At least two carts are required for a crash to occur.");
		}
		return EnumerateCrashPositions().First();
	}

	public Coordinate FindLastStandingCartPosition()
	{
		if (_carts.Count % 2 == 0)
		{
			throw new ApplicationException("There are an even number of carts, so there is no last standing cart.");
		}
		try
		{
			_ = EnumerateCrashPositions().Last();
			throw new ApplicationException("No lone cart left after crashes.");
		}
		catch (LoneCartLeftException e)
		{
			return e.LoneCartPosition;
		}
	}
}
