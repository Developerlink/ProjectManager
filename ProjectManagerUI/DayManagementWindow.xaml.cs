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
    /// Interaction logic for DayManagementWindow.xaml
    /// </summary>
    public partial class DayManagementWindow : Window
    {
        public List<Day> DayList { get; set; } = GlobalConfig.Connection.GetDays();

        public DayManagementWindow()
        {
            InitializeComponent();

            WireUpLists();
        }

        void WireUpLists()
        {
            daysListView.ItemsSource = null;
            daysListView.ItemsSource = DayList;
        }

        // This method makes sure that only numbers can be entered in the textbox. 
        private void wthTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num = 0;

            if (wthTextbox == null)
            {
                return;
            }

            // If parsing to int is false, then change the text in textbox to the current value in the private field.
            if (!int.TryParse(wthTextbox.Text, out num))
            {
                wthTextbox.Text = num.ToString();
            }
        }

        private void fthTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num = 0;

            if (fthTextbox == null)
            {
                return;
            }

            // If parsing to int is false, then change the text in textbox to the current value in the private field.
            if (!int.TryParse(fthTextbox.Text, out num))
            {
                fthTextbox.Text = num.ToString();
            }
        }


        private void wthUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if textbox is empty
            if (wthTextbox == null)
            {
                return;
            }

            // Check if text can be parsed into an int and store in num if it can. 
            int num = 0;
            if (int.TryParse(wthTextbox.Text, out num))
            {
                if (0 <= num && num < 24)
                {
                    // Add 1 to num and change text to new value.
                    num++;
                    wthTextbox.Text = num.ToString();
                }
                else if (24 <= num)
                {
                    num = 24;
                    wthTextbox.Text = num.ToString();
                }
                else if (num < 0)
                {
                    num = 0;
                    wthTextbox.Text = num.ToString();
                }
            }
        }

        private void wthDownButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if textbox is empty
            if (wthTextbox == null)
            {
                return;
            }

            // Check if text can be parsed into an int and store in num if it can. 
            int num = 0;
            if (int.TryParse(wthTextbox.Text, out num))
            {
                if (1 <= num && num <= 24)
                {
                    // Subtract 1 to num and change text to new value.
                    num--;
                    wthTextbox.Text = num.ToString();
                }
                else if (24 <= num)
                {
                    num = 24;
                    wthTextbox.Text = num.ToString();
                }
                else if (num < 0)
                {
                    num = 0;
                    wthTextbox.Text = num.ToString();
                }
            }
        }

        private void fthUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if textbox is empty
            if (fthTextbox == null)
            {
                return;
            }

            // Check if text can be parsed into an int and store in num if it can. 
            int num = 0;
            if (int.TryParse(fthTextbox.Text, out num))
            {
                if (0 <= num && num < 24)
                {
                    // Add 1 to num and change text to new value.
                    num++;
                    fthTextbox.Text = num.ToString();
                }
                else if (24 <= num)
                {
                    num = 24;
                    fthTextbox.Text = num.ToString();
                }
                else if (num < 0)
                {
                    num = 0;
                    fthTextbox.Text = num.ToString();
                }
            }
        }

        private void fthDownButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if textbox is empty
            if (fthTextbox == null)
            {
                return;
            }

            // Check if text can be parsed into an int and store in num if it can. 
            int num = 0;
            if (int.TryParse(fthTextbox.Text, out num))
            {
                if (1 <= num && num <= 24)
                {
                    // Subtract 1 to num and change text to new value.
                    num--;
                    fthTextbox.Text = num.ToString();
                }
                else if (24 <= num)
                {
                    num = 24;
                    fthTextbox.Text = num.ToString();
                }
                else if (num < 0)
                {
                    num = 0;
                    fthTextbox.Text = num.ToString();
                }
            }
        }
    }
}
