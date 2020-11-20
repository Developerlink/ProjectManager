using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.Models
{
    public class Day
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TimeSpan AvailableWorkTime { get; set; }
        public TimeSpan AvailableFreeTime { get; set; }
    }
}
