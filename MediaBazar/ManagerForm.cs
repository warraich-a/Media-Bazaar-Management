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
        ListViewItem listB;
        public ManagerForm()
        {
            InitializeComponent();

            // Add user name
            lblUsername.Text = mediaBazaar.CurrentUser;

            RefreshData();

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
                GenerateStatisticsSalaryPerEmployee(type);
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
                else
                {
                    GenerateStatisticsNrEmployeesPerShift(type);
                }
            }
            else if (type == "Most Restocked Items")
            {
                GenerateStatisticsMostRestockedItems(type);
            }
            else if (type == "Restocked Items On Date")
            {
                GenerateStatisticsRestockedItemsOnDate(type);
            }
            // Profit per year (stock requests)
            else if (type == "Yearly profit")
            {
                GenerateStatisticsYearlyProfit(type);
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

        private void GenerateStatisticsYearlyProfit(string type)
        {
            chartEmployeeStatistics.Series.Add("Total restock requests");

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
                chartEmployeeStatistics.Series["Total restock requests"].Points.AddXY(statistic[0].ToString(), statistic[1]);

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

            LV2.Items.Clear();
            foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
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
                    if (count >= 5)
                    {
                        schedulesPanels[i].BackColor = Color.Red;
                    }
                    else if (count == 4)
                    {
                        schedulesPanels[i].BackColor = Color.Yellow;
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
