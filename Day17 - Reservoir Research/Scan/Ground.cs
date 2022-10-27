using System.Collections;
using AdventOfCode.Year2018.Day17.Geometry;

namespace AdventOfCode.Year2018.Day17.Scan;

class Ground : IEnumerable<GroundType>
{
	private readonly GroundType[,] _ground;
	private readonly HashSet<Point> _activeWaterPoints;
	public readonly Area Area;

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
		get => _ground[x - Area.XRange.Start, y - Area.YRange.Start];
		private set => _ground[x - Area.XRange.Start, y - Area.YRange.Start] = value;
	}

	public GroundType this[Point point]
	{
		get => this[point.X, point.Y];
		private set => this[point.X, point.Y] = value;
	}

	private FlowResult TryFlowDownFrom(Point point)
	{
		bool hasSpread = false;
		bool blocked = false;
		while (Area.Contains(point.Below))
		{
			Point pointBelow = point.Below;
			GroundType groundBelow = this[pointBelow];
			if (!groundBelow.IsPassable())
			{
				blocked = groundBelow.IsBlocked();
				break;
			}
			point = pointBelow;
			this[point] = GroundType.WaterFlowing;
			hasSpread = true;
		}
		return new FlowResult(hasSpread, blocked, point);
	}

	private FlowResult TryFlowLeftFrom(Point point)
	{
		bool hasSpread = false;
		bool blocked = false;
		while (Area.Contains(point.Left) && Area.Contains(point.Below) && this[point.Below].IsBlocked())
		{
			Point pointLeft = point.Left;
			GroundType groundLeft = this[pointLeft];
			if (!groundLeft.IsPassable())
			{
				blocked = groundLeft.IsBlocked();
				break;
			}
			point = pointLeft;
			this[point] = GroundType.WaterFlowing;
			hasSpread = true;
		}
		return new FlowResult(hasSpread, blocked, point);
	}

	private FlowResult TryFlowRightFrom(Point point)
	{
		bool hasSpread = false;
		bool blocked = false;
		while (Area.Contains(point.Right) && Area.Contains(point.Below) && this[point.Below].IsBlocked())
		{
			Point pointRight = point.Right;
			GroundType groundRight = this[pointRight];
			if (!groundRight.IsPassable())
			{
				blocked = groundRight.IsBlocked();
				break;
			}
			point = pointRight;
			this[point] = GroundType.WaterFlowing;
			hasSpread = true;
		}
		return new FlowResult(hasSpread, blocked, point);
	}

	private StabilizeResult TryStabilize(Point pointToConsider)
	{
		if (this[pointToConsider] is not GroundType.WaterFlowing || !Area.Contains(pointToConsider.Below))
		{
			return StabilizeResult.NotStabilized;
		}
		Point leftmostPoint = pointToConsider;
		while (this[leftmostPoint] is GroundType.WaterFlowing)
		{
			if (!this[leftmostPoint.Below].IsBlocked())
			{
				return StabilizeResult.NotStabilized;
			}
			leftmostPoint = leftmostPoint.Left;
			if (!Area.Contains(leftmostPoint))
			{
				return StabilizeResult.NotStabilized;
			}
		}
		if (!this[leftmostPoint].IsBlocked())
		{
			return StabilizeResult.NotStabilized;
		}
		leftmostPoint = leftmostPoint.Right;
		Point rightmostPoint = pointToConsider;
		while (this[rightmostPoint] is GroundType.WaterFlowing)
		{
			if (!this[rightmostPoint.Below].IsBlocked())
			{
				return StabilizeResult.NotStabilized;
			}
			rightmostPoint = rightmostPoint.Right;
			if (!Area.Contains(rightmostPoint))
			{
				return StabilizeResult.NotStabilized;
			}
		}
		if (!this[rightmostPoint].IsBlocked())
		{
			return StabilizeResult.NotStabilized;
		}
		rightmostPoint = rightmostPoint.Left;
		Point stabilizingPoint = leftmostPoint;
		Point rightEdgePoint = rightmostPoint.Right;
		while (stabilizingPoint != rightEdgePoint)
		{
			this[stabilizingPoint] = GroundType.WaterResting;
			stabilizingPoint = stabilizingPoint.Right;
		}
		return StabilizeResult.Stabilized(leftmostPoint, rightmostPoint);
	}

	private bool FlowFrom(Point point)
	{
		_activeWaterPoints.Remove(point);
		// Try flow down
		FlowResult flowResult = TryFlowDownFrom(point);
		if (flowResult.HasSpread)
		{
			if (flowResult.Blocked)
			{
				_activeWaterPoints.Add(flowResult.FinalActivePoint);
			}
			return true;
		}
		if (!flowResult.Blocked)
		{
			return false;
		}
		// Flow sideways
		bool hasChanged = false;
		// Flow left
		flowResult = TryFlowLeftFrom(point);
		if (flowResult.HasSpread)
		{
			_activeWaterPoints.Add(flowResult.FinalActivePoint);
			hasChanged = true;
		}
		else
		{
			StabilizeResult stabilizeResult = TryStabilize(flowResult.FinalActivePoint);
			if (stabilizeResult.HasStabilized)
			{
				hasChanged = true;
				Point potentialNewActiveWaterPoint = stabilizeResult.LeftmostStabilizedPoint.Above;
				Point aboveStabilizationEdgePoint = stabilizeResult.RightmostStabilizedPoint.Right.Above;
				while (potentialNewActiveWaterPoint != aboveStabilizationEdgePoint)
				{
					if (this[potentialNewActiveWaterPoint].IsSpreading())
					{
						_activeWaterPoints.Add(potentialNewActiveWaterPoint);
					}
					potentialNewActiveWaterPoint = potentialNewActiveWaterPoint.Right;
				}
			}
		}
		// Flow right
		flowResult = TryFlowRightFrom(point);
		if (flowResult.HasSpread)
		{
			_activeWaterPoints.Add(flowResult.FinalActivePoint);
			hasChanged = true;
		}
		else if (flowResult.Blocked)
		{
			StabilizeResult stabilizeResult = TryStabilize(flowResult.FinalActivePoint);
			if (stabilizeResult.HasStabilized)
			{
				hasChanged = true;
				Point potentialNewActiveWaterPoint = stabilizeResult.LeftmostStabilizedPoint.Above;
				Point aboveStabilizationEdgePoint = stabilizeResult.RightmostStabilizedPoint.Right.Above;
				while (potentialNewActiveWaterPoint != aboveStabilizationEdgePoint)
				{
					if (this[potentialNewActiveWaterPoint].IsSpreading())
					{
						_activeWaterPoints.Add(potentialNewActiveWaterPoint);
					}
					potentialNewActiveWaterPoint = potentialNewActiveWaterPoint.Right;
				}
			}
		}
		return hasChanged;
	}

	public bool NextState()
	{
		bool changed = false;
		foreach (Point activeWaterPoint in _activeWaterPoints.ToArray())
		{
			changed |= FlowFrom(activeWaterPoint);
		}
		return changed;
	}

	public void SimulateFlow()
	{
		while (NextState()) { }
	}

	public override string ToString()
	{
		System.Text.StringBuilder sb = new();
		for (int y = Area.YRange.Start; y <= Area.YRange.End; y++)
		{
			for (int x = Area.XRange.Start; x <= Area.XRange.End; x++)
			{
				char c = this[x, y].ToChar();
				sb.Append(c);
			}
			sb.Append('\n');
		}
		sb.Remove(sb.Length - 1, 1);
		return sb.ToString();
	}

	public IEnumerator<GroundType> GetEnumerator() => _ground.Cast<GroundType>().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerable<GroundType> EnumerateArea(Area area)
	{
		if (!Area.Contains(area))
		{
			throw new ArgumentException("Area is not fully contained in the ground.");
		}
		return area.EnumeratePoints().Select(p => this[p]);
	}

	private readonly record struct FlowResult(bool HasSpread, bool Blocked, Point FinalActivePoint);

	private readonly record struct StabilizeResult(bool HasStabilized, Point LeftmostStabilizedPoint, Point RightmostStabilizedPoint)
	{
		public static StabilizeResult Stabilized(Point leftmostStabilizedPoint, Point rightmostStabilizedPoint) => new(true, leftmostStabilizedPoint, rightmostStabilizedPoint);
		public static readonly StabilizeResult NotStabilized = new(false, default, default);
	}
}
