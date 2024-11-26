using System;
using TemperatureSensor.Models;

namespace TemperatureSensor.Services
{
    public class SensorService
    {
        private readonly Logger _logger;


        public bool ValidateData(double sensorData)
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            bool isValid = sensorData >= _sensor.MinValue && sensorData <= _sensor.MaxValue;

            if (isValid)
                _logger.Log($"Valid data: {sensorData:F2}°C");
            else
                _logger.Log($"Invalid data detected: {sensorData:F2}°C");

            return isValid;
        }


        private Sensor? _sensor;

        public SensorService(string logFilePath)
        {
            _logger = new Logger(logFilePath);
        }


        // Simulates temperature data within the sensor's range
        public double SimulateData()
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            Random random = new Random();
            double noise = random.NextDouble() * 0.5 - 0.25; // Noise between -0.25 and 0.25
            return random.NextDouble() * (_sensor.MaxValue - _sensor.MinValue) + _sensor.MinValue + noise;
        }

        public void StartSensor()
        {
            Console.WriteLine("Starting sensor simulation...");
            _logger.Log("Sensor simulation started.");

            for (int i = 0; i < 10; i++) // Simulate 10 readings
            {
                double reading = SimulateData();
                ValidateData(reading);
            }

            _logger.Log("Sensor simulation completed.");
        }



        public void InitialiseSensor(string name, string location, double minValue, double maxValue)
        {
            _sensor = new Sensor(name, location, minValue, maxValue);
            _logger.Log($"Sensor '{name}' initialized at location '{location}' with range [{minValue}°C - {maxValue}°C].");
        }

    }
}
