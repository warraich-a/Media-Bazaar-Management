namespace MediaBazar
{
    partial class ModifyProduct
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
            this.tbProductPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbProductName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnModifyProduct = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbProductPrice
            // 
            this.tbProductPrice.Location = new System.Drawing.Point(141, 67);
            this.tbProductPrice.Name = "tbProductPrice";
            this.tbProductPrice.Size = new System.Drawing.Size(281, 20);
            this.tbProductPrice.TabIndex = 103;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(22, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 102;
            this.label7.Text = "Product Price";
            // 
            // tbProductName
            // 
            this.tbProductName.Location = new System.Drawing.Point(141, 20);
            this.tbProductName.Name = "tbProductName";
            this.tbProductName.Size = new System.Drawing.Size(281, 20);
            this.tbProductName.TabIndex = 101;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Product Name";
            // 
            // btnModifyProduct
            // 
            this.btnModifyProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnModifyProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyProduct.ForeColor = System.Drawing.SystemColors.Control;
            this.btnModifyProduct.Location = new System.Drawing.Point(141, 110);
            this.btnModifyProduct.Name = "btnModifyProduct";
            this.btnModifyProduct.Size = new System.Drawing.Size(281, 32);
            this.btnModifyProduct.TabIndex = 86;
            this.btnModifyProduct.Text = "Update";
            this.btnModifyProduct.UseVisualStyleBackColor = false;
            this.btnModifyProduct.Click += new System.EventHandler(this.btnModifyProduct_Click);
            // 
            // ModifyProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 173);
            this.Controls.Add(this.tbProductPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbProductName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModifyProduct);
            this.Name = "ModifyProduct";
            this.Text = "ModifyProduct";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbProductPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbProductName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnModifyProduct;
    }
}