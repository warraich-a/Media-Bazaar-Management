using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MediaBazar
{
    public partial class ManagerForm : Form
    {

        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        //ListViewItem listB;
        public ManagerForm()
        {
            InitializeComponent();

            // Add user name
            lblUsername.Text = mediaBazaar.CurrentUser;

           RefreshData();

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //AdministratorForm a = new AdministratorForm();
            //a.Show();
            mediaBazaar.LogOut();
            MessageBox.Show("Logged out successfully");
            this.Hide();
            LogInForm formLogIn = new LogInForm();
            formLogIn.ShowDialog();
            this.Close();
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            string dateFrom;
            string dateTo;
            string type;

            // Clear graph
            chartEmployeeStatistics.Series.Clear();
            chartEmployeeStatistics.Titles.Clear();

            // Hourly wage per employee
            if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Hourly wage per employee")
            {
                type = "Hourly wage per employee";

                // Title
                chartEmployeeStatistics.Titles.Add("Hourly wage per employee chart");
                // Series
                chartEmployeeStatistics.Series.Add("Hourly Wage");

                //chartEmployeeStatistics.DataSource = cmd;

                // Made it fit all data
                chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                ArrayList statistics = mediaBazaar.GetStatistics(type);

                foreach (object[] statistic in statistics)
                {
                    chartEmployeeStatistics.Series["Hourly Wage"].Points.AddXY(statistic[0].ToString() + " " + statistic[1].ToString(), statistic[2]);
                    //Displays one employee at a time
                    Refresh();
                }
            }

            // salary per employee between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Salary per employee")
            {
                type = "Salary per employee";

                // Select dates
                dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
                dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

                // Series
                chartEmployeeStatistics.Series.Add("Salary");


                // Made it fit all data
                chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                // Title
                chartEmployeeStatistics.Titles.Add($"Salary per employee chart between {dateFrom} and {dateTo}");

                ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type);

                foreach (object[] statistic in statistics)
                {
                    chartEmployeeStatistics.Series["Salary"].Points.AddXY(statistic[0].ToString() + " " + statistic[1].ToString(), statistic[2]);
                    // Displays one employee at a time
                    Refresh();
                }
            }

            // Number employees per shift between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Number of employees per shift")
            {
                // Calculate difference between two dates (number of days)
                TimeSpan nrDays = dtpTo.Value - dtpFrom.Value;
                if (nrDays.Days > 15)
                {
                    MessageBox.Show("You can view a maximum of 15 days");
                }
                else
                {
                    type = "Number of employees per shift";

                    // Select dates
                    dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
                    dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

                    // Series
                    chartEmployeeStatistics.Series.Add("Morning");
                    chartEmployeeStatistics.Series.Add("Afternoon");
                    chartEmployeeStatistics.Series.Add("Evening");

                    // Title
                    chartEmployeeStatistics.Titles.Add($"Number of employees per shift between {dateFrom} and {dateTo}");

                    // Made it fit all data
                    chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                    ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type);

                    foreach (object[] statistic in statistics)
                    {
                        if (statistic[2].ToString() == "Morning")
                        {
                            chartEmployeeStatistics.Series["Morning"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));
                        }
                        else if (statistic[2].ToString() == "Afternoon")
                        {
                            chartEmployeeStatistics.Series["Afternoon"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));
                        }
                        else
                        {
                            chartEmployeeStatistics.Series["Evening"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));
                        }
                        // Displays one employee at a time
                        Refresh();
                    }
                }
            }
        }

        private void cbxCategoryStatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hourly wage per employee
            if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Hourly wage per employee")
            {
                // disable date picking
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
            // salary per employee between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Salary per employee")
            {
                // Enable date picking
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            // Number employees per shift between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Number of employees per shift")
            {
                // Enable date picking
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
        }
        private void btnShowEmp_Click(object sender, EventArgs e)
        {
            string name = tbEmpNameToFind.Text;
            MessageBox.Show($"{mediaBazaar.foundedPerson(name).ToString()}");
        }

        public void RefreshData()
        {
            //ListViewItem listB;
            //LV2.Items.Clear();
            //foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
            //{
            //    listB = new ListViewItem(Convert.ToString(item.Id));
            //    listB.SubItems.Add(item.FirstName);
            //    listB.SubItems.Add(item.LastName);
            //    listB.SubItems.Add(item.GetEmail);
            //    listB.SubItems.Add(Convert.ToString(item.DateOfBirth));
            //    listB.SubItems.Add(item.StreetName);
            //    listB.SubItems.Add(Convert.ToString(item.HouseNr));
            //    listB.SubItems.Add(item.Zipcode);
            //    listB.SubItems.Add(item.City);
            //    listB.SubItems.Add(Convert.ToString(item.HourlyWage));
            //    listB.SubItems.Add(Convert.ToString(item.Role));
            //    LV2.Items.Add(listB);
            //}
        }
    }
}
