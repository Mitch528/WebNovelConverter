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
            this.label3 = new System.Windows.Forms.Label();
            this.tocUrlTextBox = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 512);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chapters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(662, 512);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Links to ignore";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(18, 605);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(886, 221);
            this.outputTextBox.TabIndex = 4;
            this.outputTextBox.Text = "";
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(404, 917);
            this.convertButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(112, 35);
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
            this.upButton.Location = new System.Drawing.Point(447, 326);
            this.upButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(56, 58);
            this.upButton.TabIndex = 6;
            this.upButton.Text = "▲";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(447, 392);
            this.downButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(56, 58);
            this.downButton.TabIndex = 7;
            this.downButton.Text = "▼";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftButton.Location = new System.Drawing.Point(435, 168);
            this.leftButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(81, 58);
            this.leftButton.TabIndex = 8;
            this.leftButton.Text = "<";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightButton.Location = new System.Drawing.Point(435, 234);
            this.rightButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(81, 58);
            this.rightButton.TabIndex = 9;
            this.rightButton.Text = ">";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "TOC URL";
            // 
            // tocUrlTextBox
            // 
            this.tocUrlTextBox.Location = new System.Drawing.Point(232, 18);
            this.tocUrlTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tocUrlTextBox.Name = "tocUrlTextBox";
            this.tocUrlTextBox.Size = new System.Drawing.Size(462, 26);
            this.tocUrlTextBox.TabIndex = 0;
            // 
            // retrieveButton
            // 
            this.retrieveButton.Location = new System.Drawing.Point(705, 15);
            this.retrieveButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.retrieveButton.Name = "retrieveButton";
            this.retrieveButton.Size = new System.Drawing.Size(112, 35);
            this.retrieveButton.TabIndex = 1;
            this.retrieveButton.Text = "Retrieve";
            this.retrieveButton.UseVisualStyleBackColor = true;
            this.retrieveButton.Click += new System.EventHandler(this.retrieveButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(18, 560);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(888, 35);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 13;
            this.progressBar.Visible = false;
            // 
            // unknownListBox
            // 
            this.unknownListBox.FormattingEnabled = true;
            this.unknownListBox.HorizontalScrollbar = true;
            this.unknownListBox.ItemHeight = 20;
            this.unknownListBox.Location = new System.Drawing.Point(525, 102);
            this.unknownListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.unknownListBox.Name = "unknownListBox";
            this.unknownListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.unknownListBox.Size = new System.Drawing.Size(392, 404);
            this.unknownListBox.TabIndex = 14;
            // 
            // chaptersListBox
            // 
            this.chaptersListBox.FormattingEnabled = true;
            this.chaptersListBox.HorizontalScrollbar = true;
            this.chaptersListBox.ItemHeight = 20;
            this.chaptersListBox.Location = new System.Drawing.Point(32, 102);
            this.chaptersListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chaptersListBox.Name = "chaptersListBox";
            this.chaptersListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.chaptersListBox.Size = new System.Drawing.Size(392, 404);
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
            this.label4.Location = new System.Drawing.Point(228, 842);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(278, 837);
            this.titleTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(397, 26);
            this.titleTextBox.TabIndex = 17;
            // 
            // coverTextBox
            // 
            this.coverTextBox.Location = new System.Drawing.Point(278, 877);
            this.coverTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.coverTextBox.Name = "coverTextBox";
            this.coverTextBox.Size = new System.Drawing.Size(397, 26);
            this.coverTextBox.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 882);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Cover URL (optional)";
            // 
            // manualChapAddButton
            // 
            this.manualChapAddButton.Location = new System.Drawing.Point(705, 57);
            this.manualChapAddButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.manualChapAddButton.Name = "manualChapAddButton";
            this.manualChapAddButton.Size = new System.Drawing.Size(112, 35);
            this.manualChapAddButton.TabIndex = 21;
            this.manualChapAddButton.Text = "Add";
            this.manualChapAddButton.UseVisualStyleBackColor = true;
            this.manualChapAddButton.Click += new System.EventHandler(this.manualChapAddButton_Click);
            // 
            // manualChapUrlTextBox
            // 
            this.manualChapUrlTextBox.Location = new System.Drawing.Point(232, 60);
            this.manualChapUrlTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.manualChapUrlTextBox.Name = "manualChapUrlTextBox";
            this.manualChapUrlTextBox.Size = new System.Drawing.Size(462, 26);
            this.manualChapUrlTextBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Manual Chapter URL";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(954, 956);
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
            this.Controls.Add(this.tocUrlTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Web Novel Converter v1.2.2";
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tocUrlTextBox;
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
    }
}

