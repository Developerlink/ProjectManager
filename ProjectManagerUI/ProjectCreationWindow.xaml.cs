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
    /// Interaction logic for ProjectCreationWindow.xaml
    /// </summary>
    public partial class ProjectCreationWindow : Window
    {
        public ProjectCreationWindow()
        {
            InitializeComponent();

            FillFormWithDummyData();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(InputIsValid())
            {
                var project = CreateProjectModel();

                GlobalConfig.Connection.InsertProject(project);

                ResetAllFields();
            }
        }

        public void FillFormWithDummyData()
        {
            nameTextBox.Text = "Heads up!";
            typeTextBox.Text = "Mobile";
            techStackTextBox.Text = "Flutter";
            //startDateDatePicker.SelectedDate = null;
            //endDateDatePicker.SelectedDate = null;
            workRadioButton.IsChecked = true;
        }

        private void ResetAllFields()
        {
            nameTextBox.Text = "";
            typeTextBox.Text = "";
            techStackTextBox.Text = "";
            startDateDatePicker.SelectedDate = null;
            endDateDatePicker.SelectedDate = null;
            workRadioButton.IsChecked = true;
        }

        private Project CreateProjectModel()
        {
            var project = new Project();

            project.Name = nameTextBox.Text;
            project.ProjectType = typeTextBox.Text;
            project.TechStack = techStackTextBox.Text;
            if(startDateDatePicker.SelectedDate != null)
            {
                project.StartDate = startDateDatePicker.SelectedDate.Value;
            }
            if(endDateDatePicker.SelectedDate != null)
            {
                project.EstimatedEndDate = endDateDatePicker.SelectedDate.Value;
            }
            if (workRadioButton.IsChecked.Value)
            {
                project.WorkSpace = "work";
            }
            else
            {
                project.WorkSpace = "home";
            }

            return project;
        }

        private bool InputIsValid()
        {
            bool output = true;
            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                output = false;
                sb.Append("The name of the project cannot be empty.\n");
            }

            if(string.IsNullOrWhiteSpace(typeTextBox.Text))
            {
                output = false;
                sb.Append("The type of the project cannot be empty.\n");
            }

            if (string.IsNullOrWhiteSpace(techStackTextBox.Text))
            {
                output = false;
                sb.Append("The tech stack of the project cannot be empty.");
            }

            if(sb.Length != 0)
            {
                MessageBox.Show(sb.ToString(), "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return output;
        }
    }
}
