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
        void DeleteProject(Project project);
        List<Project> GetProjects();
        Project GetProject(int id);
        void UpdateProject(Project project);
        
        // These 3 methods handle all level of tasks
        void InsertTask(Models.Task task);
        void UpdateTaskAllLevels(Models.Task task);
        void DeleteTaskAllLevels(Models.Task task);

        void InsertSubTask(Models.Task task);
        void InsertSubSubTask(Models.Task task);


        List<Models.Task> GetAllTasks(Project project);
        List<Models.Task> GetAllSubTasks(Models.Task parentTask);
        List<Models.Task> GetAllSubSubTasks(Models.Task parentTask);

    }
}
