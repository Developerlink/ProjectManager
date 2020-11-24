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
    public partial class ProjectNavigationWindow : Window
    {
        List<Project> Projects { get; set; }
        public ProjectNavigationWindow()
        {
            InitializeComponent();

            // Initialize the database connection.
            GlobalConfig.InitializeConnections(DatabaseType.Sql);

            LoadProjectsFromDB();
            WireUpLists();

            //var window = new ProjectManagerWindow();


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
            var window = new ProjectCreationWindow();
            window.Show();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Down)) || (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.Down)))
            {
                //MessageBox.Show($"{Key.Tab} + {Key.Down}");
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
                    var project = projectListView.SelectedItem;
                    var window = new ProjectManagerWindow();
                    window.Show();
                }
            }
        }
    }
}
