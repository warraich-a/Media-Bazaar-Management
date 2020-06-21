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
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MediaBazar
{
    public partial class ManagerForm : Form
    {

        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        ListViewItem listB;
        int departmentId = 0;

        string currentUserDepartment;

        private ListViewColumnSorter lvwColumnSorter;
        private ListViewColumnSorter lvProductSortByColumn;
        public ManagerForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            lvProductSortByColumn = new ListViewColumnSorter();
            this.LV2.ListViewItemSorter = lvwColumnSorter;
            this.lvStock.ListViewItemSorter = lvProductSortByColumn;


            if (DateTime.Now.Month - 1 == 0)
            {
                cbScheduleMonth.SelectedIndex = 12;
            }
            else
            {
                cbScheduleMonth.SelectedIndex = DateTime.Now.Month - 1;
            }
            // Add user name
            lblUsername.Text = mediaBazaar.CurrentUser;

            // Get user department 
            currentUserDepartment = mediaBazaar.CurrentUserDepartment;

            foreach (Person p in mediaBazaar.GetManagersList())
            {
                if (p.FirstName == mediaBazaar.CurrentUser)
                {
                    departmentId = p.DepartmentId;

                }
            }

            RefreshData();

            // Select first category statistic
            cbxCategoryStatistics.SelectedIndex = 0;

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

        /* DISPLAY STATISTICS */
        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            string type = cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem);

            // Department of manager
            string department = currentUserDepartment;

            // Clear graph
            chartEmployeeStatistics.Series.Clear();
            chartEmployeeStatistics.Titles.Clear();

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            // Hourly wage per employee
            if (type == "Hourly wage per employee")
            {
                GenerateStatisticHourlyWagePerEmployee(type, department);
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
                    GenerateStatisticsSalaryPerEmployee(type, department);
                }
            }
            // Number employees per shift between two dates
            else if (type == "Number of employees per shift")
            {
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
                    GenerateStatisticsNrEmployeesPerShift(type, department);
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
                    GenerateStatisticsMostRestockedItems(type, department);
                }
            }
            else if (type == "Restocked Items On Date")
            {
                GenerateStatisticsRestockedItemsOnDate(type, department);
            }
            // Profit per year (stock requests)
            else if (type == "Yearly stock requests")
            {
                GenerateStatisticsYearlyStockRequests(type, department);
            }
            // Profit per year
            else if (type == "Yearly profit")
            {
                GenerateStatisticsYearlyProfit(type, department);
            }
            // Top Selling Products
            else if (type == "Top Selling Products")
            {
                GenerateStatisticsTopSellingProducts(type, department);
            }
        }

        /* GENERATE STATISTICS */
        private void GenerateStatisticHourlyWagePerEmployee(string type, string department)
        {
            // Title
            chartEmployeeStatistics.Titles.Add($"Hourly wage per employee chart in department '{department}'");
            // Series
            chartEmployeeStatistics.Series.Add("Hourly Wage");

            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Hourly Wage"].Points.AddXY(statistic[0].ToString() + " " + statistic[1].ToString(), statistic[2]);
                //Displays one employee at a time
                Refresh();
            }
        }

        private void GenerateStatisticsSalaryPerEmployee(string type, string department)
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
            chartEmployeeStatistics.Titles.Add($"Salary per employee chart between {dateFrom} and {dateTo} in department '{department}'");

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Salary"].Points.AddXY(statistic[0].ToString() + " " + statistic[1].ToString(), statistic[2]);
                // Displays one employee at a time
                Refresh();
            }
        }

        private void GenerateStatisticsNrEmployeesPerShift(string type, string department)
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
            chartEmployeeStatistics.Titles.Add($"Number of employees per shift between {dateFrom} and {dateTo} in department '{department}'");

            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type, department);

            chartEmployeeStatistics.Series[0].XValueType = ChartValueType.Date;
            chartEmployeeStatistics.Series[1].XValueType = ChartValueType.Date;
            chartEmployeeStatistics.Series[2].XValueType = ChartValueType.Date;

            chartEmployeeStatistics.Series[0]["PixelPointWidth"] = "45";
            chartEmployeeStatistics.Series[1]["PixelPointWidth"] = "45";
            chartEmployeeStatistics.Series[2]["PixelPointWidth"] = "45";

            int indexMorning = 0;
            int indexAfternoon = 0;
            int indexEvening = 0;
            foreach (object[] statistic in statistics)
            {
                // If morning shift
                if (statistic[2].ToString() == "Morning")
                {
                    chartEmployeeStatistics.Series["Morning"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Morning", department).ToString();
                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Morning"].Points[indexMorning].ToolTip = $"{employees}";
                    indexMorning++;
                }
                // If afternoon shift
                else if (statistic[2].ToString() == "Afternoon")
                {
                    chartEmployeeStatistics.Series["Afternoon"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Afternoon", department).ToString();

                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Afternoon"].Points[indexAfternoon].ToolTip = $"{employees}";
                    indexAfternoon++;
                }
                // If evening shift
                else if (statistic[2].ToString() == "Evening")
                {
                    chartEmployeeStatistics.Series["Evening"].Points.AddXY((statistic[1]), Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Evening", department).ToString();

                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Evening"].Points[indexEvening].ToolTip = $"{employees}";
                    indexEvening++;
                }

                // Displays one employee at a time
                Refresh();
            }
        }

        private void GenerateStatisticsRestockedItemsOnDate(string type, string department)
        {
            string dateFrom;

            // Select dates
            dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");

            // Series
            chartEmployeeStatistics.Series.Add("Restocked Items");

            // Title
            chartEmployeeStatistics.Titles.Add($"Restocked items on {dateFrom} in department '{department}'");

            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Restocked Items"].Points.AddXY(statistic[0].ToString(), statistic[1]);
                // Displays one item at a time
                Refresh();
            }
        }

        private void GenerateStatisticsMostRestockedItems(string type, string department)
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
            chartEmployeeStatistics.Titles.Add($"Most restocked items between {dateFrom} and {dateTo} in department '{department}'");

            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["FamousItems"].Points.AddXY(statistic[1].ToString(), statistic[2]);
                chartEmployeeStatistics.Series["FamousItems"].Label = "#PERCENT{P1}";
                chartEmployeeStatistics.Series["FamousItems"].LegendText = "#AXISLABEL";

                // Displays one item at a time
                Refresh();
            }
        }

        private void GenerateStatisticsYearlyStockRequests(string type, string department)
        {
            chartEmployeeStatistics.Series.Add("Total stock requests");

            chartEmployeeStatistics.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            // Make line thicker
            chartEmployeeStatistics.Series[0].BorderWidth = 3;

            // Title
            chartEmployeeStatistics.Titles.Add($"Total restock requests per year");

            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Total stock requests"].Points.AddXY(statistic[0].ToString(), statistic[1]);
                Refresh();
            }
        }

        private void GenerateStatisticsYearlyProfit(string type, string department)
        {
            chartEmployeeStatistics.Series.Add("Total profit");

            chartEmployeeStatistics.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            // Make line thicker
            chartEmployeeStatistics.Series[0].BorderWidth = 3;

            // Title
            chartEmployeeStatistics.Titles.Add($"Total profit per year in department '{department}'");

            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Total profit"].Points.AddXY(statistic[0].ToString(), statistic[1]);
                Refresh();
            }
        }

        // Top Selling Products
        private void GenerateStatisticsTopSellingProducts(string type, string department)
        {
            string dateFrom;
            string dateTo;

            // Select dates
            dateFrom = dtpFrom.Value.ToString("yyyy/MM/dd");
            dateTo = dtpTo.Value.ToString("yyyy/MM/dd");

            // Series
            chartEmployeeStatistics.Series.Add("FamousItems");

            chartEmployeeStatistics.Series[0].ChartType = SeriesChartType.Pie;

            // Title
            chartEmployeeStatistics.Titles.Add($"Top Selling Products between {dateFrom} and {dateTo} in department '{department}'");

            // Made it fit all data
            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(dateFrom, dateTo, type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["FamousItems"].Points.AddXY(statistic[1].ToString(), statistic[2]);
                chartEmployeeStatistics.Series["FamousItems"].Label = "#PERCENT{P1}";
                chartEmployeeStatistics.Series["FamousItems"].LegendText = "#AXISLABEL";

                // Displays one item at a time
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
                cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Yearly stock requests" ||
                cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Yearly profit")
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


        private void btnShowEmp_Click(object sender, EventArgs e)
        {
            string name = tbEmpNameToFind.Text;
            MessageBox.Show($"{mediaBazaar.foundedPerson(name).ToString()}");
        }

        public void RefreshData()
        {
            mediaBazaar.ReadStocks();
            mediaBazaar.ReadProducts();
            lvStock.Items.Clear();
            foreach (Stock p in mediaBazaar.GetStockList())
            {
                if (mediaBazaar.GetProductById(p.ProductId).DapartmentId == departmentId)
                {
                    ListViewItem l = new ListViewItem(p.ProductId.ToString());
                    l.SubItems.Add(mediaBazaar.GetProductNameById(p.ProductId));
                    l.SubItems.Add(p.Quantity.ToString());

                    lvStock.Items.Add(l);
                    if (p.Quantity < 100)
                    {
                        lvStock.Items[lvStock.Items.Count - 1].BackColor = Color.Orange;

                    }
                    if (p.Quantity == 0)
                    {
                        lvStock.Items[lvStock.Items.Count - 1].BackColor = Color.Red;
                        lvStock.Items[lvStock.Items.Count - 1].ForeColor = Color.White;
                    }
                }
            }

            mediaBazaar.ReadRequests();
            lvRequests.Items.Clear();
            foreach (Request item in mediaBazaar.GetRequestsList())
            {
                if (mediaBazaar.GetProductById(item.ProductId).DapartmentId == departmentId)
                {
                    if (item.RequestedBy == "Manager")
                    {
                        ListViewItem list = new ListViewItem(Convert.ToString(item.Id));
                        list.SubItems.Add(mediaBazaar.GetProductNameById(item.ProductId));
                        list.SubItems.Add(item.Quantity.ToString());
                        list.SubItems.Add(item.Status);
                        list.SubItems.Add(item.RequestedBy);
                        list.SubItems.Add(item.DatE.ToShortDateString());
                        lvRequests.Items.Add(list);
                    }
                }
            }

            LV2.Items.Clear();
            foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
            {
                if (mediaBazaar.GetPersonatById(item.Id).DepartmentId == departmentId)
                {
                    listB = new ListViewItem(Convert.ToString(item.Id));
                    listB.SubItems.Add(item.FirstName);
                    listB.SubItems.Add(item.LastName);
                    listB.SubItems.Add(item.GetEmail);
                    listB.SubItems.Add(Convert.ToString(item.DateOfBirth));
                    listB.SubItems.Add(item.StreetName);
                    listB.SubItems.Add(Convert.ToString(item.HouseNr));
                    listB.SubItems.Add(item.Zipcode);
                    listB.SubItems.Add(item.City);
                    listB.SubItems.Add(Convert.ToString(item.HourlyWage));
                    listB.SubItems.Add(Convert.ToString(item.Role));
                    LV2.Items.Add(listB);
                }
            }
            lvProductList.Items.Clear();
            mediaBazaar.ReadProducts();
            mediaBazaar.ReadDepartment();
            mediaBazaar.ReadStocks();
            foreach (Product p in mediaBazaar.GetProductsList())
            {
                if (mediaBazaar.GetProductById(p.ProductId).DapartmentId == departmentId)
                {
                    ListViewItem l = new ListViewItem(p.ProductId.ToString());
                    l.SubItems.Add(mediaBazaar.GetDepartmentNameById(p.DapartmentId));
                    l.SubItems.Add(p.ProductName);
                    l.SubItems.Add(p.Price.ToString());
                    foreach (Stock s in mediaBazaar.GetStockList())
                    {
                        if (s.ProductId == p.ProductId)
                        {
                            l.SubItems.Add(s.Quantity.ToString());
                        }
                    }


                    lvProductList.Items.Add(l);
                }
            }
        }

        private void btnShowSchedule_Click(object sender, EventArgs e)
        {
            if (cbScheduleMonth.SelectedIndex != -1)
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
                        p.Size = new Size(145, 150);
                        p.Location = new Point(x, y);
                        p.AutoScroll = true;
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
                                if (mediaBazaar.GetPersonatById(s.EmployeeId).DepartmentId == departmentId)
                                {
                                    Label lblSchedule = new Label();
                                    lblSchedule.Name = $"lblWorker{dayN}";
                                    lblSchedule.Location = new Point(5, 35);
                                    lblSchedule.AutoSize = false;
                                    lblSchedule.Size = new Size(130, 24);
                                    String text = $"{mediaBazaar.GetPersonNameById(s.EmployeeId)}({s.ShiftType.ToString().Substring(0, 1)})";
                                    lblSchedule.Font = new Font(lblSchedule.Font.FontFamily, 10);
                                    lblSchedule.Text = text;
                                    schedulesPanels[i].Controls.Add(lblSchedule);
                                    count += 1;
                                }
                            }
                        }
                        if (count > 15)
                        {
                            schedulesPanels[i].BackColor = Color.Red;
                        }
                        else if (count == 15)
                        {
                            schedulesPanels[i].BackColor = Color.Green;
                        }
                        else if (count > 0)
                        {
                            schedulesPanels[i].BackColor = Color.Orange;
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
                                        if (mediaBazaar.GetPersonatById(s.EmployeeId).DepartmentId == departmentId)
                                        {
                                            Label lblSchedule = new Label();
                                            lblSchedule.Name = $"lblWorker{dayN}";
                                            lblSchedule.Location = new Point(5, 35);
                                            lblSchedule.Text = mediaBazaar.GetPersonNameById(s.EmployeeId);
                                            schedulesPanels[i].Controls.Add(lblSchedule);
                                            count++;
                                        }
                                    }
                                }
                                if (count > 15)
                                {
                                    schedulesPanels[i].BackColor = Color.Red;
                                }
                                else if (count == 15)
                                {
                                    schedulesPanels[i].BackColor = Color.Green;
                                }
                                else if (count > 0)
                                {
                                    schedulesPanels[i].BackColor = Color.Orange;
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
                                        if (mediaBazaar.GetPersonatById(s.EmployeeId).DepartmentId == departmentId)
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
                                        if (mediaBazaar.GetPersonatById(s.EmployeeId).DepartmentId == departmentId)
                                        {
                                            Label lblSchedule = new Label();
                                            lblSchedule.Name = $"lblWorker{dayN}";
                                            lblSchedule.Location = new Point(5, 35);
                                            lblSchedule.Text = mediaBazaar.GetPersonNameById(s.EmployeeId);
                                            schedulesPanels[i].Controls.Add(lblSchedule);
                                        }
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
            } else
            {
                MessageBox.Show("Please choose the month first!");
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
                if (p.DepartmentId == departmentId)
                {
                    cbNameOfEmp.Items.Add(p.GetFullName());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvProductList.SelectedItems.Count > 0)
            {
                if (!String.IsNullOrWhiteSpace(tbProductQuantity.Text) && Convert.ToInt32(tbProductQuantity.Text) > 0)
                {
                    mediaBazaar.SendManagerRequest(Convert.ToInt32(lvProductList.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(tbProductQuantity.Text));
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Incorrect quantity");
                }
            }
            else
            {
                MessageBox.Show("Select the Id");
            }
        }

        private void tbProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbProductName.Text))
            {
                List<ListViewItem> items = new List<ListViewItem>();
                string productName = tbProductName.Text;
                RefreshData();
                for (int i = 0; i < lvProductList.Items.Count; i++)
                {
                    if (lvProductList.Items[i].SubItems[2].Text.Contains(productName.ToLower()) || lvProductList.Items[i].SubItems[2].Text.Contains(productName.ToUpper()))
                    {
                        items.Add(lvProductList.Items[i]);
                    }
                }
                lvProductList.Items.Clear();
                foreach (ListViewItem lvi in items)
                {
                    lvProductList.Items.Add(lvi);
                }

            }
            else
            {
                lvProductList.Items.Clear();
                RefreshData();
            }
        }


        private void LV2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.LV2.Sort();
        }

        private void lvStock_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvProductSortByColumn.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvProductSortByColumn.Order == SortOrder.Ascending)
                {
                    lvProductSortByColumn.Order = SortOrder.Descending;
                }
                else
                {
                    lvProductSortByColumn.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvProductSortByColumn.SortColumn = e.Column;
                lvProductSortByColumn.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvStock.Sort();
        }
    }
}
