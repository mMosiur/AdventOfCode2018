namespace AdventOfCode.Year2018.Day07;

public class StepMap
{
	public IReadOnlySet<Step> Steps { get; }

	public StepMap(IReadOnlySet<Step> steps)
	{
		Steps = steps;
	}

	public IEnumerable<Step> GetStepCompletionOrder()
	{
		List<Step> orderedList = Steps
			.OrderBy(step => step.Letter)
			.ToList();
		while (orderedList.Any())
		{
			int firstFinishedStepIndex = -1;
			for (int i = 0; i < orderedList.Count; i++)
			{
				Step step = orderedList[i];
				if (step.Requirements.All(s => s.Finished))
				{
					step.Finished = true;
					firstFinishedStepIndex = i;
					break;
				}
			}
			if (firstFinishedStepIndex < 0)
			{
				throw new ApplicationException("No steps could be completed.");
			}
			yield return orderedList[firstFinishedStepIndex];
			orderedList.RemoveAt(firstFinishedStepIndex);
		}
		foreach (Step step in Steps)
		{
			step.Finished = false;
		}
	}
}
