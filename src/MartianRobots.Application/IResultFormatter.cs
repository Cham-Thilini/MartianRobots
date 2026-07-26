using MartianRobots.Domain;

namespace MartianRobots.Application;

/// <summary>Renders robot end-states into the output text format.</summary>
public interface IResultFormatter
{
    string Format(IEnumerable<RobotState> results);
}
