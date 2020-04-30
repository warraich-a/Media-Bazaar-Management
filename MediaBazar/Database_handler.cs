using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace MediaBazar
{
    class Database_handler
    {

        string connectionString = "Server=studmysql01.fhict.local;Uid=dbi435688;Database=dbi435688;Pwd=webhosting54;SslMode=none";
        MySqlConnection conn;
        List<Schedule> schedules = new List<Schedule>();
        Person person = new Person();
        List<Person> people = new List<Person>();
        List<string> departments = new List<string>();
        List<Product> products;


        public Database_handler()
        {
            conn = new MySqlConnection(connectionString);
        }

        public List<Schedule> ReadSchedule()
        {
            this.schedules = new List<Schedule>();
            try
            {
                string sql = "SELECT `id`, `employeeId`, `shiftType`, `date`, `statusOfShift` FROM `schedule`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Shift a = Shift.MORNING;
                    if (dr[2].ToString() == "Morning")
                    {
                        a = Shift.MORNING;
                    }
                    else if (dr[2].ToString() == "Afternoon")
                    {
                        a = Shift.AFTERNOON;
                    }
                    else if (dr[2].ToString() == "Evening")
                    {
                        a = Shift.EVENING;
                    }

                    ShiftStatus b = ShiftStatus.ASSIGNED;
                    if (dr[4].ToString() == "Assigned")
                    {
                        b = ShiftStatus.ASSIGNED;
                    }
                    else if (dr[4].ToString() == "Proposed")
                    {
                        b = ShiftStatus.PROPOSED;
                    }
                    else if (dr[4].ToString() == "Accepted")
                    {
                        b = ShiftStatus.ACCEPTED;
                    }
                    else if (dr[4].ToString() == "Rejected")
                    {
                        b = ShiftStatus.REJECTED;
                    }




                    Schedule g = new Schedule(Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]), a, Convert.ToDateTime(dr[3]), b);
                    schedules.Add(g);
                }
            }
            finally
            {
                conn.Close();
            }
            return schedules;
        }

        /* EMPLOYEE MANAGEMENT */
        public void AddPersonToDatabase(string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, string roles)
        {
            string givenEmail;
            string newPassword;
            bool personExist = false;
            try
            {
                foreach (Person item in people) // to check if the Person with the same name already exists
                {
                    if (item.FirstName + item.LastName == givenFirstName + givenSecondName)
                    {
                        personExist = true;
                    }
                }

                if (personExist == false)
                {
                    givenEmail = person.Email(givenFirstName, givenSecondName);
                    newPassword = person.SetPassword();
                    string sql = "INSERT INTO person(firstName, lastName, email, dateOfBirth, streetName, houseNr,zipcode, city, hourlyWage, password, role) VALUES(@firstName, @lastName, @email, @DOB, @streetName, @houseNr, @zipcode, @city, @hourlyWage, @password, @role)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (givenFirstName == "" || givenSecondName == "" || givenStreetName == "" || givenZipcode == "" || givenCity == "" || givenHourlyWage == 0 || givenHouseNr == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("None of the above requirements should be empty");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@firstName", givenFirstName);
                        cmd.Parameters.AddWithValue("@lastName", givenSecondName);
                        cmd.Parameters.AddWithValue("@email", givenEmail);
                        cmd.Parameters.AddWithValue("@DOB", givenDOB);
                        cmd.Parameters.AddWithValue("@streetName", givenStreetName);
                        cmd.Parameters.AddWithValue("@houseNr", givenHouseNr);
                        cmd.Parameters.AddWithValue("@zipcode", givenZipcode);
                        cmd.Parameters.AddWithValue("@city", givenCity);
                        cmd.Parameters.AddWithValue("@role", roles);
                        cmd.Parameters.AddWithValue("@hourlyWage", givenHourlyWage);
                        cmd.Parameters.AddWithValue("@password", newPassword);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("Person has been Added to the System");
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

        // to remove a person
        public void PersonToRemoveFromDataBase(int personId)
        {
            string sql = "";
            bool fkDataDeleted = false;
            try
            {
                MySqlCommand cmd;
                // to remove the data first from schedule otherwise becuase of the foreing key. Otherwise it wont work. First the data from the child has to be removed
                if (sql != "DELETE FROM schedule WHERE employeeId = @id")
                {
                    sql = "DELETE FROM schedule WHERE employeeId = @id";
                    cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection
                    cmd.Parameters.AddWithValue("@id", personId);
                    conn.Open();  // this must be before the execution which is just under this
                    cmd.ExecuteNonQuery();
                    fkDataDeleted = true;
                }
                if (fkDataDeleted) // removing the data from the main table
                {
                    sql = "DELETE FROM person WHERE id = @id"; // a query of what we want
                    cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection
                    cmd.Parameters.AddWithValue("@id", personId);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }
        }
        public Person foundedPersonFromDatabase(string givenName)
        {
            Person g = null;
            try
            {
                string sql = "SELECT id, firstName, lastName, dateOfBirth, streetName, houseNr, city, zipcode, hourlyWage, role FROM person WHERE firstName = @name"; // Getting the person by name
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", givenName);
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Roles r = Roles.Employee;
                    if (dr[9].ToString() == "Administrator")
                    {
                        r = Roles.Administrator;
                    }
                    else if (dr[9].ToString() == "Manager")
                    {
                        r = Roles.Manager;
                    }
                    else if (dr[9].ToString() == "DepotWorker")
                    {
                        r = Roles.DepotWorker;
                    }

                    g = new Person(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[3]), dr[4].ToString(), Convert.ToInt32(dr[5]), dr[6].ToString(), dr[7].ToString(), Convert.ToDouble(dr[8]), r);
                }
                return g;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Person> ReturnPeopleFromDB()
        {
            people = new List<Person>();
            try
            {
                string sql = "SELECT id, firstName, lastName, dateOfBirth, streetName, houseNr, city, zipcode, hourlyWage, role FROM person"; // a query of what we want
                MySqlCommand cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection

                conn.Open();  // this must be before the execution which is just under this
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Roles r = Roles.Employee;
                    if (dr[9].ToString() == "Administrator")
                    {
                        r = Roles.Administrator;
                    }
                    else if (dr[9].ToString() == "Manager")
                    {
                        r = Roles.Manager;
                    }
                    else if (dr[9].ToString() == "DepotWorker")
                    {
                        r = Roles.DepotWorker;
                    }
                    Person g = new Person(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[3]), dr[4].ToString(), Convert.ToInt32(dr[5]), dr[6].ToString(), dr[7].ToString(), Convert.ToDouble(dr[8]), r); // has to specify the order like this
                    people.Add(g);
                }
            }
            finally
            {
                conn.Close();
            }
            return people;
        }

        // a method to modify the data
        public void ModifyData(int id, string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, string roles)
        {
            bool personExist = false;
            try
            {
                foreach (Person item in people) // to check if the Person with the same name already exists
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
        public Person ReturnPersonFromList(int id)
        {
            Person foundPerson = null;
            foreach (Person item in people)
            {
                if (item.Id == id)
                {
                    foundPerson = item;
                }
            }
            return foundPerson;
        }

        /* LOGIN */
        /* Check users credentials */
        public bool CheckCredentials(string email, string password)
        {
            string sql = $"SELECT firstName, lastName, email, password FROM person WHERE email = @email AND password = @password";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            // Parameters
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();

            bool areCredentialsCorrect = false;
            if (dr.Read())
            {
                // Save current user's name
                //SaveCurrentUser(dr[0].ToString() + " " + dr[1].ToString());
                areCredentialsCorrect = true;
            }
            else
            {
                areCredentialsCorrect = false;
            }
            conn.Close();
            return areCredentialsCorrect;
        }

        /* Get User Type */
        public string GetUserType(string email)
        {
            string role = "";
            try
            {
                using (conn)
                {
                    // Get user role
                    string sql = $"SELECT role FROM person WHERE email = @email AND role != 'Employee'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    // Parameters
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();

                    Object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        role = result.ToString();
                    }
                    else
                    {
                        role = "User does not exist";
                    }
                    return role;
                }
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        /* LOGOUT */



        /* STATISTICS */
        public ArrayList GetStatistics(string dateFrom, string dateTo, string type)
        {
            // Salary per employee between two dates
            if (type == "Salary per employee")
            {
                try
                {

                    using (conn)
                    {
                        string sql = "SELECT p.firstName, p.lastName, p.hourlyWage * Count(s.date) * 4 FROM schedule s INNER JOIN person p ON p.id = s.employeeId WHERE date BETWEEN @dateFrom AND @dateTo GROUP BY s.employeeId";

                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();

                        // Parameters
                        cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                        cmd.Parameters.AddWithValue("@dateTo", dateTo);

                        ArrayList statistics = new ArrayList();

                        MySqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            object[] values = new object[dr.FieldCount];
                            dr.GetValues(values);
                            statistics.Add(values);
                        }

                        return statistics;
                    }
                }
                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            // Number employees per shift between two dates
            else if (type == "Number of employees per shift")
            {
                try
                {
                    using (conn)
                    {
                        string sql = "SELECT COUNT(*) AS nrEmployees, date, shiftType FROM schedule WHERE date BETWEEN @dateFrom AND @dateTo GROUP BY date, shiftType ORDER BY date;";
                        // Create command object
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        // Parameters

                        cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                        cmd.Parameters.AddWithValue("@dateTo", dateTo);
                        // Open db connection
                        conn.Open();

                        ArrayList statistics = new ArrayList();

                        MySqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            object[] values = new object[dr.FieldCount];
                            dr.GetValues(values);
                            statistics.Add(values);
                        }

                        return statistics;

                    }
                    //return statistics;
                    //}
                }
                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return null;
        }


        public ArrayList GetStatistics(string type)
        {
            // Hourly wage per employee
            if (type == "Hourly wage per employee")
            {
                try
                {
                    using (conn)
                    {
                        string sql = "SELECT firstName, lastName, hourlyWage FROM person";

                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();

                        ArrayList statistics = new ArrayList();

                        MySqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            object[] values = new object[dr.FieldCount];
                            dr.GetValues(values);
                            statistics.Add(values);
                        }

                        return statistics;
                    }
                }
                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            return null;
        }



        /* RESET PASSWORD */
        public string ResetPassword(string email, string password)
        {
            try
            {
                using (conn)
                {
                    string sql = "UPDATE person SET password= @password WHERE email = @email;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    // Parameters
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    return "Password was successfully updated";
                }
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /* Get user name */
        public string GetUserName(string email)
        {
            try
            {
                using (conn)
                {
                    // Get user name
                    string sql = $"SELECT firstName FROM person WHERE email = @email";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    // Parameters
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    string username = "";

                    if (result != null)
                    {
                        username = result.ToString();
                    }
                    return username;
                }
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Check if user exists
        public string DoesUserExist(string email)
        {
            try
            {
                using (conn)
                {
                    // Get user name
                    string sql = "SELECT email FROM person WHERE email = @email && role != 'Employee'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    // Parameters
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    // if result is empty so user doesn't exists
                    if (result != null)
                    {
                        return "User found";
                    }
                    else
                    {
                        return "User not found";
                    }
                }
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// PRODUCT
        /// </summary>
        /// <returns></returns>
        /// 
        //to get the departments
        public List<string> GetDepartments()
        {

            try
            {
                string sql = "SELECT name FROM department"; // a query of what we want
                MySqlCommand cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection

                conn.Open();  // this must be before the execution which is just under this
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    departments.Add(dr[0].ToString());
                }
            }
            finally
            {
                conn.Close();
            }
            return departments;
        }


        // to add the products

        public void AddProduct(int departmentId, string name, double price)
        {
            bool productExist = false;
            try
            {
                foreach (Product item in products) // to check if the Person with the same name already exists
                {
                    if (item.Name == name)
                    {
                        productExist = true;
                    }
                }
                if (!productExist)
                {
                    string sql = "INSERT INTO product(departmentId, productName, price) VALUES(@departmentId, @productName, @productPrice)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (departmentId == 0 || name == "" || price == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("None of the above requirements should be empty");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@departmentId", departmentId);
                        cmd.Parameters.AddWithValue("@productName", name);
                        cmd.Parameters.AddWithValue("@productPrice", price);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("Product has been Added to the System");
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Product Exists");
                }
            }

            finally
            {
                conn.Close();
            }
        }

        public List<Product> GetProducts()
        {
            products = new List<Product>();
            try
            {
                string sql = "SELECT productId, departmentId, productName, price FROM product"; // a query of what we want
                MySqlCommand cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection

                conn.Open();  // this must be before the execution which is just under this
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string department = "";

                    if (Convert.ToInt32(dr[1]) == 1)
                    {
                        department = "Household";
                    }
                    else if (Convert.ToInt32(dr[1]) == 2)
                    {
                        department = "Computer";
                    }
                    else if (Convert.ToInt32(dr[1]) == 3)
                    {
                        department = "Kitchen";
                    }
                    else if (Convert.ToInt32(dr[1]) == 4)
                    {
                        department = "Photo and Video";
                    }
                    Product g = new Product(Convert.ToInt32(dr[0]), dr[2].ToString(), Convert.ToDouble(dr[3]), department); // has to specify the order like this
                    products.Add(g);
                }
            }
            finally
            {
                conn.Close();
            }
            return products;
        }

        public void ModifyProduct(int id, string givenProductName, double givenProductPrice)
        {
            try
            {
                string sql = "UPDATE product SET productName = @productName, price = @productPrice WHERE productId ='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (givenProductName == "" || givenProductPrice == 0)
                {
                    System.Windows.Forms.MessageBox.Show("None of the above requirements should be empty");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@productName", givenProductName);
                    cmd.Parameters.AddWithValue("@productPrice", givenProductPrice);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("The information has been updated");
                }

            }

            finally
            {
                conn.Close();
            }
        }

        public Product ReturnExistingProduct(int id)
        {
            Product foundProduct = null;
            foreach (Product item in products)
            {
                if (item.ProductId == id)
                {
                    foundProduct = item;
                }
            }
            return foundProduct;
        }
        public void ProductToRemove(int productId)
        {
            string sql = "";
            bool fkStock = false;
            bool fkProductDelete = false;
            try
            {
                MySqlCommand cmd;
                // to remove the data first from schedule otherwise becuase of the foreing key. Otherwise it wont work. First the data from the child has to be removed
                if (sql != "DELETE FROM stock WHERE productId = @id")
                {
                    sql = "DELETE FROM stock WHERE productId = @id";
                    cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection
                    cmd.Parameters.AddWithValue("@id", productId);
                    conn.Open();  // this must be before the execution which is just under this
                    cmd.ExecuteNonQuery();
                    fkStock = true;
                }
                if (fkStock)
                {
                    sql = "DELETE FROM stock_request WHERE productId = @id";
                    cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.ExecuteNonQuery();

                    fkProductDelete = true;
                }
                if (fkProductDelete) // removing the data from the main table
                {
                    sql = "DELETE FROM product WHERE productId = @id"; // a query of what we want
                    cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }
        }
        public Product GetProductByName(string givenName)
        {
            Product foundProduct = null;
            foreach (Product p in products)
            {
                if (p.Name.Contains(givenName))
                {
                    foundProduct = p;
                }
            }
            return foundProduct;
        }

    }
}
