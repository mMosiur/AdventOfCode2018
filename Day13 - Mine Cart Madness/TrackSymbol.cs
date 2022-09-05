namespace AdventOfCode.Year2018.Day13;

public enum TrackSymbol
{
	Empty = ' ',
    TrackHorizontal = '-',
	TrackVertical = '|',
	TrackDownSlopeConnection = '\\',
	TrackUpSlopeConnection = '/',
	TrackIntersection = '+',
	CartUp = '^',
	CartDown = 'v',
	CartLeft = '<',
	CartRight = '>'
}
