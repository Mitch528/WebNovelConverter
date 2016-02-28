namespace WebNovelConverter
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.modeSelectedLabel = new System.Windows.Forms.Label();
            this.modeSelectedTextBox = new System.Windows.Forms.TextBox();
            this.retrieveButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.unknownListBox = new System.Windows.Forms.ListBox();
            this.chaptersListBox = new System.Windows.Forms.ListBox();
            this.convertBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.retrieveBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.coverTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.manualChapAddButton = new System.Windows.Forms.Button();
            this.manualChapUrlTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.websiteTypeComboBox = new System.Windows.Forms.ComboBox();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.reverseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 735);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chapters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(888, 735);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Links to ignore";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(29, 851);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(1180, 275);
            this.outputTextBox.TabIndex = 4;
            this.outputTextBox.Text = "";
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(544, 1241);
            this.convertButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(149, 44);
            this.convertButton.TabIndex = 5;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "epub";
            this.saveFileDialog.Filter = "epub|*.epub";
            // 
            // upButton
            // 
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(601, 503);
            this.upButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(75, 72);
            this.upButton.TabIndex = 6;
            this.upButton.Text = "▲";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(601, 585);
            this.downButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(75, 72);
            this.downButton.TabIndex = 7;
            this.downButton.Text = "▼";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftButton.Location = new System.Drawing.Point(585, 305);
            this.leftButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(108, 72);
            this.leftButton.TabIndex = 8;
            this.leftButton.Text = "<";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightButton.Location = new System.Drawing.Point(585, 387);
            this.rightButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(108, 72);
            this.rightButton.TabIndex = 9;
            this.rightButton.Text = ">";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // modeSelectedLabel
            // 
            this.modeSelectedLabel.AutoSize = true;
            this.modeSelectedLabel.Location = new System.Drawing.Point(194, 124);
            this.modeSelectedLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.modeSelectedLabel.Name = "modeSelectedLabel";
            this.modeSelectedLabel.Size = new System.Drawing.Size(104, 25);
            this.modeSelectedLabel.TabIndex = 10;
            this.modeSelectedLabel.Text = "TOC URL";
            // 
            // modeSelectedTextBox
            // 
            this.modeSelectedTextBox.Location = new System.Drawing.Point(314, 117);
            this.modeSelectedTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.modeSelectedTextBox.Name = "modeSelectedTextBox";
            this.modeSelectedTextBox.Size = new System.Drawing.Size(615, 31);
            this.modeSelectedTextBox.TabIndex = 0;
            // 
            // retrieveButton
            // 
            this.retrieveButton.Location = new System.Drawing.Point(945, 114);
            this.retrieveButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.retrieveButton.Name = "retrieveButton";
            this.retrieveButton.Size = new System.Drawing.Size(149, 44);
            this.retrieveButton.TabIndex = 1;
            this.retrieveButton.Text = "Retrieve";
            this.retrieveButton.UseVisualStyleBackColor = true;
            this.retrieveButton.Click += new System.EventHandler(this.retrieveButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(29, 795);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1184, 44);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 13;
            this.progressBar.Visible = false;
            // 
            // unknownListBox
            // 
            this.unknownListBox.FormattingEnabled = true;
            this.unknownListBox.HorizontalScrollbar = true;
            this.unknownListBox.ItemHeight = 25;
            this.unknownListBox.Location = new System.Drawing.Point(705, 223);
            this.unknownListBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.unknownListBox.Name = "unknownListBox";
            this.unknownListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.unknownListBox.Size = new System.Drawing.Size(521, 504);
            this.unknownListBox.TabIndex = 14;
            // 
            // chaptersListBox
            // 
            this.chaptersListBox.FormattingEnabled = true;
            this.chaptersListBox.HorizontalScrollbar = true;
            this.chaptersListBox.ItemHeight = 25;
            this.chaptersListBox.Location = new System.Drawing.Point(48, 223);
            this.chaptersListBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chaptersListBox.Name = "chaptersListBox";
            this.chaptersListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.chaptersListBox.Size = new System.Drawing.Size(521, 504);
            this.chaptersListBox.TabIndex = 15;
            // 
            // convertBackgroundWorker
            // 
            this.convertBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.convertBackgroundWorker_DoWork);
            // 
            // retrieveBackgroundWorker
            // 
            this.retrieveBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.retrieveBackgroundWorker_DoWork);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem});
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.settingsMenuItem,
            this.menuItem4,
            this.exitMenuItem});
            this.fileMenuItem.Text = "File";
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Index = 0;
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 2;
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 1147);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(376, 1141);
            this.titleTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(528, 31);
            this.titleTextBox.TabIndex = 17;
            // 
            // coverTextBox
            // 
            this.coverTextBox.Location = new System.Drawing.Point(376, 1191);
            this.coverTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.coverTextBox.Name = "coverTextBox";
            this.coverTextBox.Size = new System.Drawing.Size(528, 31);
            this.coverTextBox.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 1197);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Cover URL (optional)";
            // 
            // manualChapAddButton
            // 
            this.manualChapAddButton.Location = new System.Drawing.Point(945, 166);
            this.manualChapAddButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.manualChapAddButton.Name = "manualChapAddButton";
            this.manualChapAddButton.Size = new System.Drawing.Size(149, 44);
            this.manualChapAddButton.TabIndex = 21;
            this.manualChapAddButton.Text = "Add";
            this.manualChapAddButton.UseVisualStyleBackColor = true;
            this.manualChapAddButton.Click += new System.EventHandler(this.manualChapAddButton_Click);
            // 
            // manualChapUrlTextBox
            // 
            this.manualChapUrlTextBox.Location = new System.Drawing.Point(314, 170);
            this.manualChapUrlTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.manualChapUrlTextBox.Name = "manualChapUrlTextBox";
            this.manualChapUrlTextBox.Size = new System.Drawing.Size(615, 31);
            this.manualChapUrlTextBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "Manual Chapter URL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 25);
            this.label7.TabIndex = 24;
            this.label7.Text = "Type";
            // 
            // websiteTypeComboBox
            // 
            this.websiteTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.websiteTypeComboBox.FormattingEnabled = true;
            this.websiteTypeComboBox.Items.AddRange(new object[] {
            "WordPress",
            "RoyalRoadL",
            "Baka-Tsuki",
            "Blogspot"});
            this.websiteTypeComboBox.Location = new System.Drawing.Point(314, 12);
            this.websiteTypeComboBox.Name = "websiteTypeComboBox";
            this.websiteTypeComboBox.Size = new System.Drawing.Size(265, 33);
            this.websiteTypeComboBox.TabIndex = 25;
            this.websiteTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.websiteTypeComboBox_SelectedIndexChanged);
            // 
            // modeComboBox
            // 
            this.modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Items.AddRange(new object[] {
            "Table of Contents"});
            this.modeComboBox.Location = new System.Drawing.Point(314, 68);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(265, 33);
            this.modeComboBox.TabIndex = 27;
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 71);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 25);
            this.label8.TabIndex = 26;
            this.label8.Text = "Mode";
            // 
            // reverseButton
            // 
            this.reverseButton.BackgroundImage = global::WebNovelConverter.Properties.Resources.Rotate_Right_64;
            this.reverseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.reverseButton.Location = new System.Drawing.Point(585, 223);
            this.reverseButton.Name = "reverseButton";
            this.reverseButton.Size = new System.Drawing.Size(108, 73);
            this.reverseButton.TabIndex = 23;
            this.reverseButton.UseVisualStyleBackColor = true;
            this.reverseButton.Click += new System.EventHandler(this.reverseButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1272, 1301);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.websiteTypeComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.reverseButton);
            this.Controls.Add(this.manualChapAddButton);
            this.Controls.Add(this.manualChapUrlTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.coverTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chaptersListBox);
            this.Controls.Add(this.unknownListBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.retrieveButton);
            this.Controls.Add(this.modeSelectedTextBox);
            this.Controls.Add(this.modeSelectedLabel);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Web Novel Converter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Label modeSelectedLabel;
        private System.Windows.Forms.TextBox modeSelectedTextBox;
        private System.Windows.Forms.Button retrieveButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox unknownListBox;
        private System.Windows.Forms.ListBox chaptersListBox;
        private System.ComponentModel.BackgroundWorker convertBackgroundWorker;
        private System.ComponentModel.BackgroundWorker retrieveBackgroundWorker;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem fileMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem settingsMenuItem;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox coverTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button manualChapAddButton;
        private System.Windows.Forms.TextBox manualChapUrlTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button reverseButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox websiteTypeComboBox;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.Label label8;
    }
}

