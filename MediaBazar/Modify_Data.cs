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
    public partial class Modify_data : Form
    {
        int id;
        AdministratorForm form;
        MediaBazaar mediaBazaar;

        public Modify_data(int givenId, AdministratorForm f, MediaBazaar mediaBazaar)
        {
            InitializeComponent();
           
            form = f;
            id = givenId;
            this.mediaBazaar = mediaBazaar;
            FoundPerson(id);
            btnAddNewEmployee.Text = "Update";
            this.Text = "Update Data";
           // cbxRole.DataSource = Enum.GetValues(typeof(Roles)); //casting the enum class to combobox
            Departments();
        }
        // a new constructor add a new employee
        public Modify_data(MediaBazaar mediaBazaar, AdministratorForm f)
        {
            form = f;
            InitializeComponent();
            this.mediaBazaar = mediaBazaar;
           // cbxRole.DataSource = Enum.GetValues(typeof(Roles)); //casting the enum class to combobox
            btnAddNewEmployee.Text = "Add";
            this.Text = "Add New Employee";
            Departments();

        }
        // To add a new employee in the system
        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            try
            {
               
                string firstName = tbFirstName.Text;
                string lastName = tbLastName.Text;
                DateTime dateOfBirth = dtpBirthDateEmp.Value;
                string streetName = tbxStreetName.Text;
                int houseNr = Convert.ToInt32(tbxHouseNr.Text);
                string zipcode = tbZipcode.Text;
                string city = tbCity.Text;
                double hourlyWage = Convert.ToDouble(tbxHourlyWage.Text);
                string role = cbxRole.SelectedItem.ToString();
                
                
                if (btnAddNewEmployee.Text == "Add")
                {
                    if (MessageBox.Show("Do you want to Add this person?", "Add Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if(role == "Manager")
                        {
                            mediaBazaar.AddPerson(firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, role, "");
                            this.Close();
                            form.AddEmployeesToList();
                            form.RefreshData();
                        }
                        else
                        {
                            mediaBazaar.AddPerson(firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, role, cmbDepartment.SelectedItem.ToString());
                            this.Close();
                            form.AddEmployeesToList();
                            form.RefreshData();

                        }
                    }
                    else
                    {
                        MessageBox.Show("The person is not added");
                    }
                }
                else
                {
                    if (MessageBox.Show("Are you sure", "Update Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (role == "Manager")
                        {
                            mediaBazaar.UpdateData(this.id, firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, cbxRole.SelectedItem.ToString(), "");
                            this.Close();
                            form.AddEmployeesToList();
                            form.RefreshData();
                        }
                        else
                        {
                            mediaBazaar.UpdateData(this.id, firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, cbxRole.SelectedItem.ToString(), cmbDepartment.SelectedItem.ToString());
                            this.Close();
                            form.AddEmployeesToList();
                            form.RefreshData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Information is not updated");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Have you added everything?");
            }
        }

        // getting a person's data by specific ID
        public void FoundPerson(int id)
        {
            try
            {
                Person foundPerson = mediaBazaar.ReturnPerson(id); // to give the correct id through parameters
                tbFirstName.Text = foundPerson.FirstName;
                tbLastName.Text = foundPerson.LastName;
                dtpBirthDateEmp.Text = foundPerson.DateOfBirth.ToString();
                tbxStreetName.Text = foundPerson.StreetName;
                tbxHouseNr.Text = foundPerson.HouseNr.ToString();
                tbCity.Text = foundPerson.Zipcode;
                tbZipcode.Text = foundPerson.City;
                tbxHourlyWage.Text = foundPerson.HourlyWage.ToString();
                cbxRole.Text = foundPerson.Role.ToString();
                foreach (Department item in mediaBazaar.GetAllDepartments())
                {
                    if (foundPerson.DepartmentId == item.Id)
                    {
                        cmbDepartment.Text = item.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Departments()
        {
            cmbDepartment.Items.Clear();
            foreach (Department d in mediaBazaar.GetAllDepartments())
            {
                cmbDepartment.Items.Add(d.Name);
            }

        }
    }
}
