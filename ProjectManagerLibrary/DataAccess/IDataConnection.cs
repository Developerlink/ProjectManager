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
        void InsertTask(Models.Task task, Project project);
        void UpdateTask(Models.Task task);
        void DeleteTask(Models.Task task);
        void DeleteSubTask(Models.Task task);
        void DeleteSubSubTask(Models.Task task);

        List<Models.Task> GetTasks();
        List<Models.Task> GetSubTasks();
        List<Models.Task> GetSubSubTasks();

    }
}
