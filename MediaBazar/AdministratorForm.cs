using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaBazar
{
    public partial class AdministratorForm : Form
    {
<<<<<<< HEAD

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

=======
        MediaBazaar mediaBazaar = new MediaBazaar();
        ListViewItem list;
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
        public AdministratorForm()
        {
            InitializeComponent();
<<<<<<< HEAD

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
=======
            RefreshData();
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
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
            lvSchedule.Items.Clear();
            foreach (Schedule item in mediaBazaar.VeiwSchedule())
            {

                list = new ListViewItem(Convert.ToString(mediaBazaar.GetPersonNameById(item.EmployeeId)));

                list.SubItems.Add(Convert.ToString(item.DATETime));
                list.SubItems.Add(Convert.ToString(item.ShiftType));
                lvSchedule.Items.Add(list);
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

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {

<<<<<<< HEAD
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
=======
            Modify_data Add = new Modify_data(mediaBazaar, this);
            Add.Show();
        }
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9

        // to show the data in a list view item
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
<<<<<<< HEAD
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
=======
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                Modify_data m = new Modify_data(id, this); // sending the id to modify data form through the parameters
                m.Show();
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
            }
            catch (Exception)
            {
<<<<<<< HEAD
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

        private void btnRemoveShift_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the shift?", "Delete Shift", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                mediaBazaar.RemoveSchedule(mediaBazaar.GetSchedule(lvSchedule.SelectedItems[0].SubItems[0].Text, lvSchedule.SelectedItems[0].SubItems[1].Text, lvSchedule.SelectedItems[0].SubItems[2].Text));
                RefreshData();
            }
            else if (dialogResult == DialogResult.No)
            {
                RefreshData();
            }
        }

        private void btnShowSchedule_Click(object sender, EventArgs e)
        {
            lvSchedule.Items.Clear();
            string shift = "";
            if (comboBox1.Text == "AFTERNOON")
            {
                shift = "Afternoon";
            }
            else if (comboBox1.Text == "EVENING")
            {
                shift = "Evening";
            }
            else if (comboBox1.Text == "MORNING")
            {
                shift = "Morning";
            }
            if (cbSelectAll.Checked)
            {
                RefreshData();
            }
            else if (cbNameOfEmp.SelectedIndex != -1 && dtpDateShedule.Checked)
            {
                foreach (Schedule item in mediaBazaar.VeiwSpecificSchedule1(mediaBazaar.GetPersonIdByName(cbNameOfEmp.Text), dtpDateShedule.Value.Date))
                {

                    list = new ListViewItem(Convert.ToString(mediaBazaar.GetPersonNameById(item.EmployeeId)));

                    list.SubItems.Add(Convert.ToString(item.DATETime));
                    list.SubItems.Add(Convert.ToString(item.ShiftType));
                    lvSchedule.Items.Add(list);
                }
            }
            else if (dtpDateShedule.Checked && comboBox1.SelectedIndex != -1)
            {
                foreach (Schedule item in mediaBazaar.VeiwSpecificSchedule2(dtpDateShedule.Value.Date, shift))
                {

                    list = new ListViewItem(Convert.ToString(mediaBazaar.GetPersonNameById(item.EmployeeId)));

                    list.SubItems.Add(Convert.ToString(item.DATETime));
                    list.SubItems.Add(Convert.ToString(item.ShiftType));
                    lvSchedule.Items.Add(list);
                }
=======

                MessageBox.Show("No employee is selected");
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
            }
                  
        }

        private void cbNameOfEmp_Click(object sender, EventArgs e)
        {
            cbNameOfEmp.Items.Clear();
            foreach (Person p in mediaBazaar.GetPeopleList())
            {
                cbNameOfEmp.Items.Add(p.GetFullName());
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(Shift.AFTERNOON);
            comboBox1.Items.Add(Shift.EVENING);
            comboBox1.Items.Add(Shift.MORNING);
        }
    }
}
