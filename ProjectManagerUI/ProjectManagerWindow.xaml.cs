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
        public ProjectManagerWindow()
        {
            InitializeComponent();
        }

        public ProjectManagerWindow(Project project)
        {
            InitializeComponent();

            SelectedProject = project;
        }

        private void ReadProjectDataIntoForm()
        {
            //Name = "projectNameTextBox"
              //  projectTypeTextBox

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

        // read project info into the manager window 
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
