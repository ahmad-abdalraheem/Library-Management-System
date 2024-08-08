namespace ConsoleApp;

/// <summary>
/// ANSI Escape Sequence used get control over console/terminal. Console.Write these strings to get affect
/// Some of these commands require specific position, it's implemented as a method instead of string
/// </summary>
public struct AnsiCommands
{
	/// <summary>
	/// Clear Commands.
	/// </summary>
	public string Clear = "\x1b[2J";
	public string ClearLine = "\x1b[2K";

	/// <summary>
	/// Text Styling
	/// </summary>
	public string Hide = "\x1b[8m";
	public string Reset = "\x1b[0m";
	public string UnderLine = "\x1b[4m";
	public string Bold = "\x1b[1m";
	public string Italic = "\x1b[3m";
	
	/// <summary>
	/// Cursor Movement
	/// </summary>
	public string LineUp = "\x1b[1A";
	public string LineDown = "\x1b[1C";

	/// <summary>
	/// Cursor Position
	/// </summary>
	public string SavePosition = "\x1b[s";
	public string RestorePosition = "\x1b[u";
	public string CursorPosition(int row, int col) => $"x1b[{row};{col}H";

	/// <summary>
	/// Foreground Colors
	/// </summary>
	public string Black = "\x1b[30m";
	public string Red = "\x1b[31m";
	public string Green = "\x1b[32m";
	public string Blue = "\x1b[34m";
	public string Yellow = "\x1b[33m";
	public string Cyan = "\x1b[36m";
	public string Magenta = "\x1b[35m";
	public AnsiCommands() { }
}