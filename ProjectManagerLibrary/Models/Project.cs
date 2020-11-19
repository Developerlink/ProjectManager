using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public int EstimatedHoursToComplete { get; set; }
        public int EstimatedDaysToComplete { get; set; }
        public int ActualDaysUsedToComplete { get; set; }
        public int ActualHoursUsedToComplete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ProjectType { get; set; }
        public string ProjectStatus { get; set; }
        public string WorkSpace { get; set; }
        public List<Task> TaskList { get; set; }
    }
}
