using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebNovelConverter.Properties;

namespace WebNovelConverter
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            delayNumericUpDown.Value = Settings.Default.DelayPerChapter;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Settings.Default.DelayPerChapter = (int)delayNumericUpDown.Value;
            Settings.Default.Save();

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
