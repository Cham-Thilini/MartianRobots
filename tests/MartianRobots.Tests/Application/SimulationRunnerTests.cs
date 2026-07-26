using MartianRobots.Application;

namespace MartianRobots.Tests.Application;

public class SimulationRunnerTests
{
    private readonly SimulationRunner _runner = SimulationRunner.CreateDefault();

    /// <summary>
    /// The canonical acceptance test: the exact sample input and output from the brief.
    /// </summary>
    [Fact]
    public void Produces_the_expected_output_for_the_sample_input()
    {
        const string input =
            "5 3\n" +
            "1 1 E\n" +
            "RFRFRFRF\n" +
            "3 2 N\n" +
            "FRRFLLFFRRFLL\n" +
            "0 3 W\n" +
            "LLFFFLFLFL";

        const string expected =
            "1 1 E\n" +
            "3 3 N LOST\n" +
            "2 3 S";

        Assert.Equal(expected, _runner.Run(input));
    }

    [Fact]
    public void A_single_robot_that_stays_on_the_grid_reports_its_final_pose()
        => Assert.Equal("1 1 E", _runner.Run("5 3\n1 1 E\nRFRFRFRF"));

    [Fact]
    public void An_unknown_command_surfaces_as_a_format_error()
        => Assert.Throws<FormatException>(() => _runner.Run("5 3\n1 1 E\nRFXRF"));
}
