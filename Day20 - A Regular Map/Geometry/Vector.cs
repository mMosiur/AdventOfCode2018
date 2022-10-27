namespace AdventOfCode.Year2018.Day20.Geometry;

readonly struct Vector : IEquatable<Vector>
{
	public int X { get; }
	public int Y { get; }

	public Vector(int x, int y)
	{
		X = x;
		Y = y;
	}

	public static Vector FromDirection(Direction direction) => direction switch
	{
		Direction.North => new(0, -1),
		Direction.South => new(0, 1),
		Direction.West => new(-1, 0),
		Direction.East => new(1, 0),
		_ => throw new ArgumentException(null, nameof(direction))
	};

	public static Vector operator +(Vector vec1, Vector vec2) => new(vec1.X + vec2.X, vec1.Y + vec2.Y);
	public static Position operator +(Vector vector, Position position) => new(vector.X + position.X, vector.Y + position.Y);
	public static Position operator +(Position position, Vector vector) => vector + position;
	public static Vector operator -(Vector vec1, Vector vec2) => new(vec1.X - vec2.X, vec1.Y - vec2.Y);
	public static Vector operator *(Vector vector, int scalar) => new(vector.X * scalar, vector.Y * scalar);
	public static Vector operator *(int scalar, Vector vector) => vector * scalar;
	public static Vector operator -(Vector vector) => new(-vector.X, -vector.Y);

	public bool Equals(Vector other) => X == other.X && Y == other.Y;
	public override bool Equals(object? obj) => obj is Vector vector && Equals(vector);
	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(Vector left, Vector right) => left.Equals(right);
	public static bool operator !=(Vector left, Vector right) => !(left == right);

	public override string ToString() => $"Vector[{X}, {Y}]";
}
