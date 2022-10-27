using System.Text;

namespace AdventOfCode.Year2018.Day12;

class PotRow
{
	private int indexShift = 0;
	private PotState outerValue = PotState.Empty;
	private PotState[] _pots;

	private static readonly PotState[] EmptyState = new PotState[5]
	{
		PotState.Empty,
		PotState.Empty,
		PotState.Empty,
		PotState.Empty,
		PotState.Empty
	};

	public int FirstNonEmptyIndex => indexShift;
	public int LastNonEmptyIndex => _pots.Length + indexShift - 1;

	public PotRow(IEnumerable<PotState> initialState)
	{
		_pots = initialState.ToArray();
		int leftEmpty = _pots.TakeWhile(pot => pot is PotState.Empty).Count();
		int rightEmpty = _pots.AsEnumerable().Reverse().TakeWhile(pot => pot is PotState.Empty).Count();
		_pots = _pots[leftEmpty..^rightEmpty];
		indexShift = leftEmpty;
	}

	public PotState this[int index]
	{
		get
		{
			index -= indexShift;
			if (index < 0 || index >= _pots.Length)
			{
				return outerValue;
			}
			return _pots[index];
		}
	}

	public ReadOnlySpan<PotState> this[int startIndex, int endIndex]
	{
		get
		{
			int length = endIndex - startIndex + 1;
			startIndex -= indexShift;
			endIndex -= indexShift;
			if (startIndex >= 0 && endIndex < _pots.Length)
			{
				return _pots.AsSpan(startIndex, length);
			}
			PotState[] result = new PotState[length];
			for (int i = 0; i < length; i++)
			{
				result[i] = this[startIndex + i + indexShift];
			}
			return result.AsSpan();
		}
	}

	private IEnumerable<PotState> NextGenerationPots(PotTransformNotes notes)
	{
		int start = FirstNonEmptyIndex - 2;
		int end = LastNonEmptyIndex + 2;
		for (int i = start; i <= end; i++)
		{
			ReadOnlySpan<PotState> pots = this[i - 2, i + 2];
			yield return notes.GetNextStateForPots(pots);
		}
	}

	public void NextGeneration(PotTransformNotes notes)
	{
		PotState[] nextGeneration = NextGenerationPots(notes).ToArray();
		int leftEmpty = nextGeneration.TakeWhile(pot => pot is PotState.Empty).Count();
		int rightEmpty = nextGeneration.Reverse().TakeWhile(pot => pot is PotState.Empty).Count();
		_pots = nextGeneration[leftEmpty..^rightEmpty];
		indexShift = indexShift - 2 + leftEmpty;
		outerValue = notes.GetNextStateForPots(EmptyState);
	}

	public override string ToString()
	{
		StringBuilder builder = new();
		for (int i = FirstNonEmptyIndex; i <= LastNonEmptyIndex; i++)
		{
			char c = this[i] switch
			{
				PotState.Empty => '.',
				PotState.Plant => '#',
				_ => throw new ApplicationException($"Unexpected pot state: {this[i]}")
			};
			builder.Append(c);
		}
		return builder.ToString();
	}
}
