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
            if (SelectedProject.WorkSpace.ToLower().Equals("work"))
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
            // Sort all tasks according to priority and put them into the project. 
            // The stored procedure has already sorted them by priority.
            SelectedProject.TaskList = GlobalConfig.Connection.GetAllTasks(SelectedProject);
            //SelectedProject.TaskList = tempList.OrderBy(x => x.Priority).ToList();

            // Get all Subtasks and put them into each Task
            foreach (ProjectManagerLibrary.Models.Task task in SelectedProject.TaskList)
            {
                task.SubTaskList = GlobalConfig.Connection.GetAllSubTasks(task);

                // Get all SubSubTasks and put them into each SubTask
                foreach(ProjectManagerLibrary.Models.Task subTask in task.SubTaskList)
                {
                    subTask.SubTaskList = GlobalConfig.Connection.GetAllSubSubTasks(subTask);
                }
            }
            // Use data from SubSubTasks to fill in the blanks in Subtasks
            // Use data from SubTasks to fill in the blanks in Tasks
            // Use data from Tasks to fill in the blanks in project

            // Fix default datetimes to show nothing            

            PopulateSortedTaskList();
        }

        private void PopulateSortedTaskList()
        {
            // Read data from database and sort between tasks, subtasks and subsubtasks before putting them in.
            foreach (ProjectManagerLibrary.Models.Task task in SelectedProject.TaskList)
            {
                SortedTaskList.Add(task);
                foreach(ProjectManagerLibrary.Models.Task subTask in task.SubTaskList)
                {
                    SortedTaskList.Add(subTask);
                    foreach(ProjectManagerLibrary.Models.Task subSubTask in subTask.SubTaskList)
                    {
                        SortedTaskList.Add(subSubTask);
                    }
                }
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
            if (SelectedProject.IsEnded == true)
            {
                SelectedProject.ActualEndDate = endDateDatePicker.SelectedDate.Value;
            }
            else
            {
                SelectedProject.EstimatedEndDate = endDateDatePicker.SelectedDate.Value;
            }
            if (workRadioButton.IsChecked == true)
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



        private void DeleteTask()
        {
            if (taskListView.Items.Count > 0)
            {
                if (taskListView.SelectedItem != null)
                {
                    var selectedTask = taskListView.SelectedItem as ProjectManagerLibrary.Models.Task;
                    int currentIndex = taskListView.SelectedIndex;

                    GlobalConfig.Connection.DeleteTask(selectedTask);

                    GetAllData(SelectedProject.ID);
                    WireUpLists();
                    SetListviewSelectedItemVia(currentIndex);
                }
            }
        }

        void SetListviewSelectedItemVia(int index)
        {
            // If index of deleted item is the last go back 1 item. 
            // It's not count - 1 beacuse the list has been updated after deletion.
            // The index of the last item is the number of current items after it was deleted.
            if (index == taskListView.Items.Count)
            {
                taskListView.SelectedIndex = index - 1;
            }
            else
            {
                taskListView.SelectedIndex = index;
            }
        }

        private void CreateNewTask()
        {
            if (taskListView.Items.Count > 0)
            {
                if (taskListView.SelectedItem == null)
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
                    if (selectedTask.TaskLevel == 1)
                    {
                        // Put that priority into the new task
                        var newTask = new ProjectManagerLibrary.Models.Task();
                        newTask.Priority = selectedTask.Priority;

                        // Add 1 to priority of selected task and all subsequent tasks.                        
                        foreach (ProjectManagerLibrary.Models.Task t in SortedTaskList)
                        {
                            if (t.TaskLevel == 1 && t.Priority >= newTask.Priority)
                            {
                                t.Priority++;
                            }
                        }

                        // Update the database with the new priority, but only the tasks coming after the new task.
                        foreach (ProjectManagerLibrary.Models.Task t in SortedTaskList)
                        {
                            if (t.TaskLevel == 1 && t.Priority > newTask.Priority)
                            {
                                GlobalConfig.Connection.UpdateTask(t);
                            }
                        }

                        // Insert the new task into the database.
                        GlobalConfig.Connection.InsertTask(newTask, SelectedProject);
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

        private void CreateNewSubTask()
        {
            // If there are no items then don't do anything, since all subtasks need a task that they belong to.
            if (taskListView.Items.Count > 0)
            {
                // An item needs to be selected so that we can use it as a parent task
                if (taskListView.SelectedItem != null)
                {
                    var selectedTask = taskListView.SelectedItem as ProjectManagerLibrary.Models.Task;
                    // If that item is TaskLevel == 1, then use it as the parenTaskID and create the subtask at the bottom of 
                    // its list of subtasks
                    if (selectedTask.TaskLevel == 1)
                    {
                        var newSubTask = new ProjectManagerLibrary.Models.Task();
                        newSubTask.ParentTaskID = selectedTask.ID;

                        // Check if this task has any subtasks
                        int subTaskCount = SortedTaskList.Count(x => x.ParentTaskID == selectedTask.ID);

                        // If it has no subtasks then give newSubTask first priority.
                        if (subTaskCount == 0)
                        {
                            newSubTask.Priority = 1;
                        }
                        else
                        {
                            // Every task has a priority equal to its position in the list. 
                            // If this task should be at the bottom then the priority should be the latest priority 
                            // +1.                       
                            newSubTask.Priority = SortedTaskList.Where(x => x.ParentTaskID == selectedTask.ID).Max(x => x.Priority) + 1;
                        }
                        GlobalConfig.Connection.InsertSubTask(newSubTask, selectedTask);
                    }
                    else if (selectedTask.TaskLevel == 2)
                    {
                        // If item is TaskLevel == 2 then the ParenTaskID is the same.
                        // Put that priority into the new task
                        var newSubTask = new ProjectManagerLibrary.Models.Task();
                        newSubTask.ParentTaskID = selectedTask.ParentTaskID;
                        newSubTask.Priority = selectedTask.Priority;

                        // Add 1 to priority of selected task and all subsequent tasks.                        
                        foreach (ProjectManagerLibrary.Models.Task t in SortedTaskList)
                        {
                            if (t.TaskLevel == 2 && t.Priority >= newSubTask.Priority && t.ParentTaskID == newSubTask.ParentTaskID)
                            {
                                t.Priority++;
                            }
                        }

                        // Update the database with the new priority, but only the tasks coming after the new task.
                        foreach (ProjectManagerLibrary.Models.Task t in SortedTaskList)
                        {
                            if (t.TaskLevel == 2 && t.Priority > newSubTask.Priority && t.ParentTaskID == newSubTask.ParentTaskID)
                            {
                                GlobalConfig.Connection.UpdateTask(t);
                            }
                        }

                        // Not selected task since the method requires passing in the parentask, and in this case the selected task
                        // is a 'colleague' subtask
                        // Create a temporary parentTask with the correct ID and use that as a dummy to pass the correct id into the
                        // database method.
                        var tempParentTask = new ProjectManagerLibrary.Models.Task();
                        tempParentTask.ID = newSubTask.ParentTaskID;
                        // Insert the new task into the database.
                        GlobalConfig.Connection.InsertSubTask(newSubTask, tempParentTask);
                    }

                }

                // Read all data from database again and repopulate the listview.
                GetAllData(SelectedProject.ID);
                WireUpLists();
            }
        }

        private void CreateNewSubSubTask()
        {

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputIsValid())
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

        private void newSubTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewSubTask();
        }

        private void newSubSubTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewSubSubTask();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteTask();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D1)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D1)))
            {
                CreateNewTask();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D2)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D2)))
            {
                CreateNewSubTask();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D3)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D3)))
            {
                CreateNewSubSubTask();
            }
            else if (Keyboard.IsKeyDown(Key.Delete))
            {
                DeleteTask();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Down)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.Down)))
            {
                if (taskListView.Items.Count > 0 && taskListView.SelectedItem == null)
                {
                    taskListView.SelectedItem = taskListView.Items[0];
                    taskListView.Focus();
                }
            }
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

        //x Add better focus-handling after deleting a task
        // make create task button put new subtask into tasks in db
        // added more buttons to be able to do this

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
