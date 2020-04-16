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
            
            mediaBazaar.ReadSchedule();
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

                        string sql = "SELECT firstName, lastName, hourlyWage FROM person";

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
                // Add finally
            }

            // salary per employee between two dates
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Salary per employee")
            {
                try
                {
                    // Select dates
                    dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
                    dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        // Series
                        chartEmployeeStatistics.Series.Add("Salary");

                        string sql = "SELECT p.firstName, p.lastName, p.hourlyWage * Count(s.date) * 4 FROM schedule s INNER JOIN person p ON p.id = s.employeeId WHERE date BETWEEN @dateFrom AND @dateTo GROUP BY s.employeeId";

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
                if (nrDays.Days > 15) {
                    MessageBox.Show("You can view a maximum of 15 days");
                }
                else
                {
                    try
                    {
                        // Select dates
                        dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
                        dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

                        using (MySqlConnection conn = new MySqlConnection(connStr))
                        {
                            string sql = "SELECT COUNT(*) AS nrEmployees, date, shiftType FROM schedule WHERE date BETWEEN @dateFrom AND @dateTo GROUP BY date, shiftType ORDER BY date;";
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
                        if(radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
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
                if ((checknrshift(shifttype,date.ToString("yyyy-MM-dd")) <5)&&(checknrperson(employeedId, date.ToString("yyyy-MM-dd")) <1))
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

        
        //check

        private void btnShowSchedule_Click_1(object sender, EventArgs e)
        {
            mediaBazaar.ReadSchedule();
            List<FlowLayoutPanel> schedulesPanels = new List<FlowLayoutPanel>();
            Shift shift = Shift.MORNING;

            if (comboBox1.Text == "AFTERNOON")
            {
                shift = Shift.AFTERNOON;
            }
            else if (comboBox1.Text == "EVENING")
            {
                shift = Shift.EVENING;
            }



            int x = 20;
            int y = 20;

            pnlSchedule.Controls.Clear();
            int c = 0;
            for (int j = 0; j < 6; j++)
            {
                x = 20;
                for (int i = 0; i < 7; i++)
                {
                    FlowLayoutPanel p = new FlowLayoutPanel();
                    p.Name = $"pDay{c}";
                    p.Size = new Size(135, 150);
                    p.Location = new Point(x, y);

                    p.BorderStyle = BorderStyle.FixedSingle;
                    pnlSchedule.Controls.Add(p);
                    schedulesPanels.Add(p);
                    x += 150;
                    c++;
                }
                y += 165;
            }
            DateTime date = new DateTime(DateTime.Now.Year, cbScheduleMonth.SelectedIndex + 1, 1);
            int d = 0;
            if (date.ToString("dddd") == "Monday")
            {
                d = 1;
            }
            else if (date.ToString("dddd") == "Tuesday")
            {
                d = 2;
            }
            else if (date.ToString("dddd") == "Wednesday")
            {
                d = 3;
            }
            else if (date.ToString("dddd") == "Thursday")
            {
                d = 4;
            }
            else if (date.ToString("dddd") == "Friday")
            {
                d = 5;
            }
            else if (date.ToString("dddd") == "Saturday")
            {
                d = 6;
            }
            int dayN = 1;
            if (cbAllSchedule.Checked)
            {
                List<Schedule> schedules = mediaBazaar.GetSchedule();


                for (int i = d; i < DateTime.DaysInMonth(date.Year, date.Month) + d; i++)
                {
                    Label l = new Label();
                    l.Name = $"lblDay{dayN}";
                    l.AutoSize = false;
                    l.TextAlign = ContentAlignment.MiddleRight;
                    l.Size = new Size(130, 30);
                    l.Text = dayN.ToString();
                    schedulesPanels[i].Controls.Add(l);
                    foreach (Schedule s in schedules)
                    {
                        if (s.DATETime.Day == dayN && s.DATETime.Month == date.Month)
                        {
                            Label lblSchedule = new Label();
                            lblSchedule.Name = $"lblWorker{dayN}";
                            lblSchedule.Location = new Point(5, 35);
                            lblSchedule.AutoSize = false;
                            lblSchedule.Size = new Size(170, 24);
                            String text = $"{mediaBazaar.GetPersonNameById(s.EmployeeId)}({s.ShiftType.ToString()})";
                            lblSchedule.Text = text;
                            schedulesPanels[i].Controls.Add(lblSchedule);

                        }
                    }

                    dayN++;
                }
            }
            else
            {
                if (cbScheduleMonth.SelectedIndex != -1)
                {
                    if (comboBox1.SelectedIndex != -1 && cbNameOfEmp.SelectedIndex == -1)
                    {
                        List<Schedule> schedules = mediaBazaar.GetScheduleByShift(shift);


                        for (int i = d; i < DateTime.DaysInMonth(date.Year, date.Month) + d; i++)
                        {
                            Label l = new Label();
                            l.Name = $"lblDay{dayN}";
                            l.AutoSize = false;
                            l.TextAlign = ContentAlignment.MiddleRight;
                            l.Size = new Size(130, 30);
                            l.Text = dayN.ToString();
                            schedulesPanels[i].Controls.Add(l);
                            foreach (Schedule s in schedules)
                            {
                                if (s.DATETime.Day == dayN && s.DATETime.Month == date.Month)
                                {
                                    Label lblSchedule = new Label();
                                    lblSchedule.Name = $"lblWorker{dayN}";
                                    lblSchedule.Location = new Point(5, 35);
                                    lblSchedule.Text = mediaBazaar.GetPersonNameById(s.EmployeeId);
                                    schedulesPanels[i].Controls.Add(lblSchedule);
                                }
                            }

                            dayN++;
                        }
                    }
                    else if (comboBox1.SelectedIndex == -1 && cbNameOfEmp.SelectedIndex != -1)
                    {
                        List<Schedule> schedules = mediaBazaar.GetScheduleByName(cbNameOfEmp.Text);


                        for (int i = d; i < DateTime.DaysInMonth(date.Year, date.Month) + d; i++)
                        {
                            Label l = new Label();
                            l.Name = $"lblDay{dayN}";
                            l.AutoSize = false;
                            l.TextAlign = ContentAlignment.MiddleRight;
                            l.Size = new Size(130, 30);
                            l.Text = dayN.ToString();
                            schedulesPanels[i].Controls.Add(l);
                            foreach (Schedule s in schedules)
                            {
                                if (s.DATETime.Day == dayN && s.DATETime.Month == date.Month)
                                {
                                    Label lblSchedule = new Label();
                                    lblSchedule.Name = $"lblWorker{dayN}";
                                    lblSchedule.Location = new Point(5, 35);
                                    lblSchedule.Text = mediaBazaar.GetPersonNameById(s.EmployeeId);
                                    schedulesPanels[i].Controls.Add(lblSchedule);
                                    Label lblShift = new Label();
                                    lblShift.Name = $"lblShift{dayN}";
                                    lblShift.Location = new Point(5, 70);
                                    lblShift.Text = s.ShiftType.ToString();
                                    schedulesPanels[i].Controls.Add(lblShift);

                                }
                            }

                            dayN++;
                        }
                    }
                    else if (comboBox1.SelectedIndex != -1 && cbNameOfEmp.SelectedIndex != -1)
                    {
                        List<Schedule> schedules = mediaBazaar.GetScheduleByNameAndShift(cbNameOfEmp.Text, shift);


                        for (int i = d; i < DateTime.DaysInMonth(date.Year, date.Month) + d; i++)
                        {
                            Label l = new Label();
                            l.Name = $"lblDay{dayN}";
                            l.AutoSize = false;
                            l.TextAlign = ContentAlignment.MiddleRight;
                            l.Size = new Size(130, 30);
                            l.Text = dayN.ToString();
                            schedulesPanels[i].Controls.Add(l);
                            foreach (Schedule s in schedules)
                            {
                                if (s.DATETime.Day == dayN && s.DATETime.Month == date.Month)
                                {
                                    Label lblSchedule = new Label();
                                    lblSchedule.Name = $"lblWorker{dayN}";
                                    lblSchedule.Location = new Point(5, 35);
                                    lblSchedule.Text = mediaBazaar.GetPersonNameById(s.EmployeeId);
                                    schedulesPanels[i].Controls.Add(lblSchedule);
                                }
                            }

                            dayN++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please choose the month first!");
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(Shift.AFTERNOON);
            comboBox1.Items.Add(Shift.EVENING);
            comboBox1.Items.Add(Shift.MORNING);
        }

        private void cbNameOfEmp_Click(object sender, EventArgs e)
        {
            cbNameOfEmp.Items.Clear();
            foreach (Person p in mediaBazaar.GetPeopleList())
            {
                cbNameOfEmp.Items.Add(p.GetFullName());
            }
        }
    }
}