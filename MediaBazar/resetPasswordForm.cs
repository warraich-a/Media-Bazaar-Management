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
    public partial class ResetPasswordForm : Form
    {
        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;

        private string email;
        public ResetPasswordForm(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = tbxNewPassword.Text;
            string repeatPassword = tbxRepeatPassword.Text;
            // If passwords match
            if (newPassword == repeatPassword)
            {
                mediaBazaar.ResetPassword(email, newPassword);
                MessageBox.Show("Password was successfully updated");

                // Open login
                LogInForm loginForm = new LogInForm();
                loginForm.Show();

                this.Close();
            }
            // Otherwise
            else
            {
                MessageBox.Show("Passwords don't match");
            }
        }
    }
}
