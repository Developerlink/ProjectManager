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
        public DateTime EstimatedStartDate { get; set; } = DateTime.Parse("1800-01-01");
        public DateTime ActualStartDate { get; set; } = DateTime.Parse("1800-01-01");
        public DateTime EstimatedEndDate { get; set; } = DateTime.Parse("1800-01-01");
        public DateTime ActualEndDate { get; set; } = DateTime.Parse("1800-01-01");
        public int EstimatedHoursInMinutes { get; set; }
        public int ActualHoursInMinutes { get; set; }
        public int EstimatedDays { get; set; }
        public int ActualDays { get; set; }
        public int ProjectID { get; set; }
        public int ParentTaskID { get; set; }
        public int HourBalanceInMinutes { get; set; }
        public int DayBalance { get; set; }



        // 1 = task
        // 2 = subtask
        // 3 = subsubtask
        public int TaskLevel { get; set; }
        public List<Task> SubTaskList { get; set; } = new List<Task>();
    }
}
