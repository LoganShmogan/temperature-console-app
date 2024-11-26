Temperature Console App

The Temperature Console App simulates a temperature sensor in a data center room. The application generates realistic temperature readings, validates the data, detects anomalies, logs behavior, and stores readings for analysis. It is built in C# using .NET and follows a modular, test-driven development process.
Features
Core Features

    Sensor Initialization:
        Configure sensors with a name, location, and temperature range.
    Data Simulation:
        Generate random temperature readings with noise for realism.
    Data Validation:
        Ensure readings fall within the acceptable range.
    Data Logging:
        Log readings and events to a file for analysis.
    Data Storage:
        Maintain a history of readings for review.
    Data Smoothing:
        Apply a moving average to smooth noisy data.
    Anomaly Detection:
        Identify outliers or abnormal temperature spikes.
    Sensor Shutdown/Restart:
        Gracefully shut down or restart the sensor.

Getting Started
Prerequisites

    .NET SDK 7.0 or newer
    A terminal or IDE supporting C# development (e.g., Visual Studio, VS Code).

Installation

    Clone the repository:

git clone https://github.com/your-username/temperature-console-app.git
cd temperature-console-app

Build the solution:

dotnet build

Run the application:

    dotnet run --project temperature-console-app

Usage

    When the application starts, it initializes the sensor with default settings.
    The sensor begins simulating temperature readings in a separate thread.
    Type stop in the console to shut down the sensor gracefully.
    The application logs sensor behavior to sensor_logs.txt.

Project Structure

temperature-console-app/
├── Program.cs                  # Entry point
├── Models/
│   └── Sensor.cs               # Sensor data model
├── Services/
│   ├── Logger.cs               # Handles logging
│   ├── SensorService.cs        # Core sensor logic
│   └── ProgramService.cs       # Encapsulates main program logic
TemperatureSensor.Tests/        # Unit tests for the application

Testing

    Navigate to the test project directory:

cd TemperatureSensor.Tests

Run tests:

dotnet test --collect:"XPlat Code Coverage"

View code coverage using ReportGenerator:

    reportgenerator -reports:TestResults/**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html

Development Process
Git Workflow

    Feature Branching: Each feature was developed on its own branch.
    Frequent Commits: Descriptive messages documented the development progress.
    Pull Requests: Features were merged into the main branch after code review.

Testing

    Unit tests were written for all core features, achieving over 70% code coverage.
    Simulated user input was used to test interactive components.

Challenges Faced

    Namespace Issues: Early namespace mismatches caused build errors.
    Interactive Testing: Simulating user input required refactoring program logic into testable services.
    Merge Conflicts: Conflicts in Git were resolved by clearly defining task boundaries.

Future Improvements

    Add a UI for real-time data visualization.
    Implement advanced anomaly detection using machine learning.
    Allow dynamic sensor reconfiguration through user input.

License

This project is licensed under the MIT License.
Acknowledgments

    Coverlet: For code coverage tools.
    xUnit: For unit testing.
    Community resources and online tutorials for their invaluable guidance.
