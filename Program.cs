using TemperatureSensor.Services;

class Program
{
    static void Main(string[] args)
    {
        var sensorService = new SensorService();
        sensorService.InitialiseSensor("DataCenterSensor", "Room 1", 22, 24);
    }
}
