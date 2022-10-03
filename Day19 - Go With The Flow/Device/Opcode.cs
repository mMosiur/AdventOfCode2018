namespace AdventOfCode.Year2018.Day19.Device;

// Opcodes are defined according to my own input result from day 16.
public enum Opcode : byte
{
	AddRegister = 10,
	AddImmediate = 6,
	MultiplyRegister = 9,
	MultiplyImmediate = 0,
	BitwiseAndRegister = 14,
	BitwiseAndImmediate = 2,
	BitwiseOrRegister = 11,
	BitwiseOrImmediate = 12,
	SetRegister = 15,
	SetImmediate = 1,
	GreaterThanImmediateRegister = 8,
	GreaterThanRegisterImmediate = 3,
	GreaterThanRegisterRegister = 4,
	EqualImmediateRegister = 7,
	EqualRegisterImmediate = 13,
	EqualRegisterRegister = 5,
}
