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
           // Generate random code
           Random random = new Random();
           randomCode = (random.Next(999999)).ToString();

            // Send Reset code
            string result = mediaBazaar.SendResetCode(tbxEmail.Text, randomCode);

            MessageBox.Show(result);
        }

        /* Verify reset password code */
        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            string enteredCode = tbxCode.Text;
            if(randomCode == enteredCode)
            {
                // Pass user email to reset password form (used to update his/her acccount)
                to = tbxEmail.Text;
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
