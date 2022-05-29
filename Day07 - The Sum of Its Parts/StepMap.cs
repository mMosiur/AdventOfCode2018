namespace AdventOfCode.Year2018.Day07;

public class StepMap
{
	public IReadOnlySet<Step> Steps { get; }

	public StepMap(IReadOnlySet<Step> steps)
	{
		Steps = steps;
	}

	private void ResetStepsFinishedStatus(bool status = false)
	{
		foreach (Step step in Steps)
		{
			step.Finished = status;
		}
	}

	public IEnumerable<Step> GetAlphabeticalStepCompletionOrder()
	{
		List<Step> stepsLeft = Steps
			.OrderBy(step => step.Letter)
			.ToList();

		while (stepsLeft.Count > 0)
		{
			int firstFinishedStepIndex = -1;
			for (int i = 0; i < stepsLeft.Count; i++)
			{
				Step step = stepsLeft[i];
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
			yield return stepsLeft[firstFinishedStepIndex];
			stepsLeft.RemoveAt(firstFinishedStepIndex);
		}

		ResetStepsFinishedStatus();
	}

	private static int CalculateStepTime(Step step, int stepOverheadDuration)
	{
		int time = step.Letter - 'A' + 1;
		if (time < 1 || time > 26) throw new Exception("Unexpected step letter.");
		time += stepOverheadDuration;
		return time;
	}

	public IEnumerable<StepWithFinishTime> GetSimultaneousStepCompletionOrder(int workersCount, int stepOverheadDuration)
	{
		List<Step> stepsLeft = Steps
			.OrderBy(step => step.Letter)
			.ToList();
		PriorityQueue<StepWithFinishTime, int> currentSteps = new();

		int currentTime = 0;
		while (stepsLeft.Count + currentSteps.Count > 0)
		{
			for (int i = 0; i < stepsLeft.Count; i++)
			{
				if (currentSteps.Count >= workersCount) break;
				Step step = stepsLeft[i];
				if (step.Requirements.All(s => s.Finished))
				{
					int stepFinishTime = currentTime + CalculateStepTime(step, stepOverheadDuration);
					currentSteps.Enqueue(
						new StepWithFinishTime(step, stepFinishTime),
						stepFinishTime
					);
					stepsLeft.RemoveAt(i--);
				}
			}

			StepWithFinishTime workedOnStep = currentSteps.Dequeue();
			currentTime = workedOnStep.FinishTime;
			workedOnStep.Step.Finished = true;
			yield return workedOnStep;
			while (currentSteps.Count > 0 && currentSteps.Peek().FinishTime <= currentTime)
			{
				workedOnStep = currentSteps.Dequeue();
				workedOnStep.Step.Finished = true;
				yield return workedOnStep;
			}
		}

		ResetStepsFinishedStatus();
	}
}
