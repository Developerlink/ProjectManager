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
        public ProjectNavigationWindow()
        {
            InitializeComponent();

            var window = new ProjectManagerWindow();
            window.Show();
            Close();
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
    }
}
