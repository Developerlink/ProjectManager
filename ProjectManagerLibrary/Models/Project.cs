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
        public DateTime StartDate { get; set; } = DateTime.Parse("1800-01-01");          
        public DateTime EstimatedEndDate { get; set; } = DateTime.Parse("1800-01-01");
        public DateTime ActualEndDate { get; set; } = DateTime.Parse("1800-01-01");
        public int EstimatedHours { get; set; }
        public int ActualHours { get; set; }
        public int EstimatedDays { get; set; }
        public int ActualDays { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ProjectType { get; set; }
        public string TechStack { get; set; }
        public bool IsEnded { get; set; }
        public string WorkSpace { get; set; }
        public List<Task> TaskList { get; set; } = new List<Task>();
    }
}
