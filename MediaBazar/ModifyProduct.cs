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
    public partial class ModifyProduct : Form
    {
        int id;
        AdministratorForm form;
        MediaBazaar mediaBazaar;
        public ModifyProduct(int givenId, AdministratorForm f, MediaBazaar mediaBazaar)
        {
            InitializeComponent();
            form = f;
            id = givenId;
            this.mediaBazaar = mediaBazaar;
            FoundProduct(id);
        }

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure", "Update Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string productNewName = tbProductName.Text;
                    double productNewPrice = Convert.ToDouble(tbProductPrice.Text);
                    double sellingPrice = Convert.ToDouble(tbSellingPrice.Text);
                    mediaBazaar.ModifyProduct(this.id, productNewName, productNewPrice, sellingPrice);
                    this.Close();
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
        }

        public void FoundProduct(int id)
        {
            try
            {
                Product foundProduct = mediaBazaar.ReturnExistingProduct(id); // to give the correct id through parameters
                tbProductName.Text = foundProduct.Name;
                tbProductPrice.Text = foundProduct.Price.ToString();
                tbSellingPrice.Text = foundProduct.SellingPrice.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifyProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
