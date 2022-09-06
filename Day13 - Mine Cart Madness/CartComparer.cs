namespace AdventOfCode.Year2018.Day13;

public class CartComparer : IComparer<Cart>
{
	public int Compare(Cart x, Cart y)
	{
		int comparison = x.Position.Y - y.Position.Y;
		if (comparison != 0)
		{
			return comparison;
		}
		comparison = x.Position.X - y.Position.X;
		return comparison;
	}
}
