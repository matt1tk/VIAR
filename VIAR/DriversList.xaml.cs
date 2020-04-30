using System;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Management;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace VIAR
{
    public partial class DriverWindow
    {

        List<string> driverList = new List<string>();

        public DriverWindow()
        {
            InitializeComponent();
            System.Windows.Forms.Application.EnableVisualStyles();
            progressBar.Visibility = Visibility.Hidden;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            driversListBox.Items.Clear();

            lblStatus.Content = "Status: Obtaining driver list...";

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += obtainList;
            bw.RunWorkerCompleted += workFinished;
            bw.RunWorkerAsync();
            progressBar.Visibility = Visibility.Visible;
        }

        private void obtainList(object sender, DoWorkEventArgs e)
        {
            ManagementObjectSearcher helper = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");

            foreach (ManagementObject obj in helper.Get())
            {
                try
                {
                    if (helper.Get() == null)
                        continue;

                    if (obj.GetPropertyValue("DeviceName") == null)
                        continue;
                    
                    this.driverList.Add(obj.GetPropertyValue("DeviceName").ToString());
                }
                catch (System.Reflection.TargetInvocationException)
                {
                    System.Windows.MessageBox.Show("An error has occured", "VIAR", MessageBoxButton.OK);
                    break;
                }
            }
        }

        private void workFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (string s in this.driverList)
            {
                driversListBox.Items.Add(s.ToString());
            }

            progressBar.Visibility = Visibility.Hidden;
            lblStatus.Content = "Status: Idle";
        }
    }
}
