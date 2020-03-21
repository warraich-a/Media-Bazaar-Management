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
        MediaBazaar mediaBazaar = new MediaBazaar();
        ListViewItem list;
        public AdministratorForm()
        {
            InitializeComponent();
            cbxRole.DataSource = Enum.GetValues(typeof(Roles)); //casting the enum class to combobox
           RefreshData();


        }
        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            mediaBazaar.AddPerson("Aqib", "Butt", dtpBirthDateEmp.Value, "Jannismunnestraat", 28, "5731HJ", "Geldrop", 8);
            mediaBazaar.AddPerson("Arsalan", "Ahmad", dtpBirthDateEmp.Value, "Idontknow", 54, "5731HA", "Neunan", 10.2);
            mediaBazaar.AddPerson("Zehrish", "Khan", dtpBirthDateEmp.Value, "NowayStraat", 54, "5731AS", "Eindhoven", 10.2);
            mediaBazaar.AddPerson("James", "Bond", dtpBirthDateEmp.Value, "shoid", 28, "5731HJ", "New York", 2);
            mediaBazaar.AddPerson("Keanue", "Singh", dtpBirthDateEmp.Value, "LifeNoeasy", 33, "5731HA", "Alberta", 10.2);
            mediaBazaar.AddPerson("Brad", "Pitt", dtpBirthDateEmp.Value, "Awww", 54, "5731AS", "Breda", 10.2);
            /* string firstName = tbFirstName.Text;
             string lastName = tbLastName.Text;
             DateTime dateOfBirth = dtpBirthDateEmp.Value;
             string streetName = tbxStreetName.Text;
             int houseNr = Convert.ToInt32(tbxHouseNr.Text);
             string zipcode = tbZipCode.Text;
             string city = tbCity.Text;
             double hourlyWage = Convert.ToDouble(tbxHourlyWage.Text);
             Roles role = (Roles)cbxRole.SelectedItem;
             mediaBazaar.AddPerson(firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, role);*/
           // RefreshData();
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

        private void btnRemoveEmp_Click(object sender, EventArgs e)
        {
           int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
           mediaBazaar.RemovePerson(Convert.ToInt32(id));
            this.Refresh();
        }


        private void btnModifyStack_Click(object sender, EventArgs e)
        {
            ManagerForm m = new ManagerForm(mediaBazaar);
            m.Show();
        }

        private void btnModifyEmp_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            Modify_data m = new Modify_data(id);
            m.Show();
            this.Close();
        }

        private void AdministratorForm_Load(object sender, EventArgs e)
        {

        }

        private void btnShowSchedule_Click(object sender, EventArgs e)
        {
            lvSchedule.Items.Clear();
            string shift = "";
            if(comboBox1.Text == "AFTERNOON")
            {
                shift = "Afternoon";
            } else if (comboBox1.Text == "EVENING")
            {
                shift = "Evening";
            }
            else if (comboBox1.Text == "MORNING")
            {
                shift = "Morning";
            }
            if(cbSelectAll.Checked)
            {
                RefreshData();
            } else if (cbNameOfEmp.SelectedIndex != -1 && dtpDateShedule.Checked)
            {
                foreach (Schedule item in mediaBazaar.VeiwSpecificSchedule1(mediaBazaar.GetPersonIdByName(cbNameOfEmp.Text), dtpDateShedule.Value.Date))
                {

                    list = new ListViewItem(Convert.ToString(mediaBazaar.GetPersonNameById(item.EmployeeId)));

                    list.SubItems.Add(Convert.ToString(item.DATETime));
                    list.SubItems.Add(Convert.ToString(item.ShiftType));
                    lvSchedule.Items.Add(list);
                }
            } else if(dtpDateShedule.Checked && comboBox1.SelectedIndex != -1)
            {
                foreach (Schedule item in mediaBazaar.VeiwSpecificSchedule2( dtpDateShedule.Value.Date, shift))
                {

                    list = new ListViewItem(Convert.ToString(mediaBazaar.GetPersonNameById(item.EmployeeId)));

                    list.SubItems.Add(Convert.ToString(item.DATETime));
                    list.SubItems.Add(Convert.ToString(item.ShiftType));
                    lvSchedule.Items.Add(list);
                }
            }



            
            
        }

        private void cbNameOfEmp_Click(object sender, EventArgs e)
        {
            cbNameOfEmp.Items.Clear();
            foreach(Person p in mediaBazaar.GetPeopleList())
            {
                cbNameOfEmp.Items.Add(p.ToString());
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(Shift.AFTERNOON);
            comboBox1.Items.Add(Shift.EVENING);
            comboBox1.Items.Add(Shift.MORNING);
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

        private void metroTabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
