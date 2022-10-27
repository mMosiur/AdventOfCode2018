namespace AdventOfCode.Year2018.Day13;

enum TrackSymbol
{
	Unknown = default,

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
