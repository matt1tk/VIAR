using System;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Management;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VIAR
{
    public partial class MainWindow
    {
        List<string> drivers = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            UpdateStatus();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) //lets you move window
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
        private void UpdateStatus()
        {
            if (File.Exists(@"C:\Program Files\Riot Vanguard\vgk.sys"))
            {
                status.Text = "Status: Vanguard is enabled!";
                this.Background = Brushes.DarkSeaGreen;
                this.BorderBrush = Brushes.DarkOliveGreen;
            }
            else
            {
                status.Text = "Status: Vanguard is disabled!";
                this.Background = Brushes.IndianRed;
                this.BorderBrush = Brushes.DarkRed;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)//enable vanguard
        {
            try
            {
                File.Move(@"C:\Program Files\Riot Vanguard\vgk1.sys", @"C:\Program Files\Riot Vanguard\vgk.sys");
                System.Windows.MessageBox.Show("A restart is required for these changes to take full effect!");

            } catch (System.UnauthorizedAccessException)
            {
                System.Windows.MessageBox.Show("Error: Must run in administrator mode");
            }

            UpdateStatus();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//disable vanguard
        {
            try
            {
                File.Move(@"C:\Program Files\Riot Vanguard\vgk.sys", @"C:\Program Files\Riot Vanguard\vgk1.sys");
                System.Windows.MessageBox.Show("A restart is required for these changes to take full effect!");
            }
            catch (System.UnauthorizedAccessException)
            {
                System.Windows.MessageBox.Show("Error: Must run in administrator mode");
            }
            
            UpdateStatus();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//close
        {
            Environment.Exit(0);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//restart
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you'd like to restart?", "VIAR", System.Windows.MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Process.Start("shutdown.exe", "-r -t 0");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DriverWindow driverWindow = new DriverWindow();
            driverWindow.Show();
        }
    }
}
