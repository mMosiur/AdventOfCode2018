using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

static class CuboidSplitter
{
	public static IEnumerable<Cuboid> Split(Cuboid cuboid)
	{
		int newXRangeLength = cuboid.XRange.Length / 2;
		Geometry.Range? newXRangeLower = newXRangeLength > 0
			? new(cuboid.XRange.Start, cuboid.XRange.Start + newXRangeLength - 1)
			: null;
		Geometry.Range newXRangeUpper = new(cuboid.XRange.Start + newXRangeLength, cuboid.XRange.End);

		int newYRangeLength = cuboid.YRange.Length / 2;
		Geometry.Range? newYRangeLower = newYRangeLength > 0
			? new(cuboid.YRange.Start, cuboid.YRange.Start + newYRangeLength - 1)
			: null;
		Geometry.Range newYRangeUpper = new(cuboid.YRange.Start + newYRangeLength, cuboid.YRange.End);

		int newZRangeLength = cuboid.ZRange.Length / 2;
		Geometry.Range? newZRangeLower = newZRangeLength > 0
			? new(cuboid.ZRange.Start, cuboid.ZRange.Start + newZRangeLength - 1)
			: null;
		Geometry.Range newZRangeUpper = new(cuboid.ZRange.Start + newZRangeLength, cuboid.ZRange.End);

		if (TryCreateCuboid(newXRangeLower, newYRangeLower, newZRangeLower, out Cuboid newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeLower, newYRangeLower, newZRangeUpper, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeLower, newYRangeUpper, newZRangeLower, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeLower, newYRangeUpper, newZRangeUpper, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeLower, newZRangeLower, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeLower, newZRangeUpper, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeUpper, newZRangeLower, out newCuboid)) yield return newCuboid;
		if (TryCreateCuboid(newXRangeUpper, newYRangeUpper, newZRangeUpper, out newCuboid)) yield return newCuboid;
	}

	private static bool TryCreateCuboid(Geometry.Range? xRange, Geometry.Range? yRange, Geometry.Range? zRange, out Cuboid cuboid)
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
