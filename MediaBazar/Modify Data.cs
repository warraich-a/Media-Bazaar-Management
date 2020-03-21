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
        string connectionString = "Server=studmysql01.fhict.local;Uid=dbi435688;Database=dbi435688;Pwd=webhosting54;";
        MySqlConnection conn;
        int id;
        AdministratorForm form;
        MediaBazaar mediaBazaar;

        public Modify_data(int givenId, AdministratorForm f, MediaBazaar mediaBazaar)
        {
            InitializeComponent();
            conn = new MySqlConnection(connectionString);
            form = f;
            id = givenId;
            this.mediaBazaar = mediaBazaar;
            ReturnPeopleFromDB(id);

            cbxRole.DataSource = Enum.GetValues(typeof(Roles)); //casting the enum class to combobox
        }
        // a new constructor add a new employee
        public Modify_data(MediaBazaar mediaBazaar, AdministratorForm f)
        {
            form = f;
            InitializeComponent();
            this.mediaBazaar = mediaBazaar;
            cbxRole.DataSource = Enum.GetValues(typeof(Roles)); //casting the enum class to combobox

        }
        // To add a new employee in the system
        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to Add this person?", "Add Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //mediaBazaar.AddPerson("Aqib", "Butt", dtpBirthDateEmp.Value, "Jannismunnestraat", 28, "5731HJ", "Geldrop", 8, cbxRole.SelectedItem.ToString());
                    string firstName = tbFirstName.Text;
                    string lastName = tbLastName.Text;
                    DateTime dateOfBirth = dtpBirthDateEmp.Value;
                    string streetName = tbxStreetName.Text;
                    int houseNr = Convert.ToInt32(tbxHouseNr.Text);
                    string zipcode = tbZipcode.Text;
                    string city = tbCity.Text;
                    double hourlyWage = Convert.ToDouble(tbxHourlyWage.Text);
                    mediaBazaar.AddPerson(firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, cbxRole.SelectedItem.ToString());
                    form.RefreshData();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("The person is not added");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Have you added everything?");
            }
        }

        // a method to modify the data
        public void UpdateData(int id, string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, string roles)
        {
            bool personExist = false;
            try
            {
                foreach (Person item in mediaBazaar.ReturnPeopleFromDB()) // to check if the Person with the same name already exists
                {
                    if (item.FirstName + item.LastName == givenFirstName + givenSecondName)
                    {
                        personExist = true;
                    }
                }
                if (!personExist)
                {
                    string sql = "UPDATE person SET firstName = @firstName, lastName = @lastName, dateOfBirth = @DOB, streetName = @streetName, houseNr = @houseNr, city = @city, zipcode = @zipcode, hourlyWage = @hourlyWage, role = @role WHERE id ='" + id + "';";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (givenFirstName == "" || givenSecondName == "" || givenStreetName == "" || givenZipcode == "" || givenCity == "" || givenHourlyWage == 0 || givenHouseNr == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("None of the above requirements should be empty");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@firstName", givenFirstName);
                        cmd.Parameters.AddWithValue("@lastName", givenSecondName);
                        cmd.Parameters.AddWithValue("@DOB", givenDOB);
                        cmd.Parameters.AddWithValue("@streetName", givenStreetName);
                        cmd.Parameters.AddWithValue("@houseNr", givenHouseNr);
                        cmd.Parameters.AddWithValue("@zipcode", givenZipcode);
                        cmd.Parameters.AddWithValue("@city", givenCity);
                        cmd.Parameters.AddWithValue("@role", roles);
                        cmd.Parameters.AddWithValue("@hourlyWage", givenHourlyWage);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("The information has been updated");
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Person with the same name already exists");
                }
            }

            finally
            {
                conn.Close();
            }
        }

        // getting a person's data by specific ID
        public void ReturnPeopleFromDB(int id)
        {
            try
            {
                string sql = "SELECT firstName, lastName, dateOfBirth, streetName, houseNr, city, zipcode, hourlyWage, role FROM person WHERE id = @id "; // a query of what we want
                MySqlCommand cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection

                conn.Open();  // this must be before the execution which is just under this
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tbFirstName.Text = dr[0].ToString();
                    tbLastName.Text = dr[1].ToString();
                    dtpBirthDateEmp.Value = Convert.ToDateTime(dr[2]);
                    tbxStreetName.Text = dr[3].ToString();
                    tbxHouseNr.Text = dr[4].ToString();
                    tbCity.Text = dr[5].ToString();

                    tbZipcode.Text = dr[6].ToString();
                    tbxHourlyWage.Text = dr[7].ToString();

                    cbxRole.Text = dr[8].ToString();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void Modify_data_Load(object sender, EventArgs e)
        {

        }

        //Updating a selected person's data
        private void btnUpdate_Click(object sender, EventArgs e)
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


                if (MessageBox.Show("Are you sure", "Update Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateData(this.id, firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage, cbxRole.SelectedItem.ToString());

                    form.RefreshData();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Information is not updated");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


    }
}
