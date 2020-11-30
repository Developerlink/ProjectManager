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
    /// Interaction logic for ProjectNavigationWindow.xaml
    /// </summary>
    public partial class ProjectNavigationWindow : Window, IDataUpdater
    {
        List<Project> Projects { get; set; }
        public ProjectNavigationWindow()
        {
            InitializeComponent();

            // Initialize the database connection.
            GlobalConfig.InitializeConnections(DatabaseType.Sql);

            LoadProjectsFromDB();
            WireUpLists();

            //var window = new ProjectManagerWindow(24);
            //window.ShowDialog();
            //Close();
        }

        private void LoadProjectsFromDB()
        {
            Projects = GlobalConfig.Connection.GetProjects();
        }

        private void WireUpLists()
        {
            projectListView.ItemsSource = null;
            projectListView.ItemsSource = Projects;
        }

        private void creditsButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreditsWindow();
            window.Show();
        }

        private void modifyDayButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new DayManagementWindow();
            window.Show();
        }

        private void createProjectButton_Click(object sender, RoutedEventArgs e)
        {
            // Remember to pass in this window as an IDataUpdater interface
            var window = new ProjectCreationWindow(this);
            window.Show();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Down)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.Down)))
            {
                if (projectListView.Items.Count > 0)
                {
                    projectListView.SelectedItem = projectListView.Items[0];
                    projectListView.Focus();
                }
            }
        }

        private void projectListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (projectListView.SelectedItem != null)
            {
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    var project = projectListView.SelectedItem as Project;
                    var window = new ProjectManagerWindow(project.ID);
                    window.Show();
                }
                else if (Keyboard.IsKeyDown(Key.Delete))
                {
                    int currentIndex = projectListView.SelectedIndex;

                    if (projectListView.Items.Count > 0)
                    {
                        var project = projectListView.SelectedItem as Project;
                        GlobalConfig.Connection.DeleteProject(project);
                        Projects.Remove(project);
                        //LoadProjectsFromDB();
                        WireUpLists();
                        SetListviewSelectedItemVia(currentIndex);
                    }
                }
            }
        }

        void SetListviewSelectedItemVia(int index)
        {
            // If index of deleted item is the last go back 1 item. 
            // It's not count - 1 beacuse the list has been updated after deletion.
            // The index of the last item is the number of current items after it was deleted.
            if (index == projectListView.Items.Count)
            {
                projectListView.SelectedIndex = index - 1;
            }
            else
            {
                projectListView.SelectedIndex = index;
            }
        }

        public void UpdateProjectList(Project project)
        {
            // For this to work first create the IDataUpdater interface
            // Add the AddProject method to the interface 
            // Implement that interface to this window
            // Create a constructor for the other window that takes an IDataUpdater interface called 'caller'
            // Pass this window as an IDataUpdater interface to the other window (benefit of loose coupling instead of binding to this exact window)
            // The other window now has access to this method and all it has to do is pass in a project 
            // (don't make logic for this window in the other window!)           
            // The project will then be used to do the following:
            Projects.Add(project);
            WireUpLists();
        }
    }
}
