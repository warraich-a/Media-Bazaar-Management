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
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }


        private void btnLogIn_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
           /* AdministratorForm a = new AdministratorForm();
            a.Show();*/

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
                if (mediaBazaar.GetUserType(email) == "Manager")
                {
                    if (mediaBazaar.CheckCredentials(email, password))
                    {
                        ManagerForm managerForm = new ManagerForm();

                        managerForm.ShowDialog();

                        this.Hide();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credentials are wrong");
                    }
                }
                else if (mediaBazaar.GetUserType(email) == "Administrator")
                {
                    if (mediaBazaar.CheckCredentials(email, password))
                    {
                        AdministratorForm adminForm = new AdministratorForm();

                        adminForm.ShowDialog();
                        this.Hide();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credentials are wrong");
                    }
                }
                else if (mediaBazaar.GetUserType(email) == "DepotWorker")
                {
                    if (mediaBazaar.CheckCredentials(email, password))
                    {
                        DepotWorkerForm depotWorkerForm = new DepotWorkerForm();

                        depotWorkerForm.ShowDialog();
                        this.Hide();
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
=======
            ManagerForm a = new ManagerForm();
            a.Show();
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {

        }
    }
}
