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
    public partial class AdministratorForm : Form
    {

        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        ListViewItem list;
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public int checknrshift(string shifttype, string date)
        {
            int nr = 0;
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = "SELECT * FROM schedule WHERE (shiftType='" + shifttype + "' AND date='" + date + "');";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        nr++;
                    }
                    rdr.Close();
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
            return nr;
        }

        public int checknrperson(int employeeid, string date)
        {
            int nr = 0;
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {

                    string sql = "SELECT * FROM schedule WHERE (employeeId='" + employeeid + "' AND date='" + date + "');";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        nr++;
                    }
                    rdr.Close();
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
            return nr;
        }

        public AdministratorForm()
        {

            InitializeComponent();

            RefreshData();


            // Add user name
            lblUsername.Text = mediaBazaar.CurrentUser;

            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {

                    string sql = "SELECT * FROM person WHERE (role='Employee' OR role='DepotWorker');";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = rdr.GetString("firstName") + " " + rdr.GetString("lastName") + " - " + rdr.GetString("role");
                        item.Value = rdr.GetString("id");
                        cbEmpShift.Items.Add(item);
                    }
                    rdr.Close();
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        public void RefreshData()
        {
            listView1.Items.Clear();
            foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
            {
                list = new ListViewItem(Convert.ToString(item.Id));
                list.SubItems.Add(item.FirstName);
                list.SubItems.Add(item.LastName);
                list.SubItems.Add(item.GetEmail);
                list.SubItems.Add(Convert.ToString(item.DateOfBirth));
                list.SubItems.Add(item.StreetName);
                list.SubItems.Add(Convert.ToString(item.HouseNr));
                list.SubItems.Add(item.Zipcode);
                list.SubItems.Add(item.City);
                list.SubItems.Add(Convert.ToString(item.HourlyWage));
                list.SubItems.Add(Convert.ToString(item.Role));
                listView1.Items.Add(list);
            }
        }

        // To remove an employee from the system
        private void btnRemoveEmp_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                if (MessageBox.Show("Do you want to remove this person?", "Remove Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mediaBazaar.RemovePerson(Convert.ToInt32(id));
                    MessageBox.Show("Person is removed");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Person is not removed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No employee is selected");
            }
        }
        private void btnModifyStack_Click(object sender, EventArgs e)
        {

        }

        // to modify a selected person's data
        private void btnModifyEmp_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                Modify_data m = new Modify_data(id, this, mediaBazaar); // sending the id to modify data form through the parameters
                m.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("No employee is selected");
            }

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
            string dateFrom;
            string dateTo;
            string type = cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem);

            // Clear graph
            chartEmployeeStatistics.Series.Clear();
            chartEmployeeStatistics.Titles.Clear();

            // Hourly wage per employee
            if (type == "Hourly wage per employee")
            {
                // Title
                chartEmployeeStatistics.Titles.Add("Hourly wage per employee chart");
                // Series
                chartEmployeeStatistics.Series.Add("Hourly Wage");

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
            else if (type == "Salary per employee")
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
            else if (type == "Number of employees per shift")
            {
                type = "Number of employees per shift";

                // Calculate difference between two dates (number of days)
                TimeSpan nrDays = dtpTo.Value - dtpFrom.Value;
                if (nrDays.Days > 15)
                {
                    MessageBox.Show("You can view a maximum of 15 days");
                }
                else
                {
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
                        else if (statistic[2].ToString() == "Evening")
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


        private void btnAssignShift_Click_1(object sender, EventArgs e)
        {
            bool writeindb = false;
            int count = -1;
            string shifttype = "";
            int employeedId = -1;
            DateTime date = DateTime.Today;
            // MessageBox.Show((cbEmpShift.SelectedItem as ComboboxItem).Value.ToString());
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {

                    string sql = "SELECT MAX(id) FROM schedule;";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    Object result = cmd.ExecuteScalar();
                    if (result != null) { count = Convert.ToInt32(result) + 1; }
                    //MessageBox.Show(count.ToString());
                    if (cbEmpShift.SelectedItem != null)
                    {
                        if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
                        {
                            employeedId = Convert.ToInt32((cbEmpShift.SelectedItem as ComboboxItem).Value.ToString());
                            date = dtpTimeForShift.Value;
                            if (radioButton1.Checked)
                            {
                                Schedule schedule = new Schedule(employeedId, Shift.MORNING, date);
                                shifttype = "Morning";
                            }
                            if (radioButton2.Checked)
                            {
                                Schedule schedule = new Schedule(employeedId, Shift.AFTERNOON, date);
                                shifttype = "Afternoon";
                            }
                            if (radioButton3.Checked)
                            {
                                Schedule schedule = new Schedule(employeedId, Shift.EVENING, date);
                                shifttype = "Evening";
                            }
                            writeindb = true;
                        }
                        else MessageBox.Show("Please select a shift type!");
                    }
                    else MessageBox.Show("Please select an employee!");
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
            if (writeindb)
            {
                DateTime dayonly = date.Date;
                // checknrshift - check for less than 5 employees on one shift
                // checknrperson - check for one employee shifts in one day
                if ((checknrshift(shifttype, date.ToString("yyyy-MM-dd")) < 5) && (checknrperson(employeedId, date.ToString("yyyy-MM-dd")) < 1))
                {
                    connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connStr))
                        {
                            //all rules are ok
                            string sql = "INSERT INTO schedule (id,employeeId,shiftType,date,statusOfShift) VALUES (@id,@emploeeid,@shifttype,@date,@statusofshift);";
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = sql;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@id", count);
                            cmd.Parameters.AddWithValue("@emploeeid", employeedId);
                            cmd.Parameters.AddWithValue("@shifttype", shifttype);
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@statusofshift", "Assigned");
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else MessageBox.Show("Shift not possible!");
            }
        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            Modify_data Add = new Modify_data(mediaBazaar, this);
            Add.Show();
        }

        private void metroTabPage4_Click(object sender, EventArgs e)
        {

        }

    }
}
