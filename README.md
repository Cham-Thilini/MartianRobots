# MartianRobots# Martian Robots

A C#/.NET 8 console application implementing the **Martian Robots** programming challenge.

Robots navigate a bounded grid on Mars by executing a sequence of movement instructions (`L`, `R`, `F`). When a robot moves beyond the edge of the grid, it becomes **LOST** and leaves a **scent** at its last valid position. Future robots ignore any move that would cause them to be lost from the same scented location.

---

## Requirements

- .NET 8 SDK or later

---

## Project Structure

```
src/
├── MartianRobots.Domain
├── MartianRobots.Application
└── MartianRobots.Console

tests/
└── MartianRobots.Tests

samples/
├── official-example-input.txt
├── official-example-output.txt
├── lost-robot-input.txt
├── lost-robot-output.txt
├── scent-behaviour-input.txt
└── scent-behaviour-output.txt
```

---

## Running the Application

Run using one of the provided sample files:

```bash
dotnet run --project src/MartianRobots.Console -- samples/official-example-input.txt
```

or provide input via standard input:

```bash
dotnet run --project src/MartianRobots.Console
```

Then paste the input and press:

- **Ctrl+Z**, then **Enter** (Windows)
- **Ctrl+D** (Linux/macOS)

Run the test suite:

```bash
dotnet test
```

---

## Sample Scenario

### Input

```text
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
```

### Output

```text
1 1 E
3 3 N LOST
2 3 S
```

Additional scenarios are available in the `samples` directory, including:

- Official challenge example
- Robot becoming **LOST**
- Scent preventing a subsequent robot from being lost at the same location

---

## Architecture

The solution follows a lightweight Clean Architecture approach, separating business rules from parsing and console input/output.

### Domain

Contains the core business rules and domain model:

- Coordinates
- Orientation
- Robot state
- Grid and scent tracking
- Robot commands

The domain project has no dependency on parsing or console I/O.

### Application

Orchestrates the simulation by:

- Parsing input
- Executing robot instructions
- Formatting results

### Console

Provides a minimal command-line interface for reading input and writing output.

---

## Design Decisions

The implementation prioritises simplicity while remaining easy to extend.

### Command Pattern

Each instruction (`L`, `R`, `F`) is implemented as an `IRobotCommand`.

This allows additional instruction types to be introduced without modifying the simulation engine, following the Open/Closed Principle.

### Separation of Concerns

Parsing, simulation and output formatting are isolated from the domain model, making the code easier to understand, test and maintain.

### Shared World State

A single `MarsWorld` instance is shared across all robot missions so that scents created by one robot affect subsequent robots, matching the challenge specification.

---

## Assumptions

Where the specification leaves behaviour open, the following assumptions were made:

- Blank lines in the input are ignored.
- Instruction and orientation letters are treated case-insensitively.
- Grid coordinates are validated to remain within the specified range (0–50).
- Instruction strings must contain fewer than 100 characters.
- Robot execution stops immediately once a robot becomes **LOST**.
- Invalid input results in a descriptive `FormatException`.

---

## Testing

The solution includes comprehensive unit tests covering:

- Robot movement and turning
- Grid boundary behaviour
- Lost robot and scent rules
- Input parsing and validation
- Command resolution
- Simulation execution

It also includes end-to-end integration tests verifying:

- The official challenge scenario
- Multiple robot missions
- Shared scent behaviour across robots

---

## AI Usage

AI was used as a development assistant for brainstorming, reviewing design ideas, generating boilerplate and improving documentation.

All architectural decisions, domain modelling, implementation of the simulation logic, testing and final review were completed and verified by me.

---

## Future Enhancements

If this solution were to evolve into a larger system, potential enhancements include:

- Exposing the simulation through an ASP.NET Core API
- Adding a CI pipeline with automated build and test execution
- Containerising the application with Docker
- Persisting simulation requests and results
- Adding structured logging and monitoring