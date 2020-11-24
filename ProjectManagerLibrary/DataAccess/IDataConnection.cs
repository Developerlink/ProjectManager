using ProjectManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.DataAccess
{
    public interface IDataConnection
    {
        List<Day> GetDays();
        void UpdateDay(Day day);
        void InsertProject(Project project);
        List<Project> GetProjects();
        Project GetProject(int id);
        List<ProjectManagerLibrary.Models.Task> GetTasks();
        List<ProjectManagerLibrary.Models.Task> GetSubTasks();
        List<ProjectManagerLibrary.Models.Task> GetSubSubTasks();

    }
}
