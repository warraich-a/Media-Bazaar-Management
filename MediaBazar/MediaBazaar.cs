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
    class MediaBazaar
    {
        //List<Person> people = new List<Person>();
        //List<Schedule> schedules = new List<Schedule>();
        private static MediaBazaar instance = null;
        // Current user
        private string currentUser;

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

        // Statistics
        // Employees names and age
        //public void GetStatistics()
        //{
        //    MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");

        //    string sql = "SELECT name, TIMESTAMPDIFF(YEAR, dateOfBirth, CURDATE()) AS age FROM person ORDER BY email;";
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    conn.Open();

        //    //Object result = cmd.ExecuteScalar();
        //    MySqlDataReader dr = cmd.ExecuteReader();

        //    List<string> users = new List<string>();
        //    while (dr.Read())
        //    {
        //        users.Add((dr[0].ToString()));
        //        //listBox1.Items.Add((dr[0].ToString()));
        //        //this.chart1.Series["Age"].Points.AddXY(dr[0].ToString(), Convert.ToInt32(dr[1]));
        //    }
        //    conn.Close();
        //}


        /* Login */
        /* Get User Type */
        public string GetUserType(string email)
        {
            // Create connection string to db
            MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");
            // Get user role
            string sql = $"SELECT role FROM person WHERE email = '{email}' AND role != 'Employee'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            Object result = cmd.ExecuteScalar();

            string role = "";
            if (result != null) {
                role = result.ToString();
            }
            else
            {
                role = "No user found";
            }
            conn.Close();
            return role;
        }

        /* Check users credentials */
        public bool CheckCredentials(string email, string password)
        {
            // Create connection string to db
            MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");
            // Get user role
            string sql = $"SELECT firstName, lastName, email, password FROM person WHERE email = '{email}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            //Object result = cmd.ExecuteReader();
            MySqlDataReader dr = cmd.ExecuteReader();




            bool areCredentialsCorrect  = false;
            if (dr.Read())
            {
                // Save current user's name
                SaveCurrentUser(dr[0].ToString() + dr[1].ToString());
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
        // End session user
        public void LogOut()
        {
            this.currentUser = null;
        }

        /* Reset password */
        public void ResetPassword(string email, string password)
        {
            MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");

            string sql = "UPDATE person SET password= @password WHERE email = @email;"; 
            MySqlCommand cmd = new MySqlCommand(sql, conn); 
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }  

        /* Send reset code */
        public string SendResetCode(string email, string randomCode)
        {
            // ************** Check if user email exists
            string from, password, messageBody;

            // Send code per email
            MailMessage message = new MailMessage();

            // Since email's of user isn't real, I'm using my own email
            to = "rawan.ad7@gmail.com";
            from = "mediabazaar2@gmail.com";
            password = "Sendcode43";
            messageBody = $"<h4>Hello {GetUserName(email)},</h4> <p> You recently requested to reset your password for your Media Bazaar account. <p> Here is your reset code {randomCode}</p> <p>If you did not request a password reset, please ignore this email or reply to let us know.</p> <p> Best Regards, </p> <p>Media Bazaar</p>";

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
            // Create connection string to db
            MySqlConnection conn = new MySqlConnection("server=studmysql01.fhict.local;database=dbi435688;uid=dbi435688;password=webhosting54;");
            // Get user role
            string sql = $"SELECT firstName FROM person WHERE email = '{email}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            //Object result = cmd.ExecuteReader();
            MySqlDataReader dr = cmd.ExecuteReader();

            string username = "";
            if (dr.Read())
            {
                // Save current user's name
                 username = (dr[0].ToString());
            }

            conn.Close();
            return username;
        }



    }
}
