﻿using System;
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
    public partial class LogInForm : Form
    {
        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        public LogInForm()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            // Get login details
            string email = tbxEmail.Text;
            string password = tbxPassword.Text;

            // if email is empty
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in your e-mail address");
            }
            // if password is empty
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in your password");
            }
            else
            {
                // If manager
                if (mediaBazaar.GetUserType(email) == "Manager")
                {
                    if (mediaBazaar.CheckCredentials(email, password))
                    {
                        this.Hide();
                        ManagerForm managerForm = new ManagerForm();

                        managerForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credentials are wrong");
                    }
                }
                // If admin
                else if (mediaBazaar.GetUserType(email) == "Administrator")
                {
                    if (mediaBazaar.CheckCredentials(email, password))
                    {
                        this.Hide();
                        AdministratorForm adminForm = new AdministratorForm();

                        adminForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credentials are wrong");
                    }
                }
                // If depot worker
                else if (mediaBazaar.GetUserType(email) == "DepotWorker")
                {
                    if (mediaBazaar.CheckCredentials(email, password))
                    {
                        this.Hide();
                        DepotWorkerForm depotWorkerForm = new DepotWorkerForm();

                        depotWorkerForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credentials are wrong");
                    }
                }
                else
                {
                    MessageBox.Show(mediaBazaar.GetUserType(email));
                }
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            this.Hide();

            ForgetPasswordForm forgetPasswordForm = new ForgetPasswordForm();

            forgetPasswordForm.ShowDialog();

            this.Close();
        }
    }
}
