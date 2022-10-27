namespace AdventOfCode.Year2018.Day12;

class PotTransformNotes
{
	private readonly Dictionary<byte, PotState> _notes;
	private readonly PotState? _stateForAbsentNotes;

	public PotTransformNotes(IEnumerable<PotTransformNote> notes, PotState? stateForAbsentNotes = null)
	{
		_stateForAbsentNotes = stateForAbsentNotes;
		if (notes.TryGetNonEnumeratedCount(out int count))
		{
			_notes = new Dictionary<byte, PotState>(count);
		}
		else
		{
			_notes = new Dictionary<byte, PotState>();
		}
		foreach (PotTransformNote note in notes)
		{
			byte from = EncodePotStates(note.From);
			_notes.Add(from, note.To);
		}
	}

	private static byte EncodePotStates(ReadOnlySpan<PotState> pots)
	{
		if (pots.Length != 5)
		{
			throw new ArgumentException("You can only encode 5 pots.");
		}
		byte result = 0;
		for (byte i = 0; i < pots.Length; i++)
		{
			result *= 2;
			result += (byte)pots[i];
		}
		return result;
	}

	public PotState GetNextStateForPots(ReadOnlySpan<PotState> pots)
	{
		try
		{
			byte key = EncodePotStates(pots);
			if (_stateForAbsentNotes.HasValue)
			{
				return _notes.GetValueOrDefault(key, _stateForAbsentNotes.Value);
			}
			else
			{
				return _notes[key];
			}
		}
		catch (KeyNotFoundException e)
		{
			string potsRepresentation = string.Join(',', pots.ToArray());
			throw new ApplicationException($"No note found for pots: {potsRepresentation}", e);
		}
	}
}
