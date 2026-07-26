using System.Text;
using MartianRobots.Domain;

namespace MartianRobots.Application;

/// <summary>
/// Formats each robot as "<c>x y O</c>", with " LOST" appended for robots that drove off
/// the grid. One robot per line.
/// </summary>
public sealed class ResultFormatter : IResultFormatter
{
    public string Format(IEnumerable<RobotState> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var builder = new StringBuilder();
        foreach (var state in results)
        {
            if (builder.Length > 0)
                builder.Append('\n');
            builder.Append(FormatOne(state));
        }

        return builder.ToString();
    }

    private static string FormatOne(RobotState state)
    {
        var position = $"{state.Position.X} {state.Position.Y} {state.Orientation.ToSymbol()}";
        return state.IsLost ? $"{position} LOST" : position;
    }
}
