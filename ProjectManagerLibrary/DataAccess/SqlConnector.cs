using Dapper;
using ProjectManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string SqlConnectionNameInAppConfig = "ProjectManagerSql";

        public List<Day> GetDays()
        {
            List<Day> output;

            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                output = conn.Query<Day>("dbo.spDays_GetAll").ToList();
            }

            return output;
        }

        public void UpdateDay(Day day)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ID", day.ID);
                p.Add("@Name", day.Name);
                p.Add("@AvailableWorkTime", day.AvailableWorkTime);
                p.Add("@AvailableFreeTime", day.AvailableFreeTime);

                conn.Execute("dbo.spDays_Update", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void InsertProject(Project project)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@Name", project.Name);
                p.Add("@ProjectType", project.ProjectType);
                p.Add("@TechStack", "TestingStack");
                p.Add("@StartDate", project.StartDate);
                p.Add("@EstimatedEndDate", project.EstimatedEndDate);
                p.Add("@WorkSpace", project.WorkSpace);
                p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("[dbo].[spProjects_Insert]", p, commandType: CommandType.StoredProcedure);

                project.ID = p.Get<int>("@ID");
            }
        }

        public void DeleteProject(Project project)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ID", project.ID);

                conn.Execute("dbo.spProjects_Delete", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Project> GetProjects()
        {
            List<Project> output;

            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                output = conn.Query<Project>("dbo.spProjects_GetAll").ToList();
            }

            return output;
        }

        public Project GetProject(int id)
        {
            Project output = new Project();

            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ID", id);

                output = conn.QuerySingle<Project>("dbo.spProjects_Get", p, commandType: CommandType.StoredProcedure);
            }

            return output;
        }
        public void UpdateProject(Project project)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ID", project.ID);
                p.Add("@Name", project.Name);
                p.Add("@ProjectType", project.ProjectType);
                p.Add("@TechStack", project.TechStack);
                p.Add("@StartDate", project.StartDate);
                p.Add("@EstimatedEndDate", project.EstimatedEndDate);
                p.Add("@ActualEndDate", project.ActualEndDate);
                p.Add("@WorkSpace", project.WorkSpace);
                p.Add("@IsEnded", project.IsEnded);

                conn.Execute("dbo.Projects_Update", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Models.Task> GetAllSubSubTasks(Models.Task parentTask)
        {
            List<Models.Task> output;

            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ParentTaskID", parentTask.ID);

                output = conn.Query<Models.Task>("dbo.spSubSubTasks_GetAll", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public List<Models.Task> GetAllSubTasks(Models.Task parentTask)
        {
            List<Models.Task> output;

            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ParentTaskID", parentTask.ID);

                output = conn.Query<Models.Task>("dbo.spSubTasks_GetAll", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public List<Models.Task> GetAllTasks(Project project)
        {
            List<Models.Task> output;

            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ProjectID", project.ID);

                output = conn.Query<Models.Task>("dbo.spTasks_GetAll", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public void InsertTask(Models.Task task)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@Name", task.Name);
                p.Add("@Priority", task.Priority);
                p.Add("@EstimatedStartDate", task.EstimatedStartDate);
                p.Add("@ActualStartDate", task.ActualStartDate);
                p.Add("@EstimatedEndDate", task.EstimatedEndDate);
                p.Add("@ActualEndDate", task.ActualEndDate);
                p.Add("@EstimatedHoursInMinutes", task.EstimatedHoursInMinutes);
                p.Add("@ActualHoursInMinutes", task.ActualHoursInMinutes);
                p.Add("@EstimatedDays", task.EstimatedDays);
                p.Add("@ActualDays", task.ActualDays);
                p.Add("@ProjectID", task.ProjectID);
                p.Add("ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("dbo.spTasks_Insert", p, commandType: CommandType.StoredProcedure);

                // p.get<Type>("ColumnName");
                task.ID = p.Get<int>("ID");
            }
        }

        public void UpdateTaskAllLevels(Models.Task task)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@ID", task.ID);
                p.Add("@Name", task.Name);
                p.Add("@Priority", task.Priority);
                p.Add("@EstimatedStartDate", task.EstimatedStartDate);
                p.Add("@ActualStartDate", task.ActualStartDate);
                p.Add("@EstimatedEndDate", task.EstimatedEndDate);
                p.Add("@ActualEndDate", task.ActualEndDate);
                p.Add("@EstimatedHoursInMinutes", task.EstimatedHoursInMinutes);
                p.Add("@ActualHoursInMinutes", task.ActualHoursInMinutes);
                p.Add("@EstimatedDays", task.EstimatedDays);
                p.Add("@ActualDays", task.ActualDays);

                if (task.TaskLevel == 1)
                {
                    conn.Execute("dbo.spTasks_Update", p, commandType: CommandType.StoredProcedure);
                }
                if (task.TaskLevel == 2)
                {
                    conn.Execute("dbo.spSubTasks_Update", p, commandType: CommandType.StoredProcedure);
                }
                if (task.TaskLevel == 3)
                {
                    conn.Execute("dbo.spSubSubTasks_Update", p, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void DeleteTaskAllLevels(Models.Task task)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("ID", task.ID);

                if (task.TaskLevel == 1)
                {
                    conn.Execute("dbo.spTasks_Delete", p, commandType: CommandType.StoredProcedure);
                }
                if (task.TaskLevel == 2)
                {
                    conn.Execute("dbo.spSubTasks_Delete", p, commandType: CommandType.StoredProcedure);
                }
                if (task.TaskLevel == 3)
                {
                    conn.Execute("dbo.spSubSubTasks_Delete", p, commandType: CommandType.StoredProcedure);
                }
            }
        }


        public void InsertSubTask(Models.Task task)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@Name", task.Name);
                p.Add("@Priority", task.Priority);
                p.Add("@EstimatedStartDate", task.EstimatedStartDate);
                p.Add("@ActualStartDate", task.ActualStartDate);
                p.Add("@EstimatedEndDate", task.EstimatedEndDate);
                p.Add("@ActualEndDate", task.ActualEndDate);
                p.Add("@EstimatedHoursInMinutes", task.EstimatedHoursInMinutes);
                p.Add("@ActualHoursInMinutes", task.ActualHoursInMinutes);
                p.Add("@EstimatedDays", task.EstimatedDays);
                p.Add("@ActualDays", task.ActualDays);
                p.Add("@ParentTaskID", task.ParentTaskID);
                p.Add("ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("dbo.spSubTasks_Insert", p, commandType: CommandType.StoredProcedure);

                // p.get<Type>("ColumnName");
                task.ID = p.Get<int>("ID");
            }
        }

        public void InsertSubSubTask(Models.Task task)
        {
            using (IDbConnection conn = new SqlConnection(GlobalConfig.GetConnectionStringFromAppConfigByName(SqlConnectionNameInAppConfig)))
            {
                var p = new DynamicParameters();
                p.Add("@Name", task.Name);
                p.Add("@Priority", task.Priority);
                p.Add("@EstimatedStartDate", task.EstimatedStartDate);
                p.Add("@ActualStartDate", task.ActualStartDate);
                p.Add("@EstimatedEndDate", task.EstimatedEndDate);
                p.Add("@ActualEndDate", task.ActualEndDate);
                p.Add("@EstimatedHoursInMinutes", task.EstimatedHoursInMinutes);
                p.Add("@ActualHoursInMinutes", task.ActualHoursInMinutes);
                p.Add("@EstimatedDays", task.EstimatedDays);
                p.Add("@ActualDays", task.ActualDays);
                p.Add("@ParentTaskID", task.ParentTaskID);
                p.Add("ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("dbo.spSubSubTasks_Insert", p, commandType: CommandType.StoredProcedure);

                // p.get<Type>("ColumnName");
                task.ID = p.Get<int>("ID");
            }
        }

    }
}
