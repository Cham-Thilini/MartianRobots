namespace MartianRobots.Application;

/// <summary>Turns raw input text into a validated <see cref="ParsedInput"/>.</summary>
public interface IMissionParser
{
    ParsedInput Parse(string input);
}
