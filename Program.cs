using TemperatureSensor.Services;

class Program
{
    static void Main(string[] args)
    {
        var sensorService = new SensorService();

        // Initialize the sensor
        sensorService.InitialiseSensor("DataCenterSensor", "Room 1", 22, 24);

        // Start temperature simulation
        sensorService.StartSensor();
    }
}
