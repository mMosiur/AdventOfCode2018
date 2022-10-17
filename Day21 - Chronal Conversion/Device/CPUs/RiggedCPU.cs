namespace AdventOfCode.Year2018.Day21.Device.CPUs;

public class RiggedCPU
{
	private readonly ulong _targetRegisterResetValue;

	public RiggedCPU(ulong targetRegisterResetValue)
	{
		_targetRegisterResetValue = targetRegisterResetValue;
	}

	public IEnumerable<ulong> FindNonHaltingValues()
	{
		ISet<ulong> seenTargetRegisterValues = new HashSet<ulong>();
		ulong targetRegister = 0;
		while (true)
		{
			ulong tempRegister = targetRegister | 65536;
			targetRegister = _targetRegisterResetValue;
			while (true)
			{
				targetRegister = (((targetRegister + (tempRegister & 255)) & 16777215) * 65899) & 16777215;
				if (256 > tempRegister)
				{
					bool duplicate = !seenTargetRegisterValues.Add(targetRegister);
					if (duplicate)
					{
						yield break;
					}
					yield return targetRegister;
					break;
				}
				else
				{
					tempRegister /= 256;
				}
			}
		}
	}
}
