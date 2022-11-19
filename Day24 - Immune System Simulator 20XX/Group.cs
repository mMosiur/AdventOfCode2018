namespace AdventOfCode.Year2018.Day24;

sealed class Group
{
	private readonly string[] _weaknesses;
	private readonly string[] _immunities;

	public int UnitCount { get; private set; }
	public int HitPoints { get; }
	public int AttackDamage { get; }
	public string AttackType { get; }
	public int Initiative { get; }
	public IReadOnlyCollection<string> Weaknesses => _weaknesses;
	public IReadOnlyCollection<string> Immunities => _immunities;
	public int EffectivePower => UnitCount * AttackDamage;
	public bool IsDefeated => UnitCount <= 0;

	public Group(int unitCount, int hitPoints, int attackDamage, string attackType, int initiative, IEnumerable<string>? weaknesses, IEnumerable<string>? immunities)
	{
		if (unitCount < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(unitCount), unitCount, "Unit count must be non-negative.");
		}
		UnitCount = unitCount;
		if (hitPoints < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(hitPoints), hitPoints, "Hit points must be non-negative.");
		}
		HitPoints = hitPoints;
		if (attackDamage < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(attackDamage), attackDamage, "Attack damage must be non-negative.");
		}
		AttackDamage = attackDamage;
		if (string.IsNullOrWhiteSpace(attackType))
		{
			throw new ArgumentException("Attack type must be non-empty.", nameof(attackType));
		}
		AttackType = attackType.ToLower();
		if (initiative < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(initiative), initiative, "Initiative must be non-negative.");
		}
		Initiative = initiative;
		_weaknesses = weaknesses is null ? Array.Empty<string>() : weaknesses.Select(w => w.ToLower()).ToArray();
		_immunities = immunities is null ? Array.Empty<string>() : immunities.Select(i => i.ToLower()).ToArray();
	}

	public int CalculateDamageTowards(Group defender)
	{
		if (defender.Immunities.Contains(AttackType))
		{
			return 0;
		}
		int damage = EffectivePower;
		if (defender.Weaknesses.Contains(AttackType))
		{
			damage *= 2;
		}
		return damage;
	}

	// Returns true if the group was defeated.
	public bool Attack(Group defender)
	{
		if (defender.IsDefeated)
		{
			throw new ArgumentException("Defender must have at least one unit.", nameof(defender));
		}
		if (IsDefeated)
		{
			throw new InvalidOperationException("Attacker must have at least one unit.");
		}
		int damage = CalculateDamageTowards(defender);
		int casualties = damage / defender.HitPoints;
		defender.UnitCount -= casualties;
		if (defender.UnitCount < 0)
		{
			defender.UnitCount = 0;
			return true;
		}
		return false;
	}
}
