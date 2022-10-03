namespace AdventOfCode.Year2018.Day16;

public class OpcodeDictionaryResolver : OpcodeDictionary
{
	private readonly List<string> _unresolvedOperations;

	protected readonly Dictionary<byte, string> _resolvedOpcodeNumberToNameDictionary;
	protected readonly Dictionary<string, byte> _resolvedOpcodeNameToNumberDictionary;

	public override IReadOnlyDictionary<byte, string> OpcodeNumberToName => _resolvedOpcodeNumberToNameDictionary;

	public override IReadOnlyDictionary<string, byte> OpcodeNameToNumber => _resolvedOpcodeNameToNumberDictionary;

	public bool IsFullyResolved => _unresolvedOperations.Count == 0;

	public OpcodeDictionaryResolver()
	{
		_unresolvedOperations = new List<string>(OpcodeNames);
		_resolvedOpcodeNumberToNameDictionary = new Dictionary<byte, string>();
		_resolvedOpcodeNameToNumberDictionary = new Dictionary<string, byte>();
	}

	public OpcodeDictionary ResolveFromSamples(IEnumerable<Sample> samples)
	{
		while (!IsFullyResolved)
		{
			bool somethingChanged = false;
			foreach (Sample sample in samples)
			{
				somethingChanged |= ResolveSample(sample);
			}
			if (!somethingChanged)
			{
				throw new ApplicationException("Unable to fully resolve opcode dictionary");
			}
		}
		return this;
	}

	private bool ResolveSample(Sample sample)
	{
		Device.CPUs.NamedOpcodeCPU cpu = new(sample.RegistersBeforeOperation);
		if (_resolvedOpcodeNumberToNameDictionary.TryGetValue(sample.Operation.Opcode, out string? resolvedOperation))
		{
			// Check for contradiction
			cpu.ForceExecuteOperation(resolvedOperation, sample.Operation);
			if (!cpu.CheckRegistersEquality(sample.RegistersAfterOperation))
			{
				throw new ApplicationException("Contradictory sample found.");
			}
			return false;
		}
		resolvedOperation = null;
		foreach (string operation in _unresolvedOperations)
		{
			cpu.ForceExecuteOperation(operation, sample.Operation);
			if (cpu.CheckRegistersEquality(sample.RegistersAfterOperation))
			{
				if (resolvedOperation is not null)
				{
					// Multiple matches, so we can't resolve this sample yet
					return false;
				}
				resolvedOperation = operation;
			}
			cpu.Reset();
		}
		if (resolvedOperation is null)
		{
			throw new ApplicationException("No matches found for sample.");
		}
		_unresolvedOperations.Remove(resolvedOperation);
		_resolvedOpcodeNumberToNameDictionary.Add(sample.Operation.Opcode, resolvedOperation);
		_resolvedOpcodeNameToNumberDictionary.Add(resolvedOperation, sample.Operation.Opcode);
		return true;
	}
}
