using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace MediaBazar
{
    public partial class ForgetPasswordForm : Form
    {
        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;

        private string randomCode;
        private string to;
        public ForgetPasswordForm()
        {
            InitializeComponent();
        }

        /* Send email with code to user to reset password */
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            // If no email was entered
            if (string.IsNullOrEmpty(tbxEmail.Text))
            {
                MessageBox.Show("Please fill in a valid e-mail address");
            }
            else
            {
                //string userExistsResult = (mediaBazaar.DoesUserExist(tbxEmail.Text));
                // For testing purposes
                string userExistsResult = (mediaBazaar.DoesUserExist("CheyenneConway@mediabazaar.com"));
                // If user doesn't exist
                if (userExistsResult == "User not found")
                {
                    MessageBox.Show("Email does not exist");
                }
                else if(userExistsResult == "User found")
                {
                    // Generate random code
                    Random random = new Random();
                    randomCode = (random.Next(999999)).ToString();

                    // Send Reset code
                    string resetResult = mediaBazaar.SendResetCode(tbxEmail.Text, randomCode);

                    MessageBox.Show(resetResult);
                }
                else
                {
                    MessageBox.Show(userExistsResult);
                }
            }
        }

        /* Verify reset password code */
        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            string enteredCode = tbxCode.Text;
            if (String.IsNullOrWhiteSpace(enteredCode))
            {
                MessageBox.Show("You need to fill in the reset code");
            }
            else if(randomCode == enteredCode)
            {
                // Pass user email to reset password form (used to update his/her acccount)
                //to = tbxEmail.Text;
                
                // For testing purposes
                to = "CheyenneConway@mediabazaar.com";
                ResetPasswordForm resetPasswordForm = new ResetPasswordForm(to);
                resetPasswordForm.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Code is incorrect");
            }
        }
    }
}
