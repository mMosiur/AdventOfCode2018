using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

static class CuboidSplitter
{
	public static IEnumerable<Cuboid> Split(Cuboid cuboid)
	{
		int newXRangeLength = cuboid.XRange.Count / 2;
		Range? newXRangeLower = newXRangeLength > 0
			? new(cuboid.XRange.Start, cuboid.XRange.Start + newXRangeLength - 1)
			: null;
		Range newXRangeUpper = new(cuboid.XRange.Start + newXRangeLength, cuboid.XRange.End);

		int newYRangeLength = cuboid.YRange.Count / 2;
		Range? newYRangeLower = newYRangeLength > 0
			? new(cuboid.YRange.Start, cuboid.YRange.Start + newYRangeLength - 1)
			: null;
		Range newYRangeUpper = new(cuboid.YRange.Start + newYRangeLength, cuboid.YRange.End);

		int newZRangeLength = cuboid.ZRange.Count / 2;
		Range? newZRangeLower = newZRangeLength > 0
			? new(cuboid.ZRange.Start, cuboid.ZRange.Start + newZRangeLength - 1)
			: null;
		Range newZRangeUpper = new(cuboid.ZRange.Start + newZRangeLength, cuboid.ZRange.End);

		if (TryCreateCuboid(newXRangeLower, newYRangeLower, newZRangeLower, out Cuboid newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeLower, newYRangeLower, newZRangeUpper, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeLower, newYRangeUpper, newZRangeLower, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeLower, newYRangeUpper, newZRangeUpper, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeLower, newZRangeLower, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeLower, newZRangeUpper, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeUpper, newZRangeLower, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeUpper, newZRangeUpper, out newCuboid)) yield return newCuboid;
	}

	private static bool TryCreateCuboid(Range? xRange, Range? yRange, Range? zRange, out Cuboid cuboid)
	{
		if (xRange.HasValue && yRange.HasValue && zRange.HasValue)
		{
			cuboid = new Cuboid(xRange.Value, yRange.Value, zRange.Value);
			return true;
		}
		cuboid = default;
		return false;
	}
}
