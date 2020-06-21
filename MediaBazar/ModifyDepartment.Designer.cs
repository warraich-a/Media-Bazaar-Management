namespace MediaBazar
{
    partial class ModifyDepartment
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
            this.cbManager = new System.Windows.Forms.ComboBox();
            this.tbMinEmp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnModifyProduct = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbManager
            // 
            this.cbManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbManager.FormattingEnabled = true;
            this.cbManager.Location = new System.Drawing.Point(157, 87);
            this.cbManager.Name = "cbManager";
            this.cbManager.Size = new System.Drawing.Size(281, 21);
            this.cbManager.TabIndex = 118;
            // 
            // tbMinEmp
            // 
            this.tbMinEmp.Location = new System.Drawing.Point(157, 140);
            this.tbMinEmp.Name = "tbMinEmp";
            this.tbMinEmp.Size = new System.Drawing.Size(281, 20);
            this.tbMinEmp.TabIndex = 117;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.label2.Location = new System.Drawing.Point(38, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 116;
            this.label2.Text = "Min Emp";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.label7.Location = new System.Drawing.Point(38, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 115;
            this.label7.Text = "Manager";
            // 
            // tbDName
            // 
            this.tbDName.Location = new System.Drawing.Point(157, 37);
            this.tbDName.Name = "tbDName";
            this.tbDName.Size = new System.Drawing.Size(281, 20);
            this.tbDName.TabIndex = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.label1.Location = new System.Drawing.Point(38, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 113;
            this.label1.Text = "Name";
            // 
            // btnModifyProduct
            // 
            this.btnModifyProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnModifyProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyProduct.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyProduct.ForeColor = System.Drawing.SystemColors.Control;
            this.btnModifyProduct.Location = new System.Drawing.Point(157, 192);
            this.btnModifyProduct.Name = "btnModifyProduct";
            this.btnModifyProduct.Size = new System.Drawing.Size(281, 40);
            this.btnModifyProduct.TabIndex = 112;
            this.btnModifyProduct.Text = "Update";
            this.btnModifyProduct.UseVisualStyleBackColor = false;
            this.btnModifyProduct.Click += new System.EventHandler(this.btnModifyProduct_Click);
            // 
            // ModifyDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 269);
            this.Controls.Add(this.cbManager);
            this.Controls.Add(this.tbMinEmp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModifyProduct);
            this.Name = "ModifyDepartment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModifyDepartment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbManager;
        private System.Windows.Forms.TextBox tbMinEmp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnModifyProduct;
    }
}