namespace AdventOfCode.Year2018.Day08;

public struct LicenseTreeNodeHeader
{
	public int ChildNodesCount { get; }
	public int MetadataEntriesCount { get; }

	public LicenseTreeNodeHeader(int childNodesCount, int metadataEntriesCount)
	{
		ChildNodesCount = childNodesCount;
		MetadataEntriesCount = metadataEntriesCount;
	}
}
