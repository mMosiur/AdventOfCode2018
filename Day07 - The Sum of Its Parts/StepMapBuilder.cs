namespace AdventOfCode.Year2018.Day07;

class StepMapBuilder
{
	private readonly HashSet<char> _allRegisteredSteps = new();
	private readonly Dictionary<char, HashSet<char>> _requirements = new();

	public void AddStepRequirement(char stepLetter, char requiredStepLetter)
	{
		if (!_requirements.TryGetValue(stepLetter, out HashSet<char>? stepRequirements))
		{
			stepRequirements = new HashSet<char>();
			_requirements.Add(stepLetter, stepRequirements);
		}
		if (!stepRequirements.Add(requiredStepLetter))
		{
			throw new ApplicationException($"Step '{stepLetter}' already has requirement '{requiredStepLetter}'.");
		}
		_allRegisteredSteps.Add(stepLetter);
		_allRegisteredSteps.Add(requiredStepLetter);
	}

	public StepMap Build()
	{
		Dictionary<char, SingleStepBuilder> singleStepBuilders = _allRegisteredSteps.ToDictionary(c => c, c => new SingleStepBuilder(c));
		foreach ((char stepLetter, HashSet<char> stepRequirements) in _requirements)
		{
			char letter = stepLetter;
			try
			{
				SingleStepBuilder step = singleStepBuilders[letter];
				foreach (char requiredStepLetter in stepRequirements)
				{
					letter = requiredStepLetter;
					SingleStepBuilder requiredStep = singleStepBuilders[letter];
					step.AddRequirement(requiredStep);
				}
			}
			catch (KeyNotFoundException e)
			{
				throw new ApplicationException($"Step letter '{letter}' was not registered.", e);
			}
		}
		HashSet<Step> steps = singleStepBuilders.Values.Select(step =>
		{
			step.TrimExcess();
			return (Step)step;
		}).ToHashSet();
		return new StepMap(steps);
	}

	private class SingleStepBuilder : Step
	{
		private readonly HashSet<Step> _requirements = new();

		public SingleStepBuilder(char letter) : base(letter) { }

		public bool AddRequirement(Step requirement) => _requirements.Add(requirement);
		public void TrimExcess() => _requirements.TrimExcess();

		public override IReadOnlySet<Step> Requirements => _requirements;
	}
}
