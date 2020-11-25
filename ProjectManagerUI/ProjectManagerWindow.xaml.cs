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
        public Project SelectedProject { get; set; }
        public List<ProjectManagerLibrary.Models.Task> SortedTaskList { get; set; }
        public ProjectManagerWindow()
        {
            InitializeComponent();
        }

        public ProjectManagerWindow(int id)
        {
            InitializeComponent();

            GetAllData(id);
            DisplayDataIntoForm();
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
            // Get project data from database.
            SelectedProject = GlobalConfig.Connection.GetProject(id);
            // Get all Tasks and put them into the project
            // Get all Subtasks and put them into each Task
            // Get all SubSubTasks and put them into each SubTask
            // Use data from SubSubTasks to fill in the blanks in Subtasks
            // Use data from SubTasks to fill in the blanks in Tasks
            // Use data from Tasks to fill in the blanks in project
        }

        private void PopulateSortedTaskList()
        {
            // Read data from database and sort them with logic.

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

        // make create task button put new task into tasks in db
        // read tasks into tasklistview
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
