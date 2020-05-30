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
        ListViewItem listOfProducts;
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        

        public AdministratorForm()
        {

            InitializeComponent();

            RefreshData();
            Departments();


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

        public void Departments()
        {
            cmbDepartmentStack.Items.Clear();
            foreach (string d in mediaBazaar.GetDepartments())
            {
                cmbDepartmentStack.Items.Add(d);
            }
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

            mediaBazaar.ReadStocks();
            mediaBazaar.ReadProducts();
            lvStock.Items.Clear();
            foreach (Stock p in mediaBazaar.GetStockList())
            {
                ListViewItem l = new ListViewItem(p.ProductId.ToString());
                l.SubItems.Add(mediaBazaar.GetProductNameById(p.ProductId));
                l.SubItems.Add(p.Quantity.ToString());

                lvStock.Items.Add(l);
            }

            listViewProducts.Items.Clear();
            foreach (Product p in mediaBazaar.GetProducts())
            {
                listOfProducts = new ListViewItem(p.ProductId.ToString());
                listOfProducts.SubItems.Add(Convert.ToString(p.DepartmentName));
                listOfProducts.SubItems.Add(p.Name);
                listOfProducts.SubItems.Add(Convert.ToString(p.Price));
              
                listViewProducts.Items.Add(listOfProducts);
            }
            mediaBazaar.ReadRequests();
            lvRequests.Items.Clear();
            foreach (Request item in mediaBazaar.GetRequestsList())
            {
                list = new ListViewItem(Convert.ToString(item.Id));
                list.SubItems.Add(mediaBazaar.GetProductNameById(item.ProductId));
                list.SubItems.Add(item.Quantity.ToString());
                list.SubItems.Add(item.Status);
                list.SubItems.Add(item.RequestedBy);
                list.SubItems.Add(item.DatE.Substring(0, 11));
                lvRequests.Items.Add(list);
            }
            lvDepartments.Items.Clear();
            mediaBazaar.ReadDepartment();
            foreach (Department item in mediaBazaar.GetDepartmentsList())
            {
                list = new ListViewItem(Convert.ToString(item.Id));
                list.SubItems.Add(item.Name);
                list.SubItems.Add(mediaBazaar.GetPersonNameById(item.PersonId));
                list.SubItems.Add(item.MinEmp.ToString());
                lvDepartments.Items.Add(list);
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

        /* DISPLAY STATISTICS */
        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            string type = cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem);

            // Clear graph
            chartEmployeeStatistics.Series.Clear();
            chartEmployeeStatistics.Titles.Clear();

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            // Hourly wage per employee
            if (type == "Hourly wage per employee")
            {
                GenerateStatisticHourlyWagePerEmployee(type);
            }

            // salary per employee between two dates
            else if (type == "Salary per employee")
            {
                // Calculate difference between two dates (number of days)
                TimeSpan nrDays = dtpTo.Value - dtpFrom.Value;
                if (nrDays.Days < 0)
                {
                    MessageBox.Show("Dates are not valid");
                }
                else
                {
                    GenerateStatisticsSalaryPerEmployee(type);
                }
            }

            // Number employees per shift between two dates
            else if (type == "Number of employees per shift")
            {
                //type = "Number of employees per shift";

                // Calculate difference between two dates (number of days)
                TimeSpan nrDays = dtpTo.Value - dtpFrom.Value;
                if (nrDays.Days > 15)
                {
                    MessageBox.Show("You can view a maximum of 15 days");
                }
                else if (nrDays.Days < 0)
                {
                    MessageBox.Show("Dates are not valid");
                }
                else
                {
                    GenerateStatisticsNrEmployeesPerShift(type);
                }
            }
            else if (type == "Most Restocked Items")
            {
                // Calculate difference between two dates (number of days)
                TimeSpan nrDays = dtpTo.Value - dtpFrom.Value;
                if (nrDays.Days < 0)
                {
                    MessageBox.Show("Dates are not valid");
                }
                else
                {
                    GenerateStatisticsMostRestockedItems(type);

                }
            }
            else if (type == "Restocked Items On Date")
            {
                GenerateStatisticsRestockedItemsOnDate(type);
            }
            // Profit per year (stock requests)
            else if (type == "Yearly stock requests")
            {
                GenerateStatisticsYearlyStockRequests(type);
            }
        }

        /* GENERATE STATISTICS */
        private void GenerateStatisticHourlyWagePerEmployee(string type)
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

        private void GenerateStatisticsSalaryPerEmployee(string type)
        {
            string dateFrom;
            string dateTo;
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

        private void GenerateStatisticsNrEmployeesPerShift(string type)
        {
            string dateFrom;
            string dateTo;
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


            int indexMorning = 0;
            int indexAfternoon = 0;
            int indexEvening = 0;
            foreach (object[] statistic in statistics)
            {
                if (statistic[2].ToString() == "Morning")
                {
                    chartEmployeeStatistics.Series["Morning"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Morning").ToString();
                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Morning"].Points[indexMorning].ToolTip = $"{employees}";

                    indexMorning++;
                }
                else if (statistic[2].ToString() == "Afternoon")
                {
                    chartEmployeeStatistics.Series["Afternoon"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Afternoon").ToString();

                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Afternoon"].Points[indexAfternoon].ToolTip = $"{employees}";

                    indexAfternoon++;
                }
                else if (statistic[2].ToString() == "Evening")
                {
                    chartEmployeeStatistics.Series["Evening"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));


                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Evening").ToString();

                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Evening"].Points[indexEvening].ToolTip = $"{employees}";
                    indexEvening++;
                }

                // Displays one employee at a time
                Refresh();
            }
        }

        private void GenerateStatisticsRestockedItemsOnDate(string type)
        {
            string dateFrom;

            // Select dates
            dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");

            // Series
            chartEmployeeStatistics.Series.Add("Restocked Items");

            // Title
            chartEmployeeStatistics.Titles.Add($"Restocked items on {dateFrom}");


            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, type);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Restocked Items"].Points.AddXY(statistic[0].ToString(), statistic[1]);
                // Displays one employee at a time
                Refresh();
            }
        }

        private void GenerateStatisticsMostRestockedItems(string type)
        {
            string dateFrom;
            string dateTo;

            // Select dates
            dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
            dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

            // Series
            chartEmployeeStatistics.Series.Add("FamousItems");


            chartEmployeeStatistics.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            // Title
            chartEmployeeStatistics.Titles.Add($"Most restocked items between {dateFrom} and {dateTo}");


            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["FamousItems"].Points.AddXY(statistic[1].ToString(), statistic[2]);
                chartEmployeeStatistics.Series["FamousItems"].Label = "#PERCENT{P1}";
                chartEmployeeStatistics.Series["FamousItems"].LegendText = "#AXISLABEL";

                // Displays one employee at a time
                Refresh();
            }
        }


        private void GenerateStatisticsYearlyStockRequests(string type)
        {
            chartEmployeeStatistics.Series.Add("Total stock requests");

            chartEmployeeStatistics.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            // Make line thicker
            chartEmployeeStatistics.Series[0].BorderWidth = 3;

            // Title
            chartEmployeeStatistics.Titles.Add($"Total restock requests per year");

            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(type);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Total stock requests"].Points.AddXY(statistic[0].ToString(), statistic[1]);

                // Displays one employee at a time
                Refresh();
            }
        }

        /* CHOSEN STATISTICS */
        private void cbxCategoryStatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Restocked Items On Date
            if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Restocked Items On Date")
            {
                // Enable from date picking and disable to date picking
                dtpFrom.Enabled = true;
                dtpTo.Enabled = false;
            }
            // Hourly wage per employee OR Yearly profit
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Hourly wage per employee" || 
                cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Yearly stock requests")
            {
                // Disable date picking
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
            // Salary per employee between two dates OR Number employees per shift between two dates OR Most Restocked Items
            else
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
            Database_handler connection = new Database_handler();
            try
            {
                string sql = "SELECT MAX(id) FROM schedule;";
                Object result = connection.ExecuteScalar(sql);
                if (result != null) { count = Convert.ToInt32(result) + 1; }
                //MessageBox.Show(count.ToString());
                if (cbEmpShift.SelectedItem != null)
                {
                    if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
                    {
                        employeedId = Convert.ToInt32((cbEmpShift.SelectedItem as ComboboxItem).Value.ToString());
                        date = dtpTimeForShift.Value.Date;
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
                if ((connection.checknrshift(shifttype, date.ToString("yyyy-MM-dd")) < 5) && (connection.checknrperson(employeedId, date.ToString("yyyy-MM-dd")) < 1))
                {
                    //MessageBox.Show($"{date.ToString("yyyy-MM-dd")}");
                    string sql = "INSERT INTO schedule (id,employeeId,shiftType,date,statusOfShift) VALUES ('" + count + "','" + employeedId + "','" + shifttype + "','" + date.ToString("yyyy-MM-dd") + "','Assigned');";

                    if (connection.ExecuteNonQuery(sql) >= 0) MessageBox.Show("Shift has been assigned!");
                    else MessageBox.Show("Error Writing to database! Please contact Administrator!");
                }
                else MessageBox.Show("Shift is not possible due to a shift rule(s)!");
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

        private void btnShowSchedule_Click(object sender, EventArgs e)
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
            int count = 0;
            if (cbAllSchedule.Checked)
            {
                List<Schedule> schedules = mediaBazaar.GetSchedulesList();


                for (int i = d; i < DateTime.DaysInMonth(date.Year, date.Month) + d; i++)
                {
                    Label l = new Label();
                    l.Name = $"lblDay{dayN}";
                    l.AutoSize = false;
                    l.TextAlign = ContentAlignment.MiddleRight;
                    l.Size = new Size(130, 30);
                    l.Text = dayN.ToString();
                    schedulesPanels[i].Controls.Add(l);
                    count = 0;
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
                            count += 1;
                        }
                    }
                    if (count >= 15)
                    {
                        schedulesPanels[i].BackColor = Color.Red;
                    }
                    else if (count > 0)
                    {
                        schedulesPanels[i].BackColor = Color.LightGreen;
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
                            count = 0;
                            foreach (Schedule s in schedules)
                            {
                                if (s.DATETime.Day == dayN && s.DATETime.Month == date.Month)
                                {
                                    Label lblSchedule = new Label();
                                    lblSchedule.Name = $"lblWorker{dayN}";
                                    lblSchedule.Location = new Point(5, 35);
                                    lblSchedule.Text = mediaBazaar.GetPersonNameById(s.EmployeeId);
                                    schedulesPanels[i].Controls.Add(lblSchedule);
                                    count++;
                                }
                            }
                            if (count >= 5)
                            {
                                schedulesPanels[i].BackColor = Color.Red;
                            }
                            else if (count > 0)
                            {
                                schedulesPanels[i].BackColor = Color.LightGreen;
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

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = tbProductName.Text;
                double productPrice = Convert.ToDouble(tbProductPrice.Text);
                int departmentId = cmbDepartmentStack.SelectedIndex + 1;
                mediaBazaar.AddProduct(departmentId, productName, productPrice);
                RefreshData();
                tbProductName.Text = "";
                tbProductPrice.Text = "";
                cmbDepartmentStack.Text = "";
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Price Cannot be Null");
            }
            catch (Exception ex)
            {
                MessageBox.Show("None of the above field should be empty");
            }
        }

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listViewProducts.SelectedItems[0].SubItems[0].Text);
                ModifyProduct m = new ModifyProduct(id, this, mediaBazaar); // sending the id to modify data form through the parameters
                m.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("No employee is selected");
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listViewProducts.SelectedItems[0].SubItems[0].Text);
                if (MessageBox.Show("Do you want to remove this product?", "Remove Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mediaBazaar.ProductToRemove(Convert.ToInt32(id));
                    MessageBox.Show("Product is removed");
                    RefreshData();
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Product is not removed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Product is selected");
            }
        }

       

        private void tbProductToSearch_TextChanged(object sender, EventArgs e)
        {
            List<ListViewItem> items = new List<ListViewItem>();
            string productName = tbProductToSearch.Text;
            RefreshData();
            for (int i = 0; i < listViewProducts.Items.Count; i++)
            {
                if (listViewProducts.Items[i].SubItems[2].Text.Contains(productName.ToLower()) || listViewProducts.Items[i].SubItems[2].Text.Contains(productName.ToUpper()))
                {
                    items.Add(listViewProducts.Items[i]);
                }
            }
            listViewProducts.Items.Clear();
            foreach (ListViewItem lvi in items)
            {
                listViewProducts.Items.Add(lvi);
            }

            if (tbProductToSearch.Text == "")
            {
                RefreshData();
            }
        }

        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            if (lvRequests.SelectedItems.Count > 0)
            {
                if (lvRequests.SelectedItems[0].SubItems[3].Text != "Approved") {
                    mediaBazaar.ApproveRequest(Convert.ToInt32(lvRequests.SelectedItems[0].SubItems[0].Text), mediaBazaar.GetProductIntByName(lvRequests.SelectedItems[0].SubItems[1].Text), Convert.ToInt32(lvRequests.SelectedItems[0].SubItems[2].Text));
                    RefreshData();
                } else 
                {
                    MessageBox.Show("This request is already approved");
                }
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (lvStock.SelectedItems.Count > 0)
            {
                bool x = false;
                mediaBazaar.ReadProducts();
                foreach(Product p in mediaBazaar.GetProductsList())
                {
                    if(p.ProductId == Convert.ToInt32(lvStock.SelectedItems[0].SubItems[0].Text))
                    {
                        x = true;
                    }
                }
                if (!x)
                {
                    MessageBox.Show("Item does not exist");
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(tbQuantity.Text))
                    {
                        mediaBazaar.SendAdminRequest(Convert.ToInt32(lvStock.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(tbQuantity.Text));
                        RefreshData();
                        tbQuantity.Clear();
                    } else
                    {
                        MessageBox.Show("Incorrect quantity");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select the Id");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count > 0)
            {
                if(!String.IsNullOrWhiteSpace(tbNewQuantity.Text) && Convert.ToInt32(tbNewQuantity.Text) > 0)
                    {
                    mediaBazaar.AddToStock(Convert.ToInt32(listViewProducts.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(tbNewQuantity.Text));
                    RefreshData();
                    tbNewQuantity.Clear();
                } else
                {
                    MessageBox.Show("Incorrect quantity");
                }
            } else
            {
                MessageBox.Show("Select the Id");
            }
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                foreach (Department d in mediaBazaar.GetDepartmentsList())
                {
                    id = d.Id;
                }

                mediaBazaar.AddDepartment(tbNewCategoryName.Text, mediaBazaar.GetPersonIdByName(cbManagers.SelectedItem.ToString()), Convert.ToInt32(tbMinEmp.Text), id);
                RefreshData();
                tbNewCategoryName.Text = "";
                tbMinEmp.Text = "";
                cbManagers.SelectedIndex = -1;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("MinEmp Cannot be Null");
            }
            catch (Exception ex)
            {
                MessageBox.Show("None of the above field should be empty");
            }
        }

        private void btnModifyDep_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(lvDepartments.SelectedItems[0].SubItems[0].Text);
                ModifyDepartment m = new ModifyDepartment(id, this, mediaBazaar);
                m.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("No employee is selected");
            }
        }

        private void cbManagers_Click(object sender, EventArgs e)
        {
            cbManagers.Items.Clear();
            foreach (Person p in mediaBazaar.GetManagersList())
            {
                if (p.DepartmentId <= 1)
                {
                    cbManagers.Items.Add(p.GetFullName());
                }
            }
        }
    }
}
