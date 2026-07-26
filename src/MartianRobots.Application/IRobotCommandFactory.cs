using MartianRobots.Domain.Commands;

namespace MartianRobots.Application;

/// <summary>Resolves an instruction character to the command that implements it.</summary>
public interface IRobotCommandFactory
{
    /// <summary>Returns the command for <paramref name="token"/>, or throws if it is unknown.</summary>
    IRobotCommand Create(char token);
}
