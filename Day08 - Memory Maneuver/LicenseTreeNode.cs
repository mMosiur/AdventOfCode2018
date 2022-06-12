namespace AdventOfCode.Year2018.Day08;

public class LicenseTreeNode
{
	public LicenseTreeNodeHeader Header { get; }
	public IReadOnlyList<LicenseTreeNode> ChildNodes { get; }
	public IReadOnlyList<int> MetadataEntries { get; }

	public LicenseTreeNode(LicenseTreeNodeHeader header, IReadOnlyList<LicenseTreeNode> childNodes, IReadOnlyList<int> metadataEntries)
	{
		Header = header;
		ChildNodes = childNodes;
		MetadataEntries = metadataEntries;
	}

	public static LicenseTreeNode BuildFromNumberStructure(ReadOnlySpan<int> numbers, out int nodeLength)
	{
		try
		{
			LicenseTreeNodeHeader header = new(numbers[0], numbers[1]);
			nodeLength = 2;
			LicenseTreeNode[] childNodes = new LicenseTreeNode[header.ChildNodesCount];
			for (int childIndex = 0; childIndex < header.ChildNodesCount; childIndex++)
			{
				childNodes[childIndex] = BuildFromNumberStructure(numbers[nodeLength..], out int childNodeLength);
				nodeLength += childNodeLength;
			}
			int[] metadataEntries = numbers.Slice(nodeLength, header.MetadataEntriesCount).ToArray();
			nodeLength += header.MetadataEntriesCount;
			return new LicenseTreeNode(header, childNodes, metadataEntries);
		}
		catch (IndexOutOfRangeException e)
		{
			throw new InvalidNumberStructure("Not enough numbers to build the tree node.", e);
		}
		catch (ArgumentOutOfRangeException e)
		{
			throw new InvalidNumberStructure("Not enough numbers to build the tree node.", e);
		}
	}

	public int CalculateMetadataEntriesSum()
	{
		return MetadataEntries.Sum() + ChildNodes.Sum(c => c.CalculateMetadataEntriesSum());
	}

	public int CalculateValue()
	{
		return ChildNodes.Count == 0
			? MetadataEntries.Sum()
			: MetadataEntries
				.Select(entry => entry - 1)
				.Where(childIndex => childIndex >= 0 && childIndex < ChildNodes.Count)
				.Select(childIndex => ChildNodes[childIndex])
				.Sum(node => node.CalculateValue());
	}
}
