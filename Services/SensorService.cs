using System;
using TemperatureSensor.Models;

namespace TemperatureSensor.Services
{
    public class SensorService
    {
        private Sensor? _sensor;

        public SensorService() { }

        // Simulates temperature data within the sensor's range
        public double SimulateData()
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            Random random = new Random();
            double noise = random.NextDouble() * 0.5 - 0.25; // Noise between -0.25 and 0.25
            return random.NextDouble() * (_sensor.MaxValue - _sensor.MinValue) + _sensor.MinValue + noise;
        }

        // Call this to start simulation (later extend for multiple iterations)
        public void StartSensor()
        {
            Console.WriteLine("Starting sensor simulation...");
            for (int i = 0; i < 10; i++) // Simulate 10 readings
            {
                double reading = SimulateData();
                Console.WriteLine($"Simulated Reading: {reading:F2}°C");
            }
        }

        public void InitialiseSensor(string name, string location, double minValue, double maxValue)
        {
            _sensor = new Sensor(name, location, minValue, maxValue);
            Console.WriteLine($"Sensor '{name}' initialized at location '{location}' with range [{minValue}°C - {maxValue}°C].");
        }
    }
}
