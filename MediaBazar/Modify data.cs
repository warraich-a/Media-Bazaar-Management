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
       
        public Modify_data(int givenId)
        {
            InitializeComponent();
            conn = new MySqlConnection(connectionString);
            id = givenId;
            ReturnPeopleFromDB(id);
        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            string firstName = tbFirstName.Text;
            string lastName = tbLastName.Text;
            DateTime dateOfBirth = dtpBirthDateEmp.Value;
            string streetName = tbxStreetName.Text;
            int houseNr = Convert.ToInt32(tbxHouseNr.Text);
            string zipcode = tbZipcode.Text;
            string city = tbCity.Text;
            double hourlyWage = Convert.ToDouble(tbxHourlyWage.Text);
            
            UpdateData(this.id, firstName, lastName, dateOfBirth, streetName, houseNr, zipcode, city, hourlyWage);

            AdministratorForm a = new AdministratorForm();
            a.Show();
            this.Close();

           
        }

        public void UpdateData(int id, string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage)
        {

            try
            {
                string sql = "UPDATE person SET firstName = @firstName, lastName = @lastName, dateOfBirth = @DOB, streetName = @streetName, houseNr = @houseNr, city = @city, zipcode = @zipcode, hourlyWage = @hourlyWage WHERE id ='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@firstName", givenFirstName);
                cmd.Parameters.AddWithValue("@lastName", givenSecondName);
                cmd.Parameters.AddWithValue("@DOB", givenDOB);
                cmd.Parameters.AddWithValue("@streetName", givenStreetName);
                cmd.Parameters.AddWithValue("@houseNr", givenHouseNr);
                cmd.Parameters.AddWithValue("@zipcode", givenZipcode);
                cmd.Parameters.AddWithValue("@city", givenCity);
                //cmd.Parameters.AddWithValue("@role", Roles.EMPLOYEE);
                cmd.Parameters.AddWithValue("@hourlyWage", givenHourlyWage);
                conn.Open();
                cmd.ExecuteNonQuery();

            }

            finally
            {
                conn.Close();
            }
        }

        public void ReturnPeopleFromDB(int id)
        {
            try
            {
                string sql = "SELECT id, firstName, lastName, dateOfBirth, streetName, houseNr, city, zipcode, hourlyWage, role FROM person WHERE id = @id "; // a query of what we want
                MySqlCommand cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection

                conn.Open();  // this must be before the execution which is just under this
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cmd.ExecuteReader();
               
                while (dr.Read())
                {
                    tbFirstName.Text = dr[1].ToString();

                    tbLastName.Text = dr[2].ToString();
                    dtpBirthDateEmp.Value = Convert.ToDateTime(dr[3]);
                    tbxStreetName.Text = dr[4].ToString();
                    tbxHouseNr.Text = dr[5].ToString();
                    tbCity.Text = dr[6].ToString();

                    tbZipcode.Text = dr[7].ToString();
                    tbxHourlyWage.Text = dr[8].ToString();
                    cbxRole.Items.Add(dr[9].ToString());                   
                }
               /* foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
                {
                    if (item.Id == id)
                    {
                        tbFirstName.Text = item.FirstName;
                        tbLastName.Text = item.LastName;
                        tbxHourlyWage.Text = item.HourlyWage.ToString(); ;
                        tbxHouseNr.Text = item.HouseNr.ToString();
                        tbxStreetName.Text = item.StreetName;
                        tbZipcode.Text = item.Zipcode;
                        tbCity.Text = item.City;
                        dtpBirthDateEmp.Value = item.DateOfBirth;
                        cbxRole.Items.Add(item.Role);
                    }
                }*/
            }
            finally
            {
                conn.Close();
            }
        }

        private void Modify_data_Load(object sender, EventArgs e)
        {

        }
    }
}
