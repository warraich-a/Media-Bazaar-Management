namespace MediaBazar
{
    partial class DepotWorkerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.lvStock = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.lvProductList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label22 = new System.Windows.Forms.Label();
            this.tbProductName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbProductQuantity = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnSellPrd = new System.Windows.Forms.Button();
            this.cbStockItm = new System.Windows.Forms.ComboBox();
            this.tbAmountItems = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.metroTabControl1.CustomBackground = true;
            this.metroTabControl1.ItemSize = new System.Drawing.Size(510, 40);
            this.metroTabControl1.Location = new System.Drawing.Point(-2, 100);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(1024, 656);
            this.metroTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroTabControl1.TabIndex = 3;
            this.metroTabControl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTabControl1.UseStyleColors = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.label2);
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.Controls.Add(this.tbAmountItems);
            this.metroTabPage1.Controls.Add(this.cbStockItm);
            this.metroTabPage1.Controls.Add(this.btnSellPrd);
            this.metroTabPage1.Controls.Add(this.lvStock);
            this.metroTabPage1.CustomBackground = true;
            this.metroTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 44);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1016, 608);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = " Stock";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // lvStock
            // 
            this.lvStock.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8});
            this.lvStock.GridLines = true;
            this.lvStock.HideSelection = false;
            this.lvStock.Location = new System.Drawing.Point(30, 60);
            this.lvStock.Name = "lvStock";
            this.lvStock.Size = new System.Drawing.Size(560, 456);
            this.lvStock.TabIndex = 12;
            this.lvStock.UseCompatibleStateImageBehavior = false;
            this.lvStock.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Id";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            this.columnHeader6.Width = 200;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Quantity";
            this.columnHeader8.Width = 150;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.lvProductList);
            this.metroTabPage2.Controls.Add(this.label22);
            this.metroTabPage2.Controls.Add(this.tbProductName);
            this.metroTabPage2.Controls.Add(this.label14);
            this.metroTabPage2.Controls.Add(this.tbProductQuantity);
            this.metroTabPage2.Controls.Add(this.button1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 44);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1016, 608);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Products";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // lvProductList
            // 
            this.lvProductList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader26,
            this.columnHeader27,
            this.columnHeader29,
            this.columnHeader4});
            this.lvProductList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvProductList.GridLines = true;
            this.lvProductList.HideSelection = false;
            this.lvProductList.Location = new System.Drawing.Point(39, 95);
            this.lvProductList.Name = "lvProductList";
            this.lvProductList.Size = new System.Drawing.Size(955, 389);
            this.lvProductList.TabIndex = 76;
            this.lvProductList.UseCompatibleStateImageBehavior = false;
            this.lvProductList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "Category";
            this.columnHeader26.Width = 129;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "Product Name";
            this.columnHeader27.Width = 184;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "Price";
            this.columnHeader29.Width = 115;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            this.columnHeader4.Width = 138;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.Control;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(31, 20);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(182, 20);
            this.label22.TabIndex = 73;
            this.label22.Text = "Search product by name";
            // 
            // tbProductName
            // 
            this.tbProductName.Location = new System.Drawing.Point(35, 52);
            this.tbProductName.Name = "tbProductName";
            this.tbProductName.Size = new System.Drawing.Size(304, 20);
            this.tbProductName.TabIndex = 72;
            this.tbProductName.TextChanged += new System.EventHandler(this.tbProductName_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 510);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 71;
            this.label14.Text = "Quantity";
            // 
            // tbProductQuantity
            // 
            this.tbProductQuantity.Location = new System.Drawing.Point(130, 504);
            this.tbProductQuantity.Name = "tbProductQuantity";
            this.tbProductQuantity.Size = new System.Drawing.Size(209, 20);
            this.tbProductQuantity.TabIndex = 70;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(399, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 32);
            this.button1.TabIndex = 69;
            this.button1.Text = "Send Restock Request";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLogout.Location = new System.Drawing.Point(847, 36);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(145, 36);
            this.btnLogout.TabIndex = 48;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MediaBazar.Properties.Resources.linkedin_banner_image_1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.lblUsername.Location = new System.Drawing.Point(292, 58);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(97, 24);
            this.lblUsername.TabIndex = 46;
            this.lblUsername.Text = "Username";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::MediaBazar.Properties.Resources.user;
            this.pictureBox4.Location = new System.Drawing.Point(242, 52);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(29, 30);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 45;
            this.pictureBox4.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.label19.Location = new System.Drawing.Point(292, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(234, 24);
            this.label19.TabIndex = 54;
            this.label19.Text = "Logged in as depot worker";
            // 
            // btnSellPrd
            // 
            this.btnSellPrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnSellPrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSellPrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSellPrd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSellPrd.Location = new System.Drawing.Point(716, 400);
            this.btnSellPrd.Name = "btnSellPrd";
            this.btnSellPrd.Size = new System.Drawing.Size(193, 32);
            this.btnSellPrd.TabIndex = 70;
            this.btnSellPrd.Text = "Sell Product";
            this.btnSellPrd.UseVisualStyleBackColor = false;
            this.btnSellPrd.Click += new System.EventHandler(this.btnSellPrd_Click);
            // 
            // cbStockItm
            // 
            this.cbStockItm.FormattingEnabled = true;
            this.cbStockItm.Location = new System.Drawing.Point(661, 149);
            this.cbStockItm.Name = "cbStockItm";
            this.cbStockItm.Size = new System.Drawing.Size(316, 28);
            this.cbStockItm.TabIndex = 71;
            this.cbStockItm.Click += new System.EventHandler(this.cbStockItm_Click);
            // 
            // tbAmountItems
            // 
            this.tbAmountItems.Location = new System.Drawing.Point(661, 278);
            this.tbAmountItems.Name = "tbAmountItems";
            this.tbAmountItems.Size = new System.Drawing.Size(316, 26);
            this.tbAmountItems.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(659, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 73;
            this.label1.Text = "Select a product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(659, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 74;
            this.label2.Text = "Choose an amount";
            // 
            // DepotWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(213)))), ((int)(((byte)(203)))));
            this.ClientSize = new System.Drawing.Size(1023, 702);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.lblUsername);
            this.Name = "DepotWorkerForm";
            this.Text = "Depot Worker";
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.ListView lvStock;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label19;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.ListView lvProductList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbProductName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbProductQuantity;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbAmountItems;
        private System.Windows.Forms.ComboBox cbStockItm;
        private System.Windows.Forms.Button btnSellPrd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}