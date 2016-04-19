using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2
{
    public class TimeSeries
    {
        public List<int> Values { get; set; }

        public TimeSeries()
        {
            Values = new List<int>();
        } 

        public static TimeSeries Load(string filename)
        {
            string data;
            using (var sr = new StreamReader(filename))
            {
                data = sr.ReadToEnd();
            }

            return new TimeSeries()
            {
                Values = data.Split(' ').Select(int.Parse).ToList()
            };
        }
    }
}
