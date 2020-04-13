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
    public partial class ManagerForm : Form
    {
<<<<<<< HEAD

        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
=======
        MediaBazaar mediaBazaar = new MediaBazaar();
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
        ListViewItem listB;
        public ManagerForm()
        {
            InitializeComponent();
<<<<<<< HEAD

            // Add user name
            lblUsername.Text = mediaBazaar.CurrentUser;

            RefreshData();

=======
            RefreshData();
>>>>>>> 92028dc98b336a33c170e6b273dd3288b14d4af9
        }

        private void lvEmployeesManager_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {

        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       
        public void RefreshData()
        {

            LV2.Items.Clear();
            foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
            {
                listB = new ListViewItem(Convert.ToString(item.Id));
                listB.SubItems.Add(item.FirstName);
                listB.SubItems.Add(item.LastName);
                listB.SubItems.Add(item.GetEmail);
                listB.SubItems.Add(Convert.ToString(item.DateOfBirth));
                listB.SubItems.Add(item.StreetName);
                listB.SubItems.Add(Convert.ToString(item.HouseNr));
                listB.SubItems.Add(item.Zipcode);
                listB.SubItems.Add(item.City);
                listB.SubItems.Add(Convert.ToString(item.HourlyWage));
                listB.SubItems.Add(Convert.ToString(item.Role));
                LV2.Items.Add(listB);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AdministratorForm a = new AdministratorForm();
            a.Show();
        }

        private void btnShowEmp_Click(object sender, EventArgs e)
        {
            string name = tbEmpNameToFind.Text;
            MessageBox.Show($"{mediaBazaar.foundedPerson(name).ToString()}");
           
        }

        private void btnShowEmp_Click(object sender, EventArgs e)
        {
            string name = tbEmpNameToFind.Text;
            MessageBox.Show($"{mediaBazaar.foundedPerson(name).ToString()}");
        }

        public void RefreshData()
        {

            LV2.Items.Clear();
            foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
            {
                listB = new ListViewItem(Convert.ToString(item.Id));
                listB.SubItems.Add(item.FirstName);
                listB.SubItems.Add(item.LastName);
                listB.SubItems.Add(item.GetEmail);
                listB.SubItems.Add(Convert.ToString(item.DateOfBirth));
                listB.SubItems.Add(item.StreetName);
                listB.SubItems.Add(Convert.ToString(item.HouseNr));
                listB.SubItems.Add(item.Zipcode);
                listB.SubItems.Add(item.City);
                listB.SubItems.Add(Convert.ToString(item.HourlyWage));
                listB.SubItems.Add(Convert.ToString(item.Role));
                LV2.Items.Add(listB);
            }
        }
    }
}
