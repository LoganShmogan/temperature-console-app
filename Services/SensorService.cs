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

        public void ShutdownSensor()
        {
            if (_sensor == null)
            {
                Console.WriteLine("Sensor is not initialized.");
                _logger.Log("Attempted to shut down a non-initialized sensor.");
                return;
            }

            // Clear data history
            _sensor.DataHistory.Clear();

            // Log the shutdown event
            _logger.Log($"Sensor '{_sensor.Name}' shut down. Data history cleared.");
            Console.WriteLine($"Sensor '{_sensor.Name}' has been shut down and reset.");
        }

        public void ShutdownSensorWithConfirmation()
        {
            if (_sensor == null)
            {
                Console.WriteLine("Sensor is not initialized.");
                _logger.Log("Attempted to shut down a non-initialized sensor.");
                return;
            }

            Console.Write("Are you sure you want to shut down the sensor? (yes/no): ");
            string response = Console.ReadLine();

            if (response?.Trim().ToLower() == "yes")
            {
                ShutdownSensor(); // Call the existing shutdown method
            }
            else
            {
                Console.WriteLine("Shutdown aborted.");
                _logger.Log("Shutdown aborted by user.");
            }
        }


        public void RestartSensor()
        {
            if (_sensor == null)
            {
                Console.WriteLine("Sensor is not initialized.");
                _logger.Log("Attempted to restart a non-initialized sensor.");
                return;
            }

            // Clear historical data
            _sensor.DataHistory.Clear();

            // Log and confirm restart
            _logger.Log($"Sensor '{_sensor.Name}' restarted. Data history cleared.");
            Console.WriteLine($"Sensor '{_sensor.Name}' has been restarted.");
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


        public bool DetectAnomaly(double sensorData, double threshold = 1.0)
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            if (_sensor.DataHistory.Count < 3)
            {
                _logger.Log("Not enough data for anomaly detection. Defaulting to no anomaly.");
                return false; // Not enough data to calculate an average
            }

            // Calculate the moving average (last 3 readings)
            var recentValues = _sensor.DataHistory
                .Skip(Math.Max(0, _sensor.DataHistory.Count - 3))
                .Take(3);
            double average = recentValues.Average();

            // Detect if the current reading deviates significantly
            bool isAnomaly = Math.Abs(sensorData - average) > threshold;
            if (isAnomaly)
            {
                _logger.Log($"Anomaly detected: {sensorData:F2}°C (deviation from average: {Math.Abs(sensorData - average):F2})");
            }
            return isAnomaly;
        }

        private bool _isRunning;

        public void StartSensor()
        {
            if (_sensor == null)
                throw new InvalidOperationException("Sensor is not initialized.");

            Console.WriteLine("Starting sensor simulation...");
            _logger.Log("Sensor simulation started.");

            _isRunning = true;

            while (_isRunning)
            {
                double reading = SimulateData();
                if (ValidateData(reading))
                {
                    StoreData(reading);

                    // Apply anomaly detection (optional)
                    DetectAnomaly(reading);

                    // Apply data smoothing
                    if (_sensor.DataHistory.Count >= 3)
                    {
                        SmoothData();
                    }
                }

                // Simulate a delay between readings
                Thread.Sleep(1000);
            }

            _logger.Log("Sensor simulation stopped.");
            Console.WriteLine("Sensor simulation stopped.");
        }

        public void StopSensor()
        {
            _isRunning = false;
        }




        public void InitialiseSensor(string name, string location, double minValue, double maxValue)
        {
            _sensor = new Sensor(name, location, minValue, maxValue);
            _logger.Log($"Sensor '{name}' initialized at location '{location}' with range [{minValue}°C - {maxValue}°C].");
        }

    }
}
