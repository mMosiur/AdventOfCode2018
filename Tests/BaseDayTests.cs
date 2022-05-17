using AdventOfCode.Abstractions;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

public abstract class BaseDayTests<T> where T : DaySolver
{
	public const string BaseInputsDirectory = "Inputs";
	public abstract string DayInputsDirectory { get; }

	/// <summary>
	/// We can't specify parametrized constraint for T argument, so we have to use this workaround.
	/// Subclasses should override this property with simply "return new <T>(inputFilePath);".
	/// </summary>
	protected abstract T CreateSolver(string inputFilePath);

	private string GetInputFilePath(string inputFilename)
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

	public virtual void TestPart1(string inputFilename, string expectedResult)
	{
		string filepath = GetInputFilePath(inputFilename);
		T solver = CreateSolver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	public virtual void TestPart2(string inputFilename, string expectedResult)
	{
		string filepath = GetInputFilePath(inputFilename);
		T solver = CreateSolver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
