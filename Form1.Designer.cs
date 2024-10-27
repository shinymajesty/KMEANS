namespace KmeansColorClustering
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxResolution = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel1 = new Panel();
            chkFullScreen = new CheckBox();
            label1 = new Label();
            panel2 = new Panel();
            lblSettingsApp = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            lblSettingsClustering = new Label();
            panel5 = new Panel();
            chkGenerateDiff = new CheckBox();
            numInIterations = new NumericUpDown();
            lblMaxIterations = new Label();
            numInRuns = new NumericUpDown();
            lblIterations = new Label();
            numInClusters = new NumericUpDown();
            lblClusterCount = new Label();
            panel6 = new Panel();
            horizontalLine1 = new Panel();
            tableLayoutPanel6 = new TableLayoutPanel();
            lblTitle = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            pictureBoxOutput = new PictureBox();
            lblResult = new Label();
            lblOriginal = new Label();
            pictureBoxOriginal = new PictureBox();
            panel7 = new Panel();
            btnGO = new Button();
            btnLoad = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            panel3 = new Panel();
            checkBox1 = new CheckBox();
            label2 = new Label();
            comboBox1 = new ComboBox();
            panel4 = new Panel();
            label3 = new Label();
            LoadImageDialog = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numInIterations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numInRuns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numInClusters).BeginInit();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOutput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            panel7.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxResolution
            // 
            comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxResolution.Font = new Font("Consolas", 16F);
            comboBoxResolution.FormattingEnabled = true;
            comboBoxResolution.Location = new Point(24, 33);
            comboBoxResolution.Margin = new Padding(4, 3, 4, 3);
            comboBoxResolution.Name = "comboBoxResolution";
            comboBoxResolution.Size = new Size(187, 32);
            comboBoxResolution.Sorted = true;
            comboBoxResolution.TabIndex = 0;
            comboBoxResolution.SelectedIndexChanged += ComboBoxResolution_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.008F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.992F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 2, 0);
            tableLayoutPanel1.Controls.Add(horizontalLine1, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1904, 1041);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel2.Controls.Add(panel6, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(1527, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.1067963F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.8932037F));
            tableLayoutPanel2.Size = new Size(374, 1035);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(panel1, 0, 1);
            tableLayoutPanel3.Controls.Add(panel2, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 452);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel3.Size = new Size(368, 580);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(chkFullScreen);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBoxResolution);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 61);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(360, 516);
            panel1.TabIndex = 0;
            // 
            // chkFullScreen
            // 
            chkFullScreen.AutoSize = true;
            chkFullScreen.Font = new Font("Consolas", 18F);
            chkFullScreen.Location = new Point(0, 71);
            chkFullScreen.Name = "chkFullScreen";
            chkFullScreen.Size = new Size(161, 32);
            chkFullScreen.TabIndex = 2;
            chkFullScreen.Text = "Fullscreen";
            chkFullScreen.UseVisualStyleBackColor = true;
            chkFullScreen.CheckStateChanged += ChkFullScreen_CheckStateChanged;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Consolas", 18F);
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(360, 30);
            label1.TabIndex = 1;
            label1.Text = "Resolution";
            // 
            // panel2
            // 
            panel2.Controls.Add(lblSettingsApp);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(362, 52);
            panel2.TabIndex = 1;
            // 
            // lblSettingsApp
            // 
            lblSettingsApp.Dock = DockStyle.Fill;
            lblSettingsApp.Font = new Font("Consolas", 24F, FontStyle.Bold);
            lblSettingsApp.Location = new Point(0, 0);
            lblSettingsApp.Name = "lblSettingsApp";
            lblSettingsApp.Size = new Size(362, 52);
            lblSettingsApp.TabIndex = 0;
            lblSettingsApp.Text = "App Settings";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(lblSettingsClustering, 0, 0);
            tableLayoutPanel5.Controls.Add(panel5, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(368, 438);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // lblSettingsClustering
            // 
            lblSettingsClustering.Dock = DockStyle.Fill;
            lblSettingsClustering.Font = new Font("Consolas", 24F, FontStyle.Bold);
            lblSettingsClustering.Location = new Point(3, 0);
            lblSettingsClustering.Name = "lblSettingsClustering";
            lblSettingsClustering.Size = new Size(362, 43);
            lblSettingsClustering.TabIndex = 1;
            lblSettingsClustering.Text = "Clustering Settings";
            // 
            // panel5
            // 
            panel5.Controls.Add(chkGenerateDiff);
            panel5.Controls.Add(numInIterations);
            panel5.Controls.Add(lblMaxIterations);
            panel5.Controls.Add(numInRuns);
            panel5.Controls.Add(lblIterations);
            panel5.Controls.Add(numInClusters);
            panel5.Controls.Add(lblClusterCount);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 46);
            panel5.Name = "panel5";
            panel5.Size = new Size(362, 389);
            panel5.TabIndex = 2;
            // 
            // chkGenerateDiff
            // 
            chkGenerateDiff.AutoSize = true;
            chkGenerateDiff.Font = new Font("Consolas", 18F);
            chkGenerateDiff.Location = new Point(0, 240);
            chkGenerateDiff.Name = "chkGenerateDiff";
            chkGenerateDiff.Size = new Size(187, 32);
            chkGenerateDiff.TabIndex = 8;
            chkGenerateDiff.Text = "Generate DIF";
            chkGenerateDiff.UseVisualStyleBackColor = true;
            // 
            // numInIterations
            // 
            numInIterations.Font = new Font("Consolas", 16F);
            numInIterations.Location = new Point(25, 195);
            numInIterations.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numInIterations.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numInIterations.Name = "numInIterations";
            numInIterations.Size = new Size(107, 32);
            numInIterations.TabIndex = 7;
            numInIterations.ThousandsSeparator = true;
            numInIterations.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // lblMaxIterations
            // 
            lblMaxIterations.Font = new Font("Consolas", 18F);
            lblMaxIterations.Location = new Point(0, 160);
            lblMaxIterations.Margin = new Padding(4, 0, 4, 0);
            lblMaxIterations.Name = "lblMaxIterations";
            lblMaxIterations.Size = new Size(362, 30);
            lblMaxIterations.TabIndex = 6;
            lblMaxIterations.Text = "Max Iterations per Run";
            // 
            // numInRuns
            // 
            numInRuns.Font = new Font("Consolas", 16F);
            numInRuns.Location = new Point(25, 115);
            numInRuns.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numInRuns.Name = "numInRuns";
            numInRuns.Size = new Size(107, 32);
            numInRuns.TabIndex = 5;
            numInRuns.ThousandsSeparator = true;
            numInRuns.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblIterations
            // 
            lblIterations.Font = new Font("Consolas", 18F);
            lblIterations.Location = new Point(0, 80);
            lblIterations.Margin = new Padding(4, 0, 4, 0);
            lblIterations.Name = "lblIterations";
            lblIterations.Size = new Size(362, 30);
            lblIterations.TabIndex = 4;
            lblIterations.Text = "Number of Runs";
            // 
            // numInClusters
            // 
            numInClusters.Font = new Font("Consolas", 16F);
            numInClusters.Location = new Point(25, 35);
            numInClusters.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numInClusters.Name = "numInClusters";
            numInClusters.Size = new Size(107, 32);
            numInClusters.TabIndex = 3;
            numInClusters.ThousandsSeparator = true;
            numInClusters.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblClusterCount
            // 
            lblClusterCount.Dock = DockStyle.Top;
            lblClusterCount.Font = new Font("Consolas", 18F);
            lblClusterCount.Location = new Point(0, 0);
            lblClusterCount.Margin = new Padding(4, 0, 4, 0);
            lblClusterCount.Name = "lblClusterCount";
            lblClusterCount.Size = new Size(362, 30);
            lblClusterCount.TabIndex = 2;
            lblClusterCount.Text = "Colors";
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(24, 24, 24);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 447);
            panel6.Name = "panel6";
            panel6.Size = new Size(368, 1);
            panel6.TabIndex = 3;
            // 
            // horizontalLine1
            // 
            horizontalLine1.BackColor = Color.FromArgb(24, 24, 24);
            horizontalLine1.Dock = DockStyle.Fill;
            horizontalLine1.Location = new Point(1522, 3);
            horizontalLine1.Name = "horizontalLine1";
            horizontalLine1.Size = new Size(1, 1035);
            horizontalLine1.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel8, 0, 1);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 7.05314F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 92.94686F));
            tableLayoutPanel6.Size = new Size(1513, 1035);
            tableLayoutPanel6.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.White;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Consolas", 46F);
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1507, 73);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "K-Means-Clustering";
            lblTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(tableLayoutPanel7, 0, 1);
            tableLayoutPanel8.Controls.Add(panel7, 0, 0);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 76);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 38.5983276F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 61.4016724F));
            tableLayoutPanel8.Size = new Size(1507, 956);
            tableLayoutPanel8.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Controls.Add(pictureBoxOutput, 1, 1);
            tableLayoutPanel7.Controls.Add(lblResult, 1, 0);
            tableLayoutPanel7.Controls.Add(lblOriginal, 0, 0);
            tableLayoutPanel7.Controls.Add(pictureBoxOriginal, 0, 1);
            tableLayoutPanel7.Dock = DockStyle.Top;
            tableLayoutPanel7.Location = new Point(3, 372);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 12.9237289F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 87.07627F));
            tableLayoutPanel7.Size = new Size(1501, 517);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // pictureBoxOutput
            // 
            pictureBoxOutput.Dock = DockStyle.Fill;
            pictureBoxOutput.Location = new Point(754, 73);
            pictureBoxOutput.Name = "pictureBoxOutput";
            pictureBoxOutput.Size = new Size(742, 439);
            pictureBoxOutput.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOutput.TabIndex = 3;
            pictureBoxOutput.TabStop = false;
            // 
            // lblResult
            // 
            lblResult.Dock = DockStyle.Fill;
            lblResult.Font = new Font("Consolas", 18F);
            lblResult.Location = new Point(754, 2);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(742, 66);
            lblResult.TabIndex = 1;
            lblResult.Text = "Reduced Image (Preview)";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOriginal
            // 
            lblOriginal.Dock = DockStyle.Fill;
            lblOriginal.Font = new Font("Consolas", 18F);
            lblOriginal.Location = new Point(5, 2);
            lblOriginal.Name = "lblOriginal";
            lblOriginal.Size = new Size(741, 66);
            lblOriginal.TabIndex = 0;
            lblOriginal.Text = "Original Image (Preview)";
            lblOriginal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.Dock = DockStyle.Fill;
            pictureBoxOriginal.Location = new Point(5, 73);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(741, 439);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOriginal.TabIndex = 2;
            pictureBoxOriginal.TabStop = false;
            // 
            // panel7
            // 
            panel7.Controls.Add(btnGO);
            panel7.Controls.Add(btnLoad);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(3, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(1501, 363);
            panel7.TabIndex = 1;
            // 
            // btnGO
            // 
            btnGO.Font = new Font("Consolas", 18F);
            btnGO.Location = new Point(974, 154);
            btnGO.Name = "btnGO";
            btnGO.Size = new Size(239, 49);
            btnGO.TabIndex = 1;
            btnGO.Text = "Reduce Colors";
            btnGO.UseVisualStyleBackColor = true;
            btnGO.Click += BtnGO_Click;
            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Consolas", 18F);
            btnLoad.Location = new Point(258, 154);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(193, 49);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load Image";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += BtnLoad_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(panel3, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(200, 100);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(checkBox1);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(comboBox1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(4, 23);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(192, 74);
            panel3.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Consolas", 14F);
            checkBox1.Location = new Point(24, 71);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(129, 26);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Fullscreen";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Consolas", 18F);
            label2.Location = new Point(0, 0);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(192, 30);
            label2.TabIndex = 1;
            label2.Text = "Resolution";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Consolas", 16F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(24, 33);
            comboBox1.Margin = new Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(187, 34);
            comboBox1.Sorted = true;
            comboBox1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(label3);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(194, 45);
            panel4.TabIndex = 1;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Consolas", 22F);
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(194, 45);
            label3.TabIndex = 0;
            label3.Text = "App Settings";
            // 
            // LoadImageDialog
            // 
            LoadImageDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(1280, 720);
            Name = "Form1";
            StartPosition = FormStartPosition.Manual;
            Text = "Color Reduction by Clustering";
            TopMost = true;
            SizeChanged += Form1_SizeChanged;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numInIterations).EndInit();
            ((System.ComponentModel.ISupportInitialize)numInRuns).EndInit();
            ((System.ComponentModel.ISupportInitialize)numInClusters).EndInit();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOutput).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            panel7.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBoxResolution;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Label label1;
        private CheckBox chkFullScreen;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel2;
        private Label lblSettingsApp;
        private TableLayoutPanel tableLayoutPanel5;
        private Label lblSettingsClustering;
        private TableLayoutPanel tableLayoutPanel4;
        private Panel panel3;
        private CheckBox checkBox1;
        private Label label2;
        private ComboBox comboBox1;
        private Panel panel4;
        private Label label3;
        private Panel panel5;
        private Label lblClusterCount;
        private NumericUpDown numInClusters;
        private Panel horizontalLine1;
        private NumericUpDown numInRuns;
        private Label lblIterations;
        private NumericUpDown numInIterations;
        private Label lblMaxIterations;
        private CheckBox chkGenerateDiff;
        private Panel panel6;
        private TableLayoutPanel tableLayoutPanel6;
        private Label lblTitle;
        private TableLayoutPanel tableLayoutPanel7;
        private Label lblResult;
        private Label lblOriginal;
        private TableLayoutPanel tableLayoutPanel8;
        private Panel panel7;
        private Button btnGO;
        private Button btnLoad;
        private OpenFileDialog LoadImageDialog;
        private PictureBox pictureBoxOutput;
        private PictureBox pictureBoxOriginal;
    }
}
