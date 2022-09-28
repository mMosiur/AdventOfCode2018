using System.Collections;
using AdventOfCode.Year2018.Day17.Geometry;

namespace AdventOfCode.Year2018.Day17.Scan;

public class Ground : IEnumerable<GroundType>
{
	private readonly GroundType[,] _ground;
	private readonly HashSet<Point> _activeWaterPoints;
	public Area Area { get; }

	public int Width => Area.Width;
	public int Height => Area.Height;

	protected Ground(GroundType[,] ground, Area area)
	{
		ArgumentNullException.ThrowIfNull(ground);
		if (ground.GetLength(0) != area.Width)
		{
			throw new ArgumentException("Ground width does not match specified ground area.", nameof(area));
		}
		if (ground.GetLength(1) != area.Height)
		{
			throw new ArgumentException("Ground height does not match specified ground area.", nameof(area));
		}
		_ground = ground;
		Area = area;
		_activeWaterPoints = new HashSet<Point>()
		{
			area.EnumeratePoints().Single(p => this[p] is GroundType.WaterSpring)
		};
	}

	public GroundType this[int x, int y]
	{
		get
		{
			if (!Area.XRange.Contains(x))
			{
				throw new ArgumentOutOfRangeException(nameof(x));
			}
			if (!Area.YRange.Contains(y))
			{
				throw new ArgumentOutOfRangeException(nameof(y));
			}
			return _ground[x - Area.XRange.Start, y - Area.YRange.Start];
		}
		private set
		{
			if (!Area.XRange.Contains(x))
			{
				throw new ArgumentOutOfRangeException(nameof(x));
			}
			if (!Area.YRange.Contains(y))
			{
				throw new ArgumentOutOfRangeException(nameof(y));
			}
			_ground[x - Area.XRange.Start, y - Area.YRange.Start] = value;
		}
	}

	public GroundType this[Point point]
	{
		get
		{
			if (!Area.Contains(point))
			{
				throw new ArgumentOutOfRangeException(nameof(point));
			}
			return _ground[point.X - Area.XRange.Start, point.Y - Area.YRange.Start];
		}
		private set
		{
			if (!Area.Contains(point))
			{
				throw new ArgumentOutOfRangeException(nameof(point));
			}
			_ground[point.X - Area.XRange.Start, point.Y - Area.YRange.Start] = value;
		}
	}

	private SpreadResult TrySpreadDownFrom(Point point)
	{
		Point pointBelow = point.Below;
		if (!Area.Contains(pointBelow))
		{
			return SpreadResult.NotMoved;
		}
		GroundType groundBelow = this[pointBelow];
		if (groundBelow.IsBlocked())
		{
			return SpreadResult.BlockedDirectly;
		}
		if (groundBelow.IsPassable())
		{
			this[pointBelow] = GroundType.WaterFlowing;
			return SpreadResult.MovedDirectly;
		}
		if (groundBelow.IsSpreading())
		{
			return SpreadFrom(pointBelow) with { Directly = false };
		}
		throw new InvalidOperationException("Unexpected ground type.");
	}

	private SpreadResult TrySpreadLeftFrom(Point point)
	{
		Point pointLeft = point.Left;
		if (!Area.Contains(pointLeft))
		{
			return SpreadResult.NotMoved;
		}
		GroundType groundLeft = this[pointLeft];
		if (groundLeft.IsBlocked())
		{
			return SpreadResult.BlockedDirectly;
		}
		if (groundLeft.IsPassable())
		{
			this[pointLeft] = GroundType.WaterFlowing;
			return SpreadResult.MovedDirectly;
		}
		if (groundLeft.IsSpreading())
		{
			return SpreadFrom(pointLeft, rightLocked: true) with { Directly = false };
		}
		throw new InvalidOperationException("Unexpected ground type.");
	}

	private SpreadResult TrySpreadRightFrom(Point point)
	{
		Point pointRight = point.Right;
		if (!Area.Contains(pointRight))
		{
			return SpreadResult.NotMoved;
		}
		GroundType groundRight = this[pointRight];
		if (groundRight.IsBlocked())
		{
			return SpreadResult.BlockedDirectly;
		}
		if (groundRight.IsPassable())
		{
			this[pointRight] = GroundType.WaterFlowing;
			return SpreadResult.MovedDirectly;
		}
		if (groundRight.IsSpreading())
		{
			return SpreadFrom(pointRight, leftLocked: true) with { Directly = false };
		}
		throw new InvalidOperationException("Unexpected ground type.");
	}


	private SpreadResult SpreadFrom(Point point, bool leftLocked = false, bool rightLocked = false)
	{
		if (!Area.Contains(point) || !this[point].IsSpreading())
		{
			return SpreadResult.NotMoved;
		}
		SpreadResult spreadDownResult = TrySpreadDownFrom(point);
		if (spreadDownResult.Spread is true || spreadDownResult.Directly is false)
		{
			if (spreadDownResult == SpreadResult.MovedDirectly)
			{
				_activeWaterPoints.Remove(point);
				_activeWaterPoints.Add(point.Below);
			}
			return spreadDownResult;
		}
		// spreadDownResult is BlockedDirectly
		SpreadResult spreadLeftResult = SpreadResult.NotMoved;
		if (leftLocked is false)
		{
			spreadLeftResult = TrySpreadLeftFrom(point);
			if (spreadLeftResult == SpreadResult.MovedDirectly)
			{
				_activeWaterPoints.Remove(point);
				_activeWaterPoints.Add(point.Left);
			}
		}
		SpreadResult spreadRightResult = SpreadResult.NotMoved;
		if (rightLocked is false)
		{
			spreadRightResult = TrySpreadRightFrom(point);
			if (spreadRightResult == SpreadResult.MovedDirectly)
			{
				_activeWaterPoints.Remove(point);
				_activeWaterPoints.Add(point.Right);
			}
		}
		if (spreadLeftResult.Spread || spreadRightResult.Spread)
		{
			return new(true, spreadLeftResult.Directly || spreadRightResult.Directly);
		}
		return new(false, spreadLeftResult.Directly && spreadRightResult.Directly);
	}

	private bool TryStabilizeArea(int y, int leftEdgeX, int rightEdgeX)
	{
		if (rightEdgeX <= leftEdgeX)
		{
			throw new ArgumentException("Right edge must be greater than left edge.", nameof(rightEdgeX));
		}
		int lowerY = y + 1;
		if (!Area.YRange.Contains(y) || !Area.YRange.Contains(lowerY) || !Area.XRange.Contains(leftEdgeX) || !Area.XRange.Contains(rightEdgeX))
		{
			return false;
		}
		if (!this[leftEdgeX, y].IsBlocked() || !this[rightEdgeX, y].IsBlocked())
		{
			return false;
		}
		for (int x = leftEdgeX + 1; x < rightEdgeX; x++)
		{
			// If any square under the current one is not blocked or is not flowing water, the current one cannot be stabilized.
			if (this[x, y] is not GroundType.WaterFlowing || !this[x, lowerY].IsBlocked())
			{
				return false;
			}
		}
		for (int x = leftEdgeX + 1; x < rightEdgeX; x++)
		{
			this[x, y] = GroundType.WaterResting;
		}
		return true;
	}

	private bool TryStabilizeWaters()
	{
		bool updated = false;
		for (int y = Area.YRange.Start; y < Area.YRange.End; y++) // Last row excluded as it can't be stabilized.
		{
			// Look for '#|||||#' pattern (clay at the ends, flowing water in the middle).
			int? leftEdgeX = null;
			bool hasWater = false;
			int? rightEdgeX = null;
			for (int x = Area.XRange.Start; x <= Area.XRange.End; x++)
			{
				switch (this[x, y])
				{
					case GroundType.Clay:
						if (hasWater) rightEdgeX = x;
						else leftEdgeX = x;
						break;
					case GroundType.WaterFlowing:
						if (leftEdgeX.HasValue) hasWater = true;
						break;
					default:
						leftEdgeX = null;
						hasWater = false;
						rightEdgeX = null;
						break;
				}
				if (leftEdgeX.HasValue && hasWater && rightEdgeX.HasValue)
				{
					// Pattern found, try to stabilize if possible.
					bool stabilized = TryStabilizeArea(y, leftEdgeX.Value, rightEdgeX.Value);
					if (stabilized)
					{
						updated = true;
						int yAbove = y - 1;
						for (int stabilizingX = leftEdgeX.Value + 1; stabilizingX < rightEdgeX.Value; stabilizingX++)
						{
							_activeWaterPoints.Remove(new(stabilizingX, y));
							// If any square above the stabilized area is flowing water, it can spread again.
							if (Area.YRange.Contains(yAbove) && this[stabilizingX, yAbove].IsSpreading())
							{
								_activeWaterPoints.Add(new(stabilizingX, yAbove));
							}
						}
						leftEdgeX = rightEdgeX;
					}
					else
					{
						leftEdgeX = null;
					}
					hasWater = false;
					rightEdgeX = null;
				}
			}
		}
		return updated;
	}

	public bool NextState()
	{
		bool changed = false;
		foreach (Point activeWaterPoint in _activeWaterPoints.ToArray())
		{
			changed |= SpreadFrom(activeWaterPoint).Spread;
		}
		if (changed)
		{
			return true;
		}
		changed = TryStabilizeWaters();
		return changed;
	}

	public void Print()
	{
		for (int y = Area.YRange.Start; y <= Area.YRange.End; y++)
		{
			for (int x = Area.XRange.Start; x <= Area.XRange.End; x++)
			{
				Console.Write(this[x, y].ToChar());
			}
			Console.WriteLine();
		}
	}

	public IEnumerator<GroundType> GetEnumerator() => _ground.Cast<GroundType>().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private readonly record struct SpreadResult(bool Spread, bool Directly)
	{
		/// <summary>
		/// Result when the spread did not happen because it was blocked further down the line or movement ignored.
		/// </summary>
		public static readonly SpreadResult NotMoved = new(false, false);

		/// <summary>
		/// Result when the spread did not happen because it was directly blocked from that point.
		/// </summary>
		public static readonly SpreadResult BlockedDirectly = new(false, true);

		/// <summary>
		/// Result when the spread did happened further down the line.
		/// </summary>
		public static readonly SpreadResult MovedDownTheLine = new(true, false);

		/// <summary>
		/// Result when the spread did happened directly from that point.
		/// </summary>
		public static readonly SpreadResult MovedDirectly = new(true, true);
	}
}
