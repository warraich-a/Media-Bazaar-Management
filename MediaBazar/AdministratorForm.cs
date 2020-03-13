using System;
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
    public partial class AdministratorForm : Form
    {
        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        public AdministratorForm()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            mediaBazaar.LogOut();
            MessageBox.Show("Logged out successfully");
            this.Hide();
            LogInForm formLogIn = new LogInForm();
            formLogIn.ShowDialog();
            this.Close();
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            string dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
            string dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

            // Hourly wage per employee
            if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Hourly wage per employee")
            {
                // Clear graph
                chartEmployeeStatistics.Series.Clear();
                chartEmployeeStatistics.Series.Add("Hourly Wage");
                chartEmployeeStatistics.Titles.Clear();

                // get hourly wage per employee
                MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");

                // Can fit at most nine
                string sql = $"SELECT firstName, lastName, hourlyWage FROM person";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                chartEmployeeStatistics.DataSource = cmd;

                MySqlDataReader dr = cmd.ExecuteReader();

                // Made it fit all data
                chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                chartEmployeeStatistics.Titles.Add("Hourly wage per employee chart");
                while (dr.Read())
                {
                    chartEmployeeStatistics.Series["Hourly Wage"].Points.AddXY(dr[0].ToString() + " " + dr[1].ToString(), dr[2]);
                    // Displays one employee at a time
                    Refresh();
                }
                conn.Close();
            }

            // salary per employee between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Salary per employee")
            {
                // Clear graph
                chartEmployeeStatistics.Series.Clear();
                chartEmployeeStatistics.Series.Add("Salary");
                chartEmployeeStatistics.Titles.Clear();


                MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");
                string sql = $"SELECT p.firstName, p.lastName, p.hourlyWage * Count(s.date) * 4 FROM schedule s INNER JOIN person p ON p.id = s.employeeId WHERE date BETWEEN '{dateFrom}' AND '{dateTo}' GROUP BY s.employeeId";
               
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                chartEmployeeStatistics.DataSource = cmd;

                MySqlDataReader dr = cmd.ExecuteReader();

                // Made it fit all data
                chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                chartEmployeeStatistics.Titles.Add($"Salary per employee chart between {dateFrom} and {dateTo}");
                while (dr.Read())
                {
                    chartEmployeeStatistics.Series["Salary"].Points.AddXY(dr[0].ToString() + " " + dr[1].ToString(), dr[2]);
                    // Displays one employee at a time
                    Refresh();
                }

                conn.Close();
            }

            // Number employees per shift
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Number of employees per shift")
            {
                // Clear graph
                chartEmployeeStatistics.Series.Clear();

                MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");

                string sql = $"SELECT COUNT(*) AS nrEmployees, date, shiftType FROM schedule WHERE date BETWEEN '{dateFrom}' AND '{dateTo}' GROUP BY date, shiftType ORDER BY date;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                MySqlDataReader dr = cmd.ExecuteReader();

                chartEmployeeStatistics.Titles.Clear();

                chartEmployeeStatistics.Series.Add("Morning");
                chartEmployeeStatistics.Series.Add("Afternoon");
                chartEmployeeStatistics.Series.Add("Evening");

                while (dr.Read())
                {
                    if(dr[2].ToString() == "Morning")
                    {
                        chartEmployeeStatistics.Series["Morning"].Points.AddXY((dr[1]), Convert.ToInt32(dr[0]));
                    }
                    else if (dr[2].ToString() == "Afternoon")
                    {
                        chartEmployeeStatistics.Series["Afternoon"].Points.AddXY((dr[1]), Convert.ToInt32(dr[0]));
                    }
                    else
                    {
                        chartEmployeeStatistics.Series["Evening"].Points.AddXY((dr[1]), Convert.ToInt32(dr[0]));
                    }

                    // Displays one employee at a time
                   Refresh();
                }
                // Close connection to db
                conn.Close();
            }
        }
    }
}
