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
            {
                _logger.Log($"Valid data: {sensorData:F2}°C");
                StoreData(sensorData); // Store valid data
            }
            else
            {
                _logger.Log($"Invalid data detected: {sensorData:F2}°C");
            }

            return isValid;
        }

        public void DisplayDataHistory()
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            Console.WriteLine($"Data history for sensor '{_sensor.Name}':");
            foreach (var reading in _sensor.DataHistory)
            {
                Console.WriteLine($"- {reading:F2}°C");
            }
        }

        public double SmoothData(int windowSize = 3)
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            if (_sensor.DataHistory.Count < windowSize)
                throw new InvalidOperationException($"Not enough data to apply smoothing. Need at least {windowSize} readings.");

            // Get the most recent values for the window
            var recentValues = _sensor.DataHistory
                .Skip(Math.Max(0, _sensor.DataHistory.Count - windowSize))
                .Take(windowSize);

            // Calculate the moving average
            double smoothedValue = recentValues.Average();
            _logger.Log($"Smoothed data: {smoothedValue:F2}°C (using last {windowSize} readings)");

            return smoothedValue;
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

        public void StoreData(double sensorData)
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            // Add the validated reading to the sensor's history
            _sensor.DataHistory.Add(sensorData);

            // Log the storage action
            _logger.Log($"Data stored: {sensorData:F2}°C");
        }


        public void StartSensor()
        {
            Console.WriteLine("Starting sensor simulation...");
            _logger.Log("Sensor simulation started.");

            for (int i = 0; i < 10; i++) // Simulate 10 readings
            {
                double reading = SimulateData();
                if (ValidateData(reading))
                {
                    StoreData(reading);

                    // Apply smoothing after storing at least 3 readings
                    if (_sensor.DataHistory.Count >= 3)
                    {
                        SmoothData(); // Default window size of 3
                    }
                }
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
