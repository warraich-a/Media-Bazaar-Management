using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace MediaBazar
{
    public class MediaBazaar :IConvertible<MediaBazaar>
    {
        List<Person> people = new List<Person>();
        List<Schedule> schedules = new List<Schedule>();

        Person person = new Person();
        string connectionString = "Server=studmysql01.fhict.local;Uid=dbi435688;Database=dbi435688;Pwd=webhosting54;";
        MySqlConnection conn;

        public MediaBazaar()
        {
            conn = new MySqlConnection(connectionString);
        }

        // to add a person
        public void AddPerson(string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage)
        {
           
            string givenEmail;
            string newPassword;
            try
            {
                givenEmail = person.Email(givenFirstName, givenSecondName);
                newPassword = person.SetPassword();



                string sql = "INSERT INTO person(firstName, lastName, email, dateOfBirth, streetName, houseNr, city, zipcode, hourlyWage, password, role) VALUES(@firstName, @lastName, @email, @DOB, @streetName, @houseNr, @zipcode, @city, @hourlyWage, @password, @role)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@firstName", givenFirstName);
                cmd.Parameters.AddWithValue("@lastName", givenSecondName);
                cmd.Parameters.AddWithValue("@email", givenEmail);
                cmd.Parameters.AddWithValue("@DOB", givenDOB);
                cmd.Parameters.AddWithValue("@streetName", givenStreetName);
                cmd.Parameters.AddWithValue("@houseNr", givenHouseNr);
                cmd.Parameters.AddWithValue("@zipcode", givenZipcode);
                cmd.Parameters.AddWithValue("@city", givenCity);
                cmd.Parameters.AddWithValue("@role", Roles.EMPLOYEE);
                cmd.Parameters.AddWithValue("@hourlyWage", givenHourlyWage);
                cmd.Parameters.AddWithValue("@password", newPassword);
                
                conn.Open();
                cmd.ExecuteNonQuery();

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
                AdministratorForm f = new AdministratorForm();
                f.Refresh();
                conn.Close();
            }
        }


        public void ModifyPerson(int id, string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, Roles givenRole)
        {
            foreach (Person p in people)
            {
                if (p.Id == id)
                {
                    p.FirstName = givenFirstName;
                    p.LastName = givenSecondName;
                    p.DateOfBirth= givenDOB;
                    p.StreetName = givenStreetName;
                    p.HouseNr = givenHouseNr;
                    p.Zipcode = givenZipcode;
                    p.HourlyWage = givenHourlyWage;
                    p.City = givenCity;
                    p.Role = givenRole;
                }
            }
        }

        public List<Person> ReturnPeopleFromDB()
        {
           
            try
            {
                string sql = "SELECT id, firstName, lastName, dateOfBirth, streetName, houseNr, city, zipcode, hourlyWage FROM person"; // a query of what we want
                MySqlCommand cmd = new MySqlCommand(sql, conn);  // first parameter has to be the query and the second one should be the connection

                conn.Open();  // this must be before the execution which is just under this
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Person g = new Person(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[3]), dr[4].ToString(), Convert.ToInt32(dr[5]), dr[6].ToString(), dr[7].ToString(), Convert.ToDouble(dr[8])); // has to specify the order like this
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
                    if(dr[2].ToString() == "Morning")
                    {
                        a = Shift.MORNING;
                    } else if (dr[2].ToString() == "Afternoon")
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
            string s ="";
            foreach(Person p in people)
            {
                if(id == p.Id)
                {
                    s = p.FirstName + " "+ p.LastName;
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
        public List<Schedule> VeiwSpecificSchedule2( DateTime date, string shift )
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
            foreach(Person p in people)
            {
                if(p.ToString() == name)
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
