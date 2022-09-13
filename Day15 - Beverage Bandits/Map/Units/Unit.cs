namespace AdventOfCode.Year2018.Day15.Map.Units;

public abstract class Unit : MapSpot
{
	public const int DefaultHitPoints = 200;
	public const int DefaultAttackPower = 3;

	public int HitPoints { get; private set; }
	public int AttackPower { get; private set; }

	public bool IsDead => HitPoints <= 0;

	public Coordinate Position { get; set; }

	private Unit(int hitPoints, int attackPower)
	{
		if (hitPoints <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(hitPoints), "Hit points must be greater than 0.");
		}
		if (attackPower <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(attackPower), "Attack power must be greater than 0.");
		}
		HitPoints = hitPoints;
		AttackPower = attackPower;
	}

	protected Unit(int attackPower) : this(DefaultHitPoints, attackPower) { }

	public Unit() : this(DefaultHitPoints, DefaultAttackPower)
	{
	}

	public AttackResult Attack(Unit target)
	{
		ArgumentNullException.ThrowIfNull(target);
		int prevTargetHealth = target.HitPoints;
		bool targetKilled = false;
		target.HitPoints -= AttackPower;
		if (target.HitPoints <= 0)
		{
			target.HitPoints = 0;
			targetKilled = true;
		}
		return new AttackResult
		{
			DamageDealt = prevTargetHealth - target.HitPoints,
			TargetKilled = targetKilled
		};
	}

	public override string ToString()
	{
		return $"{Type}({HitPoints})";
	}
}
