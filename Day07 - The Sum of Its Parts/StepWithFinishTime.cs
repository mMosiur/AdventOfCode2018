namespace AdventOfCode.Year2018.Day07;

public struct StepWithFinishTime
{
	public Step Step { get; }
	public int FinishTime { get; }

	public StepWithFinishTime(Step step, int finishTime)
	{
		Step = step;
		FinishTime = finishTime;
	}
}
