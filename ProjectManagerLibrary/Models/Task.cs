using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public TimeSpan EstimatedTimeNeeded { get; set; }
        public TimeSpan ActualTimeUsed { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public int WorkDays { get; set; }
        public List<Task> SubTaskList { get; set; }
    }
}
