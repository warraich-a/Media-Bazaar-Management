namespace MediaBazar
{
    partial class ManagerForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.mtbManager = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label21 = new System.Windows.Forms.Label();
            this.btnShowEmp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEmpNameToFind = new System.Windows.Forms.TextBox();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.chartEmployeeStatistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.Label();
            this.cbxCategoryStatistics = new System.Windows.Forms.ComboBox();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbNameOfEmp = new System.Windows.Forms.ComboBox();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.dtpDateShedule = new System.Windows.Forms.DateTimePicker();
            this.btnShowSchedule = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.listView5 = new System.Windows.Forms.ListView();
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label18 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.mtbManager.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmployeeStatistics)).BeginInit();
            this.metroTabPage4.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // mtbManager
            // 
            this.mtbManager.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtbManager.Controls.Add(this.metroTabPage2);
            this.mtbManager.Controls.Add(this.metroTabPage3);
            this.mtbManager.Controls.Add(this.metroTabPage5);
            this.mtbManager.Controls.Add(this.metroTabPage4);
            this.mtbManager.ItemSize = new System.Drawing.Size(263, 40);
            this.mtbManager.Location = new System.Drawing.Point(-4, 104);
            this.mtbManager.Name = "mtbManager";
            this.mtbManager.SelectedIndex = 1;
            this.mtbManager.Size = new System.Drawing.Size(1056, 685);
            this.mtbManager.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.mtbManager.Style = MetroFramework.MetroColorStyle.Silver;
            this.mtbManager.TabIndex = 3;
            this.mtbManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtbManager.UseStyleColors = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.metroTabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroTabPage2.CausesValidation = false;
            this.metroTabPage2.Controls.Add(this.listView2);
            this.metroTabPage2.Controls.Add(this.label21);
            this.metroTabPage2.Controls.Add(this.btnShowEmp);
            this.metroTabPage2.Controls.Add(this.label1);
            this.metroTabPage2.Controls.Add(this.tbEmpNameToFind);
            this.metroTabPage2.CustomBackground = true;
            this.metroTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 44);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1048, 637);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Employees";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.Click += new System.EventHandler(this.metroTabPage2_Click);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader17,
            this.columnHeader10,
            this.columnHeader14});
            this.listView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(30, 73);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(719, 419);
            this.listView2.TabIndex = 43;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Name";
            this.columnHeader7.Width = 150;
            // 
            // columnHeader8
            // 
            this.columnHeader8.DisplayIndex = 2;
            this.columnHeader8.Text = "Date Of Birth";
            this.columnHeader8.Width = 120;
            // 
            // columnHeader9
            // 
            this.columnHeader9.DisplayIndex = 1;
            this.columnHeader9.Text = "E-mail";
            this.columnHeader9.Width = 150;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Address";
            this.columnHeader17.Width = 150;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Hourly wage";
            this.columnHeader10.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Role";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(31, 35);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(87, 20);
            this.label21.TabIndex = 42;
            this.label21.Text = "Employees";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // btnShowEmp
            // 
            this.btnShowEmp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnShowEmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowEmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowEmp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnShowEmp.Location = new System.Drawing.Point(770, 218);
            this.btnShowEmp.Name = "btnShowEmp";
            this.btnShowEmp.Size = new System.Drawing.Size(130, 37);
            this.btnShowEmp.TabIndex = 40;
            this.btnShowEmp.Text = "Show";
            this.btnShowEmp.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(766, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Find employee by name";
            // 
            // tbEmpNameToFind
            // 
            this.tbEmpNameToFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmpNameToFind.Location = new System.Drawing.Point(770, 143);
            this.tbEmpNameToFind.Name = "tbEmpNameToFind";
            this.tbEmpNameToFind.Size = new System.Drawing.Size(255, 29);
            this.tbEmpNameToFind.TabIndex = 38;
            // 
            // metroTabPage5
            // 
            this.metroTabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.metroTabPage5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroTabPage5.CausesValidation = false;
            this.metroTabPage5.Controls.Add(this.label25);
            this.metroTabPage5.Controls.Add(this.label24);
            this.metroTabPage5.Controls.Add(this.chartEmployeeStatistics);
            this.metroTabPage5.Controls.Add(this.dtpTo);
            this.metroTabPage5.Controls.Add(this.dtpFrom);
            this.metroTabPage5.Controls.Add(this.btnLoadChart);
            this.metroTabPage5.Controls.Add(this.lb);
            this.metroTabPage5.Controls.Add(this.cbxCategoryStatistics);
            this.metroTabPage5.CustomBackground = true;
            this.metroTabPage5.HorizontalScrollbarBarColor = true;
            this.metroTabPage5.Location = new System.Drawing.Point(4, 44);
            this.metroTabPage5.Name = "metroTabPage5";
            this.metroTabPage5.Size = new System.Drawing.Size(1048, 637);
            this.metroTabPage5.TabIndex = 4;
            this.metroTabPage5.Text = "Statistics";
            this.metroTabPage5.VerticalScrollbarBarColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(51, 117);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 16);
            this.label25.TabIndex = 62;
            this.label25.Text = "From";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(407, 117);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(25, 16);
            this.label24.TabIndex = 61;
            this.label24.Text = "To";
            // 
            // chartEmployeeStatistics
            // 
            this.chartEmployeeStatistics.BackColor = System.Drawing.SystemColors.Control;
            this.chartEmployeeStatistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Name = "ChartArea1";
            this.chartEmployeeStatistics.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartEmployeeStatistics.Legends.Add(legend1);
            this.chartEmployeeStatistics.Location = new System.Drawing.Point(54, 163);
            this.chartEmployeeStatistics.Name = "chartEmployeeStatistics";
            this.chartEmployeeStatistics.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartEmployeeStatistics.Series.Add(series1);
            this.chartEmployeeStatistics.Size = new System.Drawing.Size(944, 370);
            this.chartEmployeeStatistics.TabIndex = 60;
            this.chartEmployeeStatistics.Text = "chartEmployeeStatistics";
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(458, 117);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(238, 20);
            this.dtpTo.TabIndex = 59;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(130, 117);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(239, 20);
            this.dtpFrom.TabIndex = 58;
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnLoadChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadChart.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLoadChart.Location = new System.Drawing.Point(739, 60);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(216, 41);
            this.btnLoadChart.TabIndex = 57;
            this.btnLoadChart.Text = "Load chart";
            this.btnLoadChart.UseVisualStyleBackColor = false;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.BackColor = System.Drawing.Color.Transparent;
            this.lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb.Location = new System.Drawing.Point(51, 60);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(63, 16);
            this.lb.TabIndex = 56;
            this.lb.Text = "Category";
            // 
            // cbxCategoryStatistics
            // 
            this.cbxCategoryStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCategoryStatistics.FormattingEnabled = true;
            this.cbxCategoryStatistics.Items.AddRange(new object[] {
            "Hourly wage per employee",
            "Salary per employee",
            "Number of employees per shift"});
            this.cbxCategoryStatistics.Location = new System.Drawing.Point(130, 60);
            this.cbxCategoryStatistics.Name = "cbxCategoryStatistics";
            this.cbxCategoryStatistics.Size = new System.Drawing.Size(566, 28);
            this.cbxCategoryStatistics.TabIndex = 55;
            this.cbxCategoryStatistics.SelectedIndexChanged += new System.EventHandler(this.cbxCategoryStatistics_SelectedIndexChanged);
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.metroTabPage4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroTabPage4.Controls.Add(this.lblFilterBy);
            this.metroTabPage4.Controls.Add(this.listView1);
            this.metroTabPage4.Controls.Add(this.comboBox1);
            this.metroTabPage4.Controls.Add(this.label4);
            this.metroTabPage4.Controls.Add(this.cbNameOfEmp);
            this.metroTabPage4.Controls.Add(this.cbSelectAll);
            this.metroTabPage4.Controls.Add(this.dtpDateShedule);
            this.metroTabPage4.Controls.Add(this.btnShowSchedule);
            this.metroTabPage4.Controls.Add(this.label8);
            this.metroTabPage4.Controls.Add(this.label9);
            this.metroTabPage4.CustomBackground = true;
            this.metroTabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 44);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(1048, 637);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "Schedule";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Location = new System.Drawing.Point(656, 128);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(64, 20);
            this.lblFilterBy.TabIndex = 53;
            this.lblFilterBy.Text = "Filter by";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(49, 43);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(563, 433);
            this.listView1.TabIndex = 52;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Employee Name";
            this.columnHeader11.Width = 199;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Date";
            this.columnHeader12.Width = 190;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Shift";
            this.columnHeader13.Width = 164;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(660, 377);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(305, 28);
            this.comboBox1.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(657, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 18);
            this.label4.TabIndex = 50;
            this.label4.Text = "Shift";
            // 
            // cbNameOfEmp
            // 
            this.cbNameOfEmp.FormattingEnabled = true;
            this.cbNameOfEmp.Location = new System.Drawing.Point(658, 202);
            this.cbNameOfEmp.Name = "cbNameOfEmp";
            this.cbNameOfEmp.Size = new System.Drawing.Size(305, 28);
            this.cbNameOfEmp.TabIndex = 49;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.cbSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(659, 57);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(156, 22);
            this.cbSelectAll.TabIndex = 48;
            this.cbSelectAll.Text = "See entire schedule";
            this.cbSelectAll.UseVisualStyleBackColor = false;
            // 
            // dtpDateShedule
            // 
            this.dtpDateShedule.Location = new System.Drawing.Point(661, 296);
            this.dtpDateShedule.Name = "dtpDateShedule";
            this.dtpDateShedule.Size = new System.Drawing.Size(304, 26);
            this.dtpDateShedule.TabIndex = 47;
            // 
            // btnShowSchedule
            // 
            this.btnShowSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnShowSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowSchedule.ForeColor = System.Drawing.SystemColors.Control;
            this.btnShowSchedule.Location = new System.Drawing.Point(659, 439);
            this.btnShowSchedule.Name = "btnShowSchedule";
            this.btnShowSchedule.Size = new System.Drawing.Size(304, 37);
            this.btnShowSchedule.TabIndex = 43;
            this.btnShowSchedule.Text = "Show";
            this.btnShowSchedule.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(657, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 18);
            this.label8.TabIndex = 41;
            this.label8.Text = "Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(655, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 18);
            this.label9.TabIndex = 39;
            this.label9.Text = "Name";
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.metroTabPage3.Controls.Add(this.listView5);
            this.metroTabPage3.CustomBackground = true;
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 44);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1048, 637);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Stock Info";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // listView5
            // 
            this.listView5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader6});
            this.listView5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView5.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView5.HideSelection = false;
            this.listView5.Location = new System.Drawing.Point(40, 45);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(954, 472);
            this.listView5.TabIndex = 20;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.Details;
            // 
            // col1
            // 
            this.col1.Text = "Category";
            this.col1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Product Name";
            this.columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Quantity";
            this.columnHeader3.Width = 250;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Price";
            this.columnHeader6.Width = 200;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.label18.Location = new System.Drawing.Point(293, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 24);
            this.label18.TabIndex = 26;
            this.label18.Text = "Username";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLogout.Location = new System.Drawing.Point(880, 35);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(145, 36);
            this.btnLogout.TabIndex = 44;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MediaBazar.Properties.Resources.linkedin_banner_image_1;
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::MediaBazar.Properties.Resources.user;
            this.pictureBox4.Location = new System.Drawing.Point(245, 55);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(29, 30);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 25;
            this.pictureBox4.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(60)))), ((int)(((byte)(77)))));
            this.label19.Location = new System.Drawing.Point(293, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(199, 24);
            this.label19.TabIndex = 54;
            this.label19.Text = "Logged in as manager";
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(213)))), ((int)(((byte)(203)))));
            this.ClientSize = new System.Drawing.Size(1049, 705);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mtbManager);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ManagerForm";
            this.Text = "Manager";
            this.mtbManager.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage5.ResumeLayout(false);
            this.metroTabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmployeeStatistics)).EndInit();
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage4.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbNameOfEmp;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.DateTimePicker dtpDateShedule;
        private System.Windows.Forms.Button btnShowSchedule;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroTabPage metroTabPage5;
        private System.Windows.Forms.Button btnShowEmp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEmpNameToFind;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnLogout;
        private MetroFramework.Controls.MetroTabControl mtbManager;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEmployeeStatistics;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnLoadChart;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.ComboBox cbxCategoryStatistics;
    }
}