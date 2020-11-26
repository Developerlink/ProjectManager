using ProjectManagerLibrary;
using ProjectManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectManagerUI
{
    /// <summary>
    /// Interaction logic for ProjectManagerWindow.xaml
    /// </summary>
    public partial class ProjectManagerWindow : Window
    {
        public Project SelectedProject { get; set; } = new Project();
        public List<ProjectManagerLibrary.Models.Task> SortedTaskList { get; set; } = new List<ProjectManagerLibrary.Models.Task>();
        public ProjectManagerWindow()
        {
            InitializeComponent();
        }

        public ProjectManagerWindow(int id)
        {
            InitializeComponent();

            GetAllData(id);
            DisplayDataIntoForm();
            WireUpLists();
        }

        private void DisplayDataIntoForm()
        {
            projectNameTextBox.Text = SelectedProject.Name;
            projectTypeTextBox.Text = SelectedProject.ProjectType;
            projectTechStackTextBox.Text = SelectedProject.TechStack;
            startDateDatePicker.SelectedDate = SelectedProject.StartDate;
            // Change label and endDateDatePicker if project has ended.
            if (SelectedProject.IsEnded == true)
            {
                endDateLabel.Content = "Actual End Date";
                endDateDatePicker.SelectedDate = SelectedProject.ActualEndDate;
            }
            else
            {
                endDateLabel.Content = "Estimated End Date:";
                endDateDatePicker.SelectedDate = SelectedProject.EstimatedEndDate;
            }
            if(SelectedProject.WorkSpace.ToLower().Equals("work"))
            {
                workRadioButton.IsChecked = true;
            }
            else
            {
                homeRadioButton.IsChecked = true;
            }
            isEndedCheckBox.IsChecked = SelectedProject.IsEnded;
        }

        private void WireUpLists()
        {
            taskListView.ItemsSource = null;
            taskListView.ItemsSource = SortedTaskList;
        }

        private void GetAllData(int id)
        {
            SortedTaskList.Clear();
            // Get project data from database.
            SelectedProject = GlobalConfig.Connection.GetProject(id);
            // Get all Tasks and put them into a temporary list.
            List<ProjectManagerLibrary.Models.Task> tempList = new List<ProjectManagerLibrary.Models.Task>(GlobalConfig.Connection.GetTasks());
            // Sort all tasks according to priority and put them into the project.
            SelectedProject.TaskList = tempList.OrderBy(x => x.Priority).ToList();

            // Get all Subtasks and put them into each Task
            // Get all SubSubTasks and put them into each SubTask
            // Use data from SubSubTasks to fill in the blanks in Subtasks
            // Use data from SubTasks to fill in the blanks in Tasks
            // Use data from Tasks to fill in the blanks in project

            // Fix default datetimes to show nothing
            DateTime defaultDatetime = DateTime.Parse("1800-01-01");
            foreach (ProjectManagerLibrary.Models.Task t in SortedTaskList)
            {
                // -1 first date is earlier, 0 dates are the same, 1 first date is later
                if(DateTime.Compare(t.EstimatedStartDate, defaultDatetime) == 0)
                {
                    
                }
            }

            PopulateSortedTaskList();
        }

        private void PopulateSortedTaskList()
        {

            // Read data from database and sort them with logic.
            foreach(ProjectManagerLibrary.Models.Task t in SelectedProject.TaskList)
            {
                SortedTaskList.Add(t);
            }
        }

        private void UpdateProjectWithInput()
        {
            SelectedProject.Name = projectNameTextBox.Text;
            SelectedProject.ProjectType = projectTypeTextBox.Text;
            SelectedProject.TechStack = projectTechStackTextBox.Text;
            SelectedProject.StartDate = startDateDatePicker.SelectedDate.Value;
            SelectedProject.IsEnded = isEndedCheckBox.IsChecked.Value;
            // Save changes to ActualEndDate if project has ended.
            if(SelectedProject.IsEnded == true)
            {
                SelectedProject.ActualEndDate = endDateDatePicker.SelectedDate.Value; 
            }
            else
            {
                SelectedProject.EstimatedEndDate = endDateDatePicker.SelectedDate.Value;
            }
            if(workRadioButton.IsChecked == true)
            {
                SelectedProject.WorkSpace = "work";
            }
            else
            {
                SelectedProject.WorkSpace = "home";
            }
        }

        private bool InputIsValid()
        {
            bool output = true;
            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(projectNameTextBox.Text))
            {
                output = false;
                sb.Append("The name of the project cannot be empty.\n");
            }

            if (string.IsNullOrWhiteSpace(projectTypeTextBox.Text))
            {
                output = false;
                sb.Append("The type of the project cannot be empty.\n");
            }

            if (string.IsNullOrWhiteSpace(projectTechStackTextBox.Text))
            {
                output = false;
                sb.Append("The tech stack of the project cannot be empty.");
            }

            if (sb.Length != 0)
            {
                MessageBox.Show(sb.ToString(), "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return output;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(InputIsValid())
            {
                UpdateProjectWithInput();

                GlobalConfig.Connection.UpdateProject(SelectedProject);
                
                MessageBox.Show("Project has been updated");

                DisplayDataIntoForm();
            }
        }

        private void newTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTask();
        }        

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.N)))
            {
                CreateNewTask();
            }
            else if(Keyboard.IsKeyDown(Key.Delete))
            {
                DeleteTask();
            }
        }

        private void DeleteTask()
        {
            if (taskListView.Items.Count > 0)
            {
                if (taskListView.SelectedItem != null)
                {
                    var selectedTask = taskListView.SelectedItem as ProjectManagerLibrary.Models.Task;

                    if (selectedTask.TaskLevel == 1)
                    {
                        GlobalConfig.Connection.DeleteTask(selectedTask);
                    }
                    else if (selectedTask.TaskLevel == 2)
                    {
                        GlobalConfig.Connection.DeleteSubTask(selectedTask);
                    }
                    else if (selectedTask.TaskLevel == 3)
                    {
                        GlobalConfig.Connection.DeleteSubSubTask(selectedTask);
                    }

                    GetAllData(SelectedProject.ID);
                    WireUpLists();
                }
            }
        }

        private void CreateNewTask()
        {
            if(taskListView.Items.Count > 0)
            {
                if(taskListView.SelectedItem == null)
                {
                    // If no item is selected Create a new task and put it into the bottom of the list.
                    var newTask = new ProjectManagerLibrary.Models.Task();
                    // Every task has a priority equal to its position in the list. 
                    // If this task should be at the bottom then the priority should be the latest priority 
                    // +1.
                    newTask.Priority = SortedTaskList.Where(x => x.TaskLevel == 1).Max(x => x.Priority) + 1;
                    GlobalConfig.Connection.InsertTask(newTask, SelectedProject);
                }
                else
                {
                    var selectedTask = taskListView.SelectedItem as ProjectManagerLibrary.Models.Task; 
                    if(selectedTask.TaskLevel == 1)
                    {
                        // Put that priority into the new task
                        var newTask = new ProjectManagerLibrary.Models.Task();
                        newTask.Priority = selectedTask.Priority;

                        // Add 1 to priority of selected task and all subsequent tasks.                        
                        foreach(ProjectManagerLibrary.Models.Task t in SortedTaskList)
                        {
                            if(t.TaskLevel == 1 && t.Priority >= newTask.Priority)
                            {
                                t.Priority++;
                            }
                        }

                        // Update the database with the new priority, but only the tasks coming after the new task.
                        foreach(ProjectManagerLibrary.Models.Task t in SortedTaskList)
                        {
                            if(t.TaskLevel == 1 && t.Priority > newTask.Priority)
                            {
                                GlobalConfig.Connection.UpdateTask(t);
                            }
                        }

                        // Insert the new task into the database.
                        GlobalConfig.Connection.InsertTask(newTask, SelectedProject);                        
                    }
                    else if (selectedTask.TaskLevel == 2)
                    {
                        // If the selected task is level 2 then create a new task just beneath the selected task with task level 2.
                    }
                    else if (selectedTask.TaskLevel == 3)
                    {
                        // If the selected task is level 3 then create a new task just beneath the selected task with task level 3.
                    }
                }

                // Read all data from database again and repopulate the listview.
                GetAllData(SelectedProject.ID);
                WireUpLists();
            }
            else
            {
                // Create the first task.
                var newTask = new ProjectManagerLibrary.Models.Task();
                //! No need to set task level, since it is default to the correct level as long as it is inserted
                //! into the correct tabel. However it cannot be changed afterwards and a new task needs to be created
                //! if it needs to be set on a different task level, so a subtask cannot be promoted to a task or vice versa. 
                //newTask.TaskLevel = 1;
                newTask.Priority = 1;
                GlobalConfig.Connection.InsertTask(newTask, SelectedProject);

                // Read all data from database again and repopulate the listview.
                GetAllData(SelectedProject.ID);
                WireUpLists();
            }
        }

        private void UpdateTaskPriority()
        {

        }

        //x finish making stored procedures for the the project insert and project update
        //x add workspace selection for create page
        //x add completed checkbox for the project
        //x add workspace selection for manager page

        //x make the create project functionality
        //x Problemer med datetime i sql. 
        //x Problemer med skærmen (som ikke blev fikset)
        //x read all projects in the navigation window
        //x navigate from navigation window to manager window

        //x update project list in navigation after new project is created
        //x delete project from navigation list
        //x add user friendly focus on list after deleting 
        //x Add checkbox to UI instead of button to complete project
        //x Problemer med techstack field som jeg lige skal kigge på
        //x Fix problem with radiobutton inbuilt behavior when using viewbox to scale it
        //x read project info into the manager window 
        //x made the string property projectstatus in c# model and in database to the boolean IsActive instead 
        //x save changes to project

        //x make create task button put new task into tasks in db
        //x read tasks into tasklistview
        //x refactor timespan to bigint
        //x create stored procedures for tasks
        //x make converter for default datetimes, color and fontsize
        //x create task and put into correct order in the list
        //x make the delete function for tasks

        // make create task button put new subtask into tasks in db
        // read subtasks into tasklistview
        // color tasks and subtasks differently to separate them
        // style the items in the listview to make them better looking
        // make create task button put new subsubtask into tasks in db
        // read subsubtasks into tasklistview
        // Make update save button function properly. 
        // make lsitview automatically calculate hours needed and enddate date
        // make the log problem window and functionality
    }
}
