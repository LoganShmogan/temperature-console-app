using TemperatureSensor.Services;

class Program
{
    static void Main(string[] args)
    {
        // Set the log file path
        string logFilePath = "sensor_logs.txt";

        // Initialize the SensorService with logging
        var sensorService = new SensorService(logFilePath);

        // Initialize the sensor
        sensorService.InitialiseSensor("DataCenterSensor", "Room 1", 22, 24);

        // Start temperature simulation
        sensorService.StartSensor();

        // Display stored data history
        sensorService.DisplayDataHistory();
    }
}
