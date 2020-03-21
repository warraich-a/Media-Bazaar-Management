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
    public partial class AdministratorForm : Form
    {
        MediaBazaar mediaBazaar = new MediaBazaar();
        ListViewItem list;
        public AdministratorForm()
        {
            InitializeComponent();
            RefreshData();
        }
        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {

            Modify_data Add = new Modify_data(mediaBazaar, this);
            Add.Show();
        }

        // to show the data in a list view item
        public void RefreshData()
        {
            listView1.Items.Clear();
            foreach (Person item in mediaBazaar.ReturnPeopleFromDB())
            {
                list = new ListViewItem(Convert.ToString(item.Id));
                list.SubItems.Add(item.FirstName);
                list.SubItems.Add(item.LastName);
                list.SubItems.Add(item.GetEmail);
                list.SubItems.Add(Convert.ToString(item.DateOfBirth));
                list.SubItems.Add(item.StreetName);
                list.SubItems.Add(Convert.ToString(item.HouseNr));
                list.SubItems.Add(item.Zipcode);
                list.SubItems.Add(item.City);
                list.SubItems.Add(Convert.ToString(item.HourlyWage));
                list.SubItems.Add(Convert.ToString(item.Role));
                listView1.Items.Add(list);
            }
        }

        // To remove an employee from the system
        private void btnRemoveEmp_Click(object sender, EventArgs e)
        {
           try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                if (MessageBox.Show("Do you want to remove this person?", "Remove Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mediaBazaar.RemovePerson(Convert.ToInt32(id));
                    MessageBox.Show("Person is removed");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Person is not removed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No employee is selected");
            }
        }
        private void btnModifyStack_Click(object sender, EventArgs e)
        {

        }

        // to modify a selected person's data
        private void btnModifyEmp_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                Modify_data m = new Modify_data(id, this); // sending the id to modify data form through the parameters
                m.Show();
            }
            catch (Exception)
            {

                MessageBox.Show("No employee is selected");
            }
                  
        }
    }
}
