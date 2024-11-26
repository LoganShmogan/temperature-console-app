using TemperatureSensor.Services;

class Program
{
    static void Main(string[] args)
    {
        string logFilePath = "sensor_logs.txt";
        var sensorService = new SensorService(logFilePath);

        sensorService.InitialiseSensor("DataCenterSensor", "Room 1", 22, 24);

        // Start simulation in a separate thread
        var simulationThread = new Thread(sensorService.StartSensor);
        simulationThread.Start();

        Console.WriteLine("Type 'stop' to shut down the sensor:");
        while (true)
        {
            string input = Console.ReadLine();
            if (input?.Trim().ToLower() == "stop")
            {
                sensorService.StopSensor();
                break;
            }
        }

        // Wait for the simulation thread to stop
        simulationThread.Join();

        // Initialize the sensor
        sensorService.InitialiseSensor("DataCenterSensor", "Room 1", 22, 24);

        // Start temperature simulation
        sensorService.StartSensor();

        // sensorService.ShutdownSensor();

        // Restart the sensor after shutdown
        sensorService.RestartSensor();

        sensorService.ShutdownSensorWithConfirmation();
    }
}