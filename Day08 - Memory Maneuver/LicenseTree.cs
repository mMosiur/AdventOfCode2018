namespace AdventOfCode.Year2018.Day08;

public class LicenseTree
{
	public LicenseTreeNode Root { get; }

	private LicenseTree(LicenseTreeNode root)
	{
		Root = root;
	}

	public static LicenseTree BuildFromNumberStructure(ReadOnlySpan<int> numbers)
	{
		LicenseTreeNode root = LicenseTreeNode.BuildFromNumberStructure(numbers, out int rootLength);
		if (rootLength != numbers.Length)
		{
			throw new InvalidNumberStructure("Not all numbers have been used to build a tree.");
		}
		return new LicenseTree(root);
	}

}
