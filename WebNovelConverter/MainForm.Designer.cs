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
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chapters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Links to ignore";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 393);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(592, 145);
            this.outputTextBox.TabIndex = 4;
            this.outputTextBox.Text = "";
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(269, 544);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 5;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "epub";
            this.saveFileDialog.Filter = "epub|*.epub|pdf|.pdf";
            // 
            // upButton
            // 
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(298, 212);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(37, 38);
            this.upButton.TabIndex = 6;
            this.upButton.Text = "▲";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(298, 255);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(37, 38);
            this.downButton.TabIndex = 7;
            this.downButton.Text = "▼";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftButton.Location = new System.Drawing.Point(290, 109);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(54, 38);
            this.leftButton.TabIndex = 8;
            this.leftButton.Text = "<";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightButton.Location = new System.Drawing.Point(290, 152);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(54, 38);
            this.rightButton.TabIndex = 9;
            this.rightButton.Text = ">";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "TOC URL";
            // 
            // tocUrlTextBox
            // 
            this.tocUrlTextBox.Location = new System.Drawing.Point(156, 22);
            this.tocUrlTextBox.Name = "tocUrlTextBox";
            this.tocUrlTextBox.Size = new System.Drawing.Size(309, 20);
            this.tocUrlTextBox.TabIndex = 0;
            // 
            // retrieveButton
            // 
            this.retrieveButton.Location = new System.Drawing.Point(471, 20);
            this.retrieveButton.Name = "retrieveButton";
            this.retrieveButton.Size = new System.Drawing.Size(75, 23);
            this.retrieveButton.TabIndex = 1;
            this.retrieveButton.Text = "Retrieve";
            this.retrieveButton.UseVisualStyleBackColor = true;
            this.retrieveButton.Click += new System.EventHandler(this.retrieveButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 364);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(592, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 13;
            this.progressBar.Visible = false;
            // 
            // unknownListBox
            // 
            this.unknownListBox.FormattingEnabled = true;
            this.unknownListBox.HorizontalScrollbar = true;
            this.unknownListBox.Location = new System.Drawing.Point(350, 66);
            this.unknownListBox.Name = "unknownListBox";
            this.unknownListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.unknownListBox.Size = new System.Drawing.Size(263, 264);
            this.unknownListBox.TabIndex = 14;
            // 
            // chaptersListBox
            // 
            this.chaptersListBox.FormattingEnabled = true;
            this.chaptersListBox.HorizontalScrollbar = true;
            this.chaptersListBox.Location = new System.Drawing.Point(21, 66);
            this.chaptersListBox.Name = "chaptersListBox";
            this.chaptersListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.chaptersListBox.Size = new System.Drawing.Size(263, 264);
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
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 2;
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 570);
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
    }
}

