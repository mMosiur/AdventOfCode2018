using System.Runtime.Serialization;

namespace AdventOfCode.Year2018.Day08;

public class InvalidNumberStructure : ApplicationException
{
	public InvalidNumberStructure() { }
	public InvalidNumberStructure(string message) : base(message) { }
	public InvalidNumberStructure(string message, Exception inner) : base(message, inner) { }
	public InvalidNumberStructure(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
