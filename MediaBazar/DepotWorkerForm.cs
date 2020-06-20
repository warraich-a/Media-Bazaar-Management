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
    public partial class DepotWorkerForm : Form
    {
        // Create instance of mediaBazaar or use made instance
        MediaBazaar mediaBazaar = MediaBazaar.Instance;
        public DepotWorkerForm()
        {
            InitializeComponent();

            // Add user name
            lblUsername.Text = mediaBazaar.CurrentUser;
            RefreshData();
        }

        public void RefreshData()
        {
            mediaBazaar.ReadStocks();
            mediaBazaar.ReadProducts();
            lvStock.Items.Clear();
            foreach (Stock p in mediaBazaar.GetStockList())
            {
                ListViewItem l = new ListViewItem(p.ProductId.ToString());
                l.SubItems.Add(mediaBazaar.GetProductNameById(p.ProductId));
                l.SubItems.Add(p.Quantity.ToString());

                lvStock.Items.Add(l);
            }

            lvProductList.Items.Clear();
            mediaBazaar.ReadProducts();
            mediaBazaar.ReadDepartment();
            mediaBazaar.ReadStocks();
            foreach (Product p in mediaBazaar.GetProductsList())
            {
                ListViewItem l = new ListViewItem(p.ProductId.ToString());
                l.SubItems.Add(mediaBazaar.GetDepartmentNameById(p.DapartmentId));
                l.SubItems.Add(p.ProductName);
                l.SubItems.Add(p.Price.ToString());
                foreach (Stock s in mediaBazaar.GetStockList())
                {
                    if (s.ProductId == p.ProductId)
                    {
                        l.SubItems.Add(s.Quantity.ToString());
                    }
                }


                lvProductList.Items.Add(l);
            }

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            mediaBazaar.LogOut();
            MessageBox.Show("Logged out successfully");
            this.Hide();
            LogInForm formLogIn = new LogInForm();
            formLogIn.ShowDialog();
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbProductName.Text))
            {
                List<ListViewItem> items = new List<ListViewItem>();
                string productName = tbProductName.Text;
                RefreshData();
                for (int i = 0; i < lvProductList.Items.Count; i++)
                {
                    if (lvProductList.Items[i].SubItems[2].Text.Contains(productName))
                    {
                        items.Add(lvProductList.Items[i]);
                    }
                }
                lvProductList.Items.Clear();
                foreach (ListViewItem lvi in items)
                {
                    lvProductList.Items.Add(lvi);
                }
            }
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            lvProductList.Items.Clear();
            RefreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(lvProductList.SelectedItems.Count > 0)
            {
                if (!String.IsNullOrWhiteSpace(tbProductQuantity.Text) && Convert.ToInt32(tbProductQuantity.Text) > 0)
                {
                    mediaBazaar.SendDepoRequest(Convert.ToInt32(lvProductList.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(tbProductQuantity.Text));
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Incorrect quantity");
                }
            }
            else
            {
                MessageBox.Show("Select the Id");
            }
        }

        private void tbProductName_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(tbProductName.Text))
            {
                    List<ListViewItem> items = new List<ListViewItem>();
                    string productName = tbProductName.Text;
                    RefreshData();
                    for (int i = 0; i < lvProductList.Items.Count; i++)
                    {
                        if (lvProductList.Items[i].SubItems[2].Text.Contains(productName.ToLower()) || lvProductList.Items[i].SubItems[2].Text.Contains(productName.ToUpper()))
                        {
                            items.Add(lvProductList.Items[i]);
                        }
                    }
                    lvProductList.Items.Clear();
                    foreach (ListViewItem lvi in items)
                    {
                        lvProductList.Items.Add(lvi);
                    }
                
            } else
            {
                lvProductList.Items.Clear();
                RefreshData();
            }
        }

        private void cbStockItm_Click(object sender, EventArgs e)
        {
            mediaBazaar.ReadStocks();
            mediaBazaar.ReadProducts();
            cbStockItm.Items.Clear();
            foreach (Stock p in mediaBazaar.GetStockList())
            {
                cbStockItm.Items.Add(mediaBazaar.GetProductNameById(p.ProductId));
            }
        }

        private void btnSellPrd_Click(object sender, EventArgs e)
        {
            if (cbStockItm.SelectedIndex > -1)
            {
                if (!String.IsNullOrEmpty(tbAmountItems.Text))
                {
                    int stockQuantity = 0;
                    int productId = mediaBazaar.GetProductIntByName(cbStockItm.SelectedItem.ToString());
                    foreach (Stock s in mediaBazaar.GetStockList())
                    {
                        if (s.ProductId == productId)
                        {
                            stockQuantity = s.Quantity;
                        }
                    }
                    if (Convert.ToInt32(tbAmountItems.Text) <= stockQuantity)
                    {
                        mediaBazaar.ReadStocks();
                        stockQuantity -= Convert.ToInt32(tbAmountItems.Text);
                        mediaBazaar.SellStockItem(productId, stockQuantity, Convert.ToInt32(tbAmountItems.Text));
                        RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("Amount cannot be bigger than quantity");
                    }
                }
                else MessageBox.Show("Choose an amount");
            }
            else MessageBox.Show("Select a product");
        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
