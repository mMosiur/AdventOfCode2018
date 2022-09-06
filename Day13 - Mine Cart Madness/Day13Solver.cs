using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day13;

public class Day13Solver : DaySolver
{
	private readonly TrackSymbol[,] _map;
	private readonly IReadOnlyList<Cart> _carts;

	public Day13Solver(Day13SolverOptions options) : base(options)
	{
		string[] lines = InputLines.ToArray();
		_map = TrackMapParser.Parse(lines);
		if (!TrackMapParser.ExtractCartsFromMap(_map, out List<Cart>? carts))
		{
			throw new ApplicationException("No carts were found.");
		}
		_carts = carts;
	}

	public Day13Solver(Action<Day13SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		const int MaxIterationsCount = 100_000;

		CartComparer cartComparer = new();
		List<Cart> carts = _carts.ToList();
		Dictionary<Coordinate, Cart> cartPositions = _carts.ToDictionary(c => c.Position);

		for (int iteration = 0; iteration < MaxIterationsCount; ++iteration)
		{
			carts.Sort(cartComparer);
			for (int cartIndex = 0; cartIndex < carts.Count; ++cartIndex)
			{
				Cart cart = carts[cartIndex];
				cartPositions.Remove(cart.Position);
				cart.Move(_map);
				carts[cartIndex] = cart;
				if (cartPositions.TryGetValue(cart.Position, out Cart collidedCart))
				{
					// Collision detected
					return cart.Position.ToString();
				}
				cartPositions[cart.Position] = cart;
			}
		}
		throw new ApplicationException($"No collision was found through {MaxIterationsCount} iterations.");
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
