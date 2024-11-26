using System.Collections.Generic;

namespace TemperatureSensor.Models
{
    public class Sensor
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public List<double> DataHistory { get; set; }

        public Sensor(string name, string location, double minValue, double maxValue)
        {
            Name = name;
            Location = location;
            MinValue = minValue;
            MaxValue = maxValue;
            DataHistory = new List<double>();
        }
    }
}
