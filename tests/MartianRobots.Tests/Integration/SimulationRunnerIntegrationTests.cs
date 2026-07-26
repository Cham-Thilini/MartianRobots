using MartianRobots.Application;

namespace MartianRobots.Tests.Integration;

public class SimulationRunnerIntegrationTests
{
    [Fact]
    public void Run_processes_multiple_robots_and_preserves_scents_between_missions()
    {
        const string input = """
            5 3
            1 1 E
            RFRFRFRF
            3 2 N
            FRRFLLFFRRFLL
            0 3 W
            LLFFFLFLFL
            """;

        var output = SimulationRunner.CreateDefault().Run(input);

        Assert.Equal("1 1 E\n3 3 N LOST\n2 3 S", output);
    }
}
