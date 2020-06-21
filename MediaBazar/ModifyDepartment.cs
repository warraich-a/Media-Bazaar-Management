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
    public partial class ModifyDepartment : Form
    {
        int id;
        AdministratorForm form;
        MediaBazaar mediaBazaar;
        public ModifyDepartment(int givenId, AdministratorForm f, MediaBazaar mediaBazaar)
        {
            InitializeComponent();
            form = f;
            id = givenId;
            this.mediaBazaar = mediaBazaar;
            FoundDepartment(id);
        }

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            if (cbManager.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Are you sure", "Update Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        string depName = tbDName.Text;
                        int minEmp = Convert.ToInt32(tbMinEmp.Text);
                        mediaBazaar.ModifyDepartment(id, depName, mediaBazaar.GetPersonIdByName(cbManager.SelectedItem.ToString()), minEmp);
                        this.Close();
                        form.Show();
                        form.RefreshData();


                    }
                    else
                    {
                        MessageBox.Show("Information is not updated");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Have you added everything?");
                }
            } else
            {
                MessageBox.Show("Select a manager first");
            }
        }
        public void FoundDepartment(int id)
        {
            try
            {
                Department dep = null;
                mediaBazaar.ReadDepartment();
                foreach (Department d in mediaBazaar.GetDepartmentsList())
                {
                    if (id == d.Id)
                    {
                        dep = d;
                    }
                }
                tbDName.Text = dep.Name;
                tbMinEmp.Text = dep.MinEmp.ToString();

                mediaBazaar.ReadPersons();
                foreach (Person p in mediaBazaar.GetManagersList())
                {

                    if (p.DepartmentId == 0)
                    {

                        cbManager.Items.Add(p.GetFullName());
                    }
                }
                foreach (Person p in mediaBazaar.GetManagersList())
                {

                    if (p.DepartmentId == id)
                    {

                        cbManager.Items.Add(p.GetFullName());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
