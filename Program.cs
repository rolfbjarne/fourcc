using System.Globalization;

foreach (var arg in args) {
	if (uint.TryParse (arg, out var i)) {
		Console.WriteLine ($"Detected integer value '{i}'. Hex: 0x{i:x} FourCC: {ToFourCC (i)}");
	} else if (arg.StartsWith ("0x", StringComparison.Ordinal) && uint.TryParse (arg [2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var h)) {
		Console.WriteLine ($"Detected integer value '{h}'. Hex: 0x{h:x} FourCC: {ToFourCC (h)}");
	} else if (TryFromFourCC (arg, out var s)) {
		Console.WriteLine ($"Detected string value '{arg}'. Hex: 0x{s:x} Integer: {s}");
	} else {
		Console.Error.WriteLine ($"Invalid fourcc (too long, {arg.Length} characters): '{arg}'");
	}
}

static string ToFourCC (uint value)
{
	return $"{(char) ((value >> 24) & 0xFF)}{(char) ((value >> 16) & 0xFF)}{(char) ((value >> 8) & 0xFF)}{(char) ((value >> 0) & 0xFF)}";
}
static bool TryFromFourCC (string value, out uint fourcc)
{
	fourcc = uint.MaxValue;

	if (value.Length > 4) {
		return false;
	}
	uint rv = 0;
	for (var i = 0; i < value.Length; i++) {
		rv <<= 8;
		rv |= ((byte) (ushort) value [i]);
	}
	fourcc = rv;
	return true;
}
