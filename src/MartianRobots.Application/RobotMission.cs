using MartianRobots.Domain;

namespace MartianRobots.Application;

/// <summary>One robot's job: where it starts and the instruction string to execute.</summary>
public sealed record RobotMission(RobotState Start, string Instructions);

/// <summary>The fully parsed input: the world to run in and the robots to run, in order.</summary>
public sealed record ParsedInput(Grid Grid, IReadOnlyList<RobotMission> Missions);
