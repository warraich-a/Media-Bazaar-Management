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
using LiveCharts.Wpf;
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
        List<Schedule> schedules;
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
            GetProducts();
            AddEmployeesToList();


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
                       // MessageBox.Show(item.ToString());
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


            // Generate departments in statistics
            GenerateDepartments();
            // get schedules
            schedules = new List<Schedule>();
            mediaBazaar.ReadSchedule();
            schedules = mediaBazaar.GetSchedulesList();

            RefreshTable();
        }

        public void RefreshTable()
        {
            lblTitle.Text = "Proposed shifts";

            if (listView3.Columns.Count < 4)
            {
                listView3.Columns.Add("Date", 150);
                listView3.Columns.Add("Shift Type", 150);
            }

            listView3.Items.Clear();
            foreach (Schedule s in schedules)
            {
                if (s.Status == ShiftStatus.PROPOSED)
                {
                    list = new ListViewItem(s.SheduleId.ToString(), 0);
                    list.SubItems.Add(mediaBazaar.GetPersonNameById(s.EmployeeId));
                    list.SubItems.Add(s.DATETime.ToString("dd-MM-yyyy"));
                    list.SubItems.Add(s.ShiftType.ToString());
                    listView3.Items.Add(list);
                }
            }
        }

        public void RefreshData()
        {
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
        public void Departments()
        {
            cmbDepartmentStack.Items.Clear();
            cmbDepartment.Items.Clear();
            cmbSearchByDepartmentProduct.Items.Clear();

           
            foreach (Department d in mediaBazaar.GetAllDepartments())
            {
                cmbDepartmentStack.Items.Add(d.Name);

                cmbSearchByDepartmentProduct.Items.Add(d.Name);

                cmbDepartment.Items.Add(d.Name);
                /*if (radioButton4.Checked)
                {
                    foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
                    {
                        if(item.Role == Roles.Manager)
                        {
                            cmbDepartment.Items.Add(item.FirstName);
                        } 
                    }
                }
                else if(radioButton5.Checked)
                {*/

                //}
            }
            cmbDepartment.Items.Add("All");
            cmbSearchByDepartmentProduct.Items.Add("All");
        }
        public void GetProducts()
        {
            listViewProducts.Items.Clear();
            foreach (Product p in mediaBazaar.GetProducts())
            {
                listOfProducts = new ListViewItem(p.ProductId.ToString());
                foreach (Department item in mediaBazaar.GetAllDepartments())
                {
                    if (p.DapartmentId == item.Id)
                    {
                        listOfProducts.SubItems.Add(Convert.ToString(item.Name));
                    }
                }
                listOfProducts.SubItems.Add(p.Name);
                listOfProducts.SubItems.Add(Convert.ToString(p.Price));
                listOfProducts.SubItems.Add(Convert.ToString(p.SellingPrice));
                listViewProducts.Items.Add(listOfProducts);
            }
        }

        public void AddEmployeesToList()
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

        /* Logout */
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
            string department = cbxDepartments.GetItemText(cbxDepartments.SelectedItem);


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
            // Stock requests per year
            else if (type == "Yearly stock requests")
            {
                GenerateStatisticsYearlyStockRequests(type, department);
            }
            // Profit per year
            else if (type == "Yearly profit")
            {
                GenerateStatisticsYearlyProfit(type, department);
            }
            // Number of employees per department
            else if (type == "Number of employees per department")
            {
                GenerateStatisticsNumberEmployeesPerDep(type, department);
            }
            // Top Selling Products
            else if(type == "Top Selling Products")
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
            //chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

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
                if (statistic[2].ToString() == "Morning")
                {

                    // MessageBox.Show(String.Format("{0:MM/dd/yyyy}", statistic[1])); 
                    chartEmployeeStatistics.Series["Morning"].Points.AddXY(statistic[1], Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Morning", department);
                    //// Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Morning"].Points[indexMorning].ToolTip = $"{employees}";

                    indexMorning++;
                }
                else if (statistic[2].ToString() == "Afternoon")
                {
                    chartEmployeeStatistics.Series["Afternoon"].Points.AddXY(statistic[1], Convert.ToInt32(statistic[0]));

                    string employees = mediaBazaar.GetEmployeesPerShift(Convert.ToDateTime(statistic[1]), "Afternoon", department).ToString();

                    // Add tooltip, Employees working that day that shift
                    chartEmployeeStatistics.Series["Afternoon"].Points[indexAfternoon].ToolTip = $"{employees}";

                    indexAfternoon++;
                }
                else if (statistic[2].ToString() == "Evening")
                {
                    chartEmployeeStatistics.Series["Evening"].Points.AddXY(statistic[1], Convert.ToInt32(statistic[0]));


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
                // Displays one employee at a time
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


            chartEmployeeStatistics.Series[0].ChartType = SeriesChartType.Pie;

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

                // Displays one employee at a time
                Refresh();
            }
        }

        private void GenerateStatisticsYearlyStockRequests(string type, string department)
        {
            chartEmployeeStatistics.Series.Add("Total stock requests");


            chartEmployeeStatistics.Series[0].ChartType = SeriesChartType.Spline;

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            // Make line thicker
            chartEmployeeStatistics.Series[0].BorderWidth = 3;

            // Title
            chartEmployeeStatistics.Titles.Add($"Total stock requests per year in department '{department}'");

            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Total stock requests"].Points.AddXY(statistic[0].ToString(), statistic[1]);

                // Displays one employee at a time
                Refresh();
            }
        }


        private void GenerateStatisticsYearlyProfit(string type, string department)
        {
            chartEmployeeStatistics.Series.Add("Total profit");


            chartEmployeeStatistics.Series[0].ChartType = SeriesChartType.Spline;

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

                // Displays one employee at a time
                Refresh();
            }
        }


        private void GenerateStatisticsNumberEmployeesPerDep(string type, string department)
        {
            chartEmployeeStatistics.Series.Add("Number of employees");

            chartEmployeeStatistics.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            // Make line thicker
            chartEmployeeStatistics.Series[0].BorderWidth = 3;

            // Title
            chartEmployeeStatistics.Titles.Add($"Number of employees per department");

            chartEmployeeStatistics.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            ArrayList statistics = mediaBazaar.GetStatistics(type, department);

            foreach (object[] statistic in statistics)
            {
                chartEmployeeStatistics.Series["Number of employees"].Points.AddXY(statistic[0].ToString(), statistic[1]);

                // Displays one employee at a time
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

                // Displays one employee at a time
                Refresh();
            }
        }


        /* CHOSEN STATISTICS */
        private void cbxCategoryStatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            // All exept for number of employees per department
            // Enable department picking
            cbxDepartments.Enabled = true;

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
            // Number of employees per department
            else if (cbxCategoryStatistics.GetItemText(cbxCategoryStatistics.SelectedItem) == "Number of employees per department")
            {
                // Disable department picking
                cbxDepartments.Enabled = false;

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

        /* Generate departments */
        private void GenerateDepartments()
        {
            ArrayList departments = mediaBazaar.GetDepartments();

            cbxDepartments.Items.Clear();
            cbxDepartments.Items.Add("All");
            foreach (object[] department in departments)
            {
                cbxDepartments.Items.Add(department[1]);
            }

            cbxDepartments.SelectedIndex = 0;
            cbxCategoryStatistics.SelectedIndex = 0;
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
                double sellingPrice = Convert.ToDouble(tbSellingPrice.Text);
                if (sellingPrice < productPrice)
                {
                    if (MessageBox.Show("Are you very rich????", "Remove Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        mediaBazaar.AddProduct(departmentId, productName, productPrice, sellingPrice);
                        GetProducts();
                        tbProductName.Text = "";
                        tbProductPrice.Text = "";
                        tbSellingPrice.Text = "";
                        cmbDepartmentStack.Text = "";
                    }
                }
                else
                {
                    mediaBazaar.AddProduct(departmentId, productName, productPrice, sellingPrice);
                    GetProducts();
                    tbProductName.Text = "";
                    tbProductPrice.Text = "";
                    tbSellingPrice.Text = "";
                    cmbDepartmentStack.Text = "";
                }

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
            GetProducts();
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
                GetProducts();
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

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentId = cmbDepartment.SelectedIndex + 1;

            listView1.Items.Clear();
            foreach (Person item in mediaBazaar.GetPeople())
            {
                if (item.DepartmentId == departmentId)
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
            if(cmbDepartment.Text == "All")
            {
                AddEmployeesToList();
            }
        }

        private void cmbSearchByDepartmentProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentId = cmbSearchByDepartmentProduct.SelectedIndex + 1;
      
            listViewProducts.Items.Clear();
            foreach (Product p in mediaBazaar.GetProducts())
            {
                if (p.DapartmentId == departmentId)
                {
                    listOfProducts = new ListViewItem(p.ProductId.ToString());
                    foreach (Department item in mediaBazaar.GetAllDepartments())
                    {
                        if (p.DapartmentId == item.Id)
                        {
                            listOfProducts.SubItems.Add(Convert.ToString(item.Name));
                        }
                    }
                    listOfProducts.SubItems.Add(p.Name);
                    listOfProducts.SubItems.Add(Convert.ToString(p.Price));
                    listOfProducts.SubItems.Add(Convert.ToString(p.SellingPrice));
                    listViewProducts.Items.Add(listOfProducts);
                }
            }
            if(cmbSearchByDepartmentProduct.Text == "All")
            {
                GetProducts();
            }
        }

        private void btnAssignShift_Click(object sender, EventArgs e)
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

        private void btnAutoAssign_Click(object sender, EventArgs e)
        {
            int NoOfDays = 30, Shift = 0, NoOfEmp, NoOfChanges = 0;
            string[] shifttype = new string[3];
            shifttype[0] = "Morning";
            shifttype[1] = "Afternoon";
            shifttype[2] = "Evening";
            lblTitle.Text = "AutoAssigned shifts";
            listView3.Items.Clear();
            DateTime startday = dtpTimeForShift.Value;
            if (startday.DayOfWeek.ToString() == "Saturday") startday = startday.AddDays(2);
            else if (startday.DayOfWeek.ToString() == "Sunday") startday = startday.AddDays(1);
            string date = startday.ToString("yyyy-MM-dd");
            int nrM, nrA, nrE;
            nrM = mediaBazaar.GetShiftsByDay(date)[0];
            nrA = mediaBazaar.GetShiftsByDay(date)[1];
            nrE = mediaBazaar.GetShiftsByDay(date)[2];
            if ((nrM == 5) && (nrA == 5) && (nrE == 5)) MessageBox.Show("No shifts available for this day: " + date);
            else
            {
                while (NoOfDays > 0)
                {
                    while (Shift < 3)
                    {
                        //test shift if full
                        date = startday.ToString("yyyy-MM-dd");
                        NoOfEmp = mediaBazaar.CheckProposalNrShift(shifttype[Shift], date);
                        if (NoOfEmp < 5)
                        {
                            // get proposals
                            mediaBazaar.ReadProposeByDay(date, shifttype[Shift]);
                            foreach (Schedule s in mediaBazaar.GetLimSchedulesListByType(5 - NoOfEmp))
                            {
                                list = new ListViewItem(s.SheduleId.ToString(), 0);
                                list.SubItems.Add(mediaBazaar.GetPersonNameById(s.EmployeeId));
                                list.SubItems.Add(startday.ToString("dd-MM-yyyy"));
                                list.SubItems.Add(shifttype[Shift]);
                                listView3.Items.Add(list);

                                //set auto-assigned status 
                                mediaBazaar.ChangeScheduleStatusById(s.SheduleId, "AutoAssigned");
                                NoOfChanges++;
                            }
                        }
                        Shift++;
                    }
                    if (startday.DayOfWeek.ToString() == "Friday") { startday = startday.AddDays(3); NoOfDays -= 3; }
                    else { startday = startday.AddDays(1); NoOfDays--; }
                    Shift = 0;

                }
                MessageBox.Show("Total of schedules auto-assigned: " + NoOfChanges.ToString());
            }
            RefreshTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshTable();
            //btnAcceptShift.Enabled = true;
            //btnRejectShift.Enabled = true;
        }

        private void dtpTimeForShift_ValueChanged(object sender, EventArgs e)
        {
            DateTime startday = dtpTimeForShift.Value;
            string date = startday.ToString("yyyy-MM-dd");
            lblTitle.Text = "Available employees on " + date;

            if (listView3.Columns.Count > 2)
            {
                listView3.Columns.RemoveAt(3);
                listView3.Columns.RemoveAt(2);
            }


            listView3.Items.Clear();
            //btnAcceptShift.Enabled = false;
            //btnRejectShift.Enabled = false;
            //string[] shifttype = new string[3];
            //shifttype[0] = "Morning";
            //shifttype[1] = "Afternoon";
            //shifttype[2] = "Evening";
            // show shifts on specific date

            List<Person> availablePeople = mediaBazaar.GetAvailablePeopleByDay(date);

            //MessageBox.Show(availablePeople.Count.ToString());
            foreach (Person p in availablePeople)
            {
                list = new ListViewItem(Convert.ToString(p.Id));
                list.SubItems.Add(p.FirstName + p.LastName);
                listView3.Items.Add(list);
            }

            //mediaBazaar.GetAvailablePeopleByDay(date);
            //foreach (Schedule s in mediaBazaar.GetProposeByDay(date))
            //{
            //    list = new ListViewItem(s.SheduleId.ToString(), 0);
            //    list.SubItems.Add(mediaBazaar.GetPersonNameById(s.EmployeeId));
            //    list.SubItems.Add(startday.ToString("dd-MM-yyyy"));
            //    list.SubItems.Add(s.ShiftType.ToString());
            //    listView3.Items.Add(list);
            //}
        }
    }
}
