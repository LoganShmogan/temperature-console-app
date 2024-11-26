using System;
using System.IO;

namespace TemperatureSensor.Services
{
    public class Logger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;

            // Create or clear the log file on initialization
            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath).Dispose();
            }
        }

        public void Log(string message)
        {
            string timestampedMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(_logFilePath, timestampedMessage + Environment.NewLine);

            // Optionally, also log to the console
            Console.WriteLine(timestampedMessage);
        }
    }
}
