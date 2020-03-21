using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace MediaBazar
{
   public  class MediaBazaar
    {
        private static MediaBazaar instance = null;
        // Current user
        private string currentUser;

        List<Person> people = new List<Person>();
        List<Schedule> schedules = new List<Schedule>();

        Person person = new Person();
        string connectionString = "Server=studmysql01.fhict.local;Uid=dbi435688;Database=dbi435688;Pwd=webhosting54;";
        MySqlConnection conn;

        public string CurrentUser
        {
            get { return this.currentUser; }
        }

        /* Reset code variables */
        private string to;

        public static MediaBazaar Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MediaBazaar();
                }
                return instance;
            }
        }


        /* Login */
        /* Get User Type */
        public string GetUserType(string email)
        {
            string role = "";
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
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

        /* Check users credentials */
        public bool CheckCredentials(string email, string password)
        {
            // Create connection string to db
            MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");
 
            string sql = $"SELECT firstName, lastName, email, password FROM person WHERE email = @email AND password = @password";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            // Parameters
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();

            bool areCredentialsCorrect  = false;
            if (dr.Read())
            {
                // Save current user's name
                SaveCurrentUser(dr[0].ToString() + " " +  dr[1].ToString());
                areCredentialsCorrect = true;
            }
            else
            {
                areCredentialsCorrect = false;
            }
            conn.Close();
            return areCredentialsCorrect;
        }

        private void SaveCurrentUser(string name)
        {
            this.currentUser = name;
            Console.WriteLine(name);
        }

        /* Logout */
        public void LogOut()
        {
            this.currentUser = null;
        }

        /* Reset password */
        public string ResetPassword(string email, string password)
        {
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
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

        /* Send reset code */
        public string SendResetCode(string email, string randomCode)
        {
            string from, password, messageBody;

            // Send code per email
            MailMessage message = new MailMessage();

            // Since email's of user isn't real, I'm using my own email
            //to = "rawan.ad7@gmail.com";
            // For testing purposes
            to = email;
            // Email sender
            from = "mediabazaar2@gmail.com";
            password = "Sendcode43";
            //messageBody = $"<h4>Hello {GetUserName(email)},</h4> <p> You recently requested to reset your password for your Media Bazaar account. <p> Here is your reset code {randomCode}</p> <p>If you did not request a password reset, please ignore this email or reply to let us know.</p> <p> Best Regards, </p> <p>Media Bazaar</p>";
            // For testing purposes
            messageBody = $"<h4>Hello {GetUserName("CheyenneConway@mediabazaar.com")},</h4> <p> You recently requested to reset your password for your Media Bazaar account. <p> Here is your reset code {randomCode}</p> <p>If you did not request a password reset, please ignore this email or reply to let us know.</p> <p> Best Regards, </p> <p>Media Bazaar</p>";

            message.To.Add(to);
            message.From = new MailAddress(from);
            message.IsBodyHtml = true;
            message.Body = messageBody;


            message.Subject = "Password resetting code";
            // Creating the smtp object
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);
            // Send email
            try
            {
                smtp.Send(message);
                return "code sent successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /* Get user name */
        public string GetUserName(string email)
        {
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
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

        public string DoesUserExist(string email)
        {
            string connStr = "server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
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


        public MediaBazaar()
        {
            conn = new MySqlConnection(connectionString);
        }
        // to add a person in a database
        public void AddPerson(string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, string roles)
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
        public void RemovePerson(int personId)
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
        public Person foundedPerson(string givenName)
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


        public List<Person> GetPeople()
        {
            return this.people;
         }

        public List<Schedule> VeiwSchedule()
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
        public String GetPersonNameById(int id)
        {
            string s = "";
            foreach (Person p in people)
            {
                if (id == p.Id)
                {
                    s = p.FirstName + " " + p.LastName;
                }
            }
            return s;
        }
        public List<Person> GetPeopleList()
        {
            return this.people;
        }
        public List<Schedule> VeiwSpecificSchedule1(int id, DateTime date)
        {
            List<Schedule> newSchedule = new List<Schedule>();
            try
            {
                string sql = "SELECT `id`, `employeeId`, `shiftType`, `date`, `statusOfShift` FROM `schedule` WHERE employeeId = @id AND date = @date";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date", date.Date);

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
                    newSchedule.Add(g);
                }
            }
            finally
            {
                conn.Close();
            }
            return newSchedule;
        }
        public List<Schedule> VeiwSpecificSchedule2(DateTime date, string shift)
        {
            List<Schedule> newSchedule = new List<Schedule>();
            try
            {
                string sql = "SELECT `id`, `employeeId`, `shiftType`, `date`, `statusOfShift` FROM `schedule` WHERE date = @date AND shiftType = @shift";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();

                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Parameters.AddWithValue("@shift", shift);

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
                    newSchedule.Add(g);
                }
            }
            finally
            {
                conn.Close();
            }
            return newSchedule;
        }
        public void RemoveSchedule(int id)
        {
            try
            {
                string sql = "DELETE FROM schedule WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                int dr = cmd.ExecuteNonQuery();


            }
            finally
            {
                conn.Close();
            }

        }

        public int GetPersonIdByName(string name)
        {
            int i = 0;
            foreach (Person p in people)
            {
                if (p.GetFullName() == name)
                {
                    i = p.Id;
                }
            }
            return i;
        }

        public int GetSchedule(string name, string date, string type)
        {
            int i = 0;
            Shift shift = Shift.MORNING;
            if (type == "AFTERNOON")
            {
                shift = Shift.AFTERNOON;
            }
            else if (type == "EVENING")
            {
                shift = Shift.EVENING;
            }
            else if (type == "MORNING")
            {
                shift = Shift.MORNING;
            }
            int c = GetPersonIdByName(name);
            foreach (Schedule s in schedules)
            {

                if (s.EmployeeId == c && s.DATETime == Convert.ToDateTime(date).Date && s.ShiftType == shift)
                {
                    i = s.SheduleId;
                }
            }
            return i;
        }


    }
}
