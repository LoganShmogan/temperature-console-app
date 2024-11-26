using TemperatureSensor.Models;

namespace TemperatureSensor.Services
{
    public class SensorService
    {
        private Sensor? _sensor;

        public void InitialiseSensor(string name, string location, double minValue, double maxValue)
        {
            _sensor = new Sensor(name, location, minValue, maxValue);
            Console.WriteLine($"Sensor '{name}' initialized at location '{location}' with range [{minValue}°C - {maxValue}°C].");
        }
    }
}
