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
    public partial class ManagerForm : Form
    {
        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

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
            string dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
            string dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

            // Clear graph
            chartEmployeeStatistics.Series.Clear();
            chartEmployeeStatistics.Titles.Clear();

            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            // Hourly wage per employee
            if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Hourly wage per employee")
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        // Title
                        chartEmployeeStatistics.Titles.Add("Hourly wage per employee chart");
                        // Series
                        chartEmployeeStatistics.Series.Add("Hourly Wage");

                        string sql = $"SELECT firstName, lastName, hourlyWage FROM person";

                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();

                        chartEmployeeStatistics.DataSource = cmd;

                        MySqlDataReader dr = cmd.ExecuteReader();

                        // Made it fit all data
                        chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                        while (dr.Read())
                        {
                            chartEmployeeStatistics.Series["Hourly Wage"].Points.AddXY(dr[0].ToString() + " " + dr[1].ToString(), dr[2]);
                            // Displays one employee at a time
                            Refresh();
                        }
                        conn.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // salary per employee between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Salary per employee")
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        // Series
                        chartEmployeeStatistics.Series.Add("Salary");

                        string sql = $"SELECT p.firstName, p.lastName, p.hourlyWage * Count(s.date) * 4 FROM schedule s INNER JOIN person p ON p.id = s.employeeId WHERE date BETWEEN @dateFrom AND @dateTo GROUP BY s.employeeId";

                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();

                        chartEmployeeStatistics.DataSource = cmd;
                        // Parameters
                        cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                        cmd.Parameters.AddWithValue("@dateTo", dateTo);

                        MySqlDataReader dr = cmd.ExecuteReader();

                        // Made it fit all data
                        chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                        // Title
                        chartEmployeeStatistics.Titles.Add($"Salary per employee chart between {dateFrom} and {dateTo}");

                        while (dr.Read())
                        {
                            chartEmployeeStatistics.Series["Salary"].Points.AddXY(dr[0].ToString() + " " + dr[1].ToString(), dr[2]);
                            // Displays one employee at a time
                            Refresh();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connStr))
                        {
                            string sql = $"SELECT COUNT(*) AS nrEmployees, date, shiftType FROM schedule WHERE date BETWEEN @dateFrom AND @dateTo GROUP BY date, shiftType ORDER BY date;";
                            // Create command object
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            // Parameters
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@dateTo", dateTo);
                            // Open db connection
                            conn.Open();
                            // Excute query via command object
                            MySqlDataReader dr = cmd.ExecuteReader();

                            // Series
                            chartEmployeeStatistics.Series.Add("Morning");
                            chartEmployeeStatistics.Series.Add("Afternoon");
                            chartEmployeeStatistics.Series.Add("Evening");

                            // Title
                            chartEmployeeStatistics.Titles.Add($"Number of employees per shift between {dateFrom} and {dateTo}");

                            // Made it fit all data
                            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                            while (dr.Read())
                            {
                                if (dr[2].ToString() == "Morning")
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
                                Console.WriteLine(dr[0].ToString() + dr[1].ToString() + dr[2].ToString());
                                // Displays one employee at a time
                                Refresh();
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
