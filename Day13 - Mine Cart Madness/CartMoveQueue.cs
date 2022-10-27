namespace AdventOfCode.Year2018.Day13;

class CartMoveQueue
{
	private readonly SortedDictionary<Coordinate, Cart> _queue;

	public int Count => _queue.Count;

	public CartMoveQueue()
	{
		_queue = new SortedDictionary<Coordinate, Cart>(
			Comparer<Coordinate>.Create((coord1, coord2)
				=> coord1.Y - coord2.Y != 0 ? coord1.Y - coord2.Y : coord1.X - coord2.X
			)
		);
	}

	public void Enqueue(Cart cart)
	{
		_queue.Add(cart.Position, cart);
	}

	public void EnqueueRange(IEnumerable<Cart> carts)
	{
		foreach (Cart cart in carts)
		{
			Enqueue(cart);
		}
	}

	public Cart Dequeue()
	{
		(Coordinate position, Cart cart) = _queue.First();
		_queue.Remove(position);
		return cart;
	}

	public bool TryDequeue(out Cart cart)
	{
		cart = default;
		if (Count == 0)
		{
			return false;
		}
		cart = Dequeue();
		return true;
	}

	public bool Remove(Coordinate position)
	{
		return _queue.Remove(position);
	}

}
