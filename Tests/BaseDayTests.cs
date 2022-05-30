using AdventOfCode.Abstractions;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

public abstract class BaseDayTests<TDaySolver, TDaySolverOptions>
	where TDaySolver : DaySolver, new()
	where TDaySolverOptions : DaySolverOptions, new()
{
	public const string BaseInputsDirectory = "Inputs";
	public abstract string DayInputsDirectory { get; }

	/// <summary>
	/// We can't specify parametrized constraint for <typeparamref name="TDaySolver"/> argument, so we have to use this workaround.
	/// Subclasses should override this property with simply "<c>return new <typeparamref name="TDaySolver"/>(inputFilePath);</c>".
	/// </summary>
	protected abstract TDaySolver CreateSolver(TDaySolverOptions options);

	protected string GetInputFilepath(string inputFilename)
	{
		string filepath = Path.Combine(
			BaseInputsDirectory,
			DayInputsDirectory,
			inputFilename
		);
		bool testFileExists = File.Exists(filepath);
		Assert.True(testFileExists, $"Specified test input file does not exist: {filepath}");
		return filepath;
	}

	public virtual void TestPart1(string inputFilename, string expectedResult, TDaySolverOptions? options = null)
	{
		options ??= new();
		options.InputFilepath = GetInputFilepath(inputFilename);
		TDaySolver solver = CreateSolver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	public virtual void TestPart2(string inputFilename, string expectedResult, TDaySolverOptions? options = null)
	{
		options ??= new();
		options.InputFilepath = GetInputFilepath(inputFilename);
		TDaySolver solver = CreateSolver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
