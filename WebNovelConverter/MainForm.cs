using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebNovelConverter.Properties;
using WebNovelConverter.Sources;

namespace WebNovelConverter
{
    public partial class MainForm : Form
    {
        private readonly RoyalRoadL _royalRoad = new RoyalRoadL();
        private readonly WordPress _wordPress = new WordPress();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Settings.Default.Upgrade();
        }

        private void retrieveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tocUrlTextBox.Text))
            {
                MessageBox.Show("Invalid url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!retrieveBackgroundWorker.IsBusy)
            {
                chaptersListBox.Items.Clear();
                unknownListBox.Items.Clear();

                progressBar.Visible = true;
                retrieveButton.Enabled = false;

                retrieveBackgroundWorker.RunWorkerAsync();
            }
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            var selected = unknownListBox.SelectedItems;

            chaptersListBox.Items.AddRange(selected.Cast<object>().ToArray());

            foreach (var item in selected.Cast<object>().ToList())
            {
                unknownListBox.Items.Remove(item);
            }
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            var selected = chaptersListBox.SelectedItems;

            unknownListBox.Items.AddRange(selected.Cast<object>().ToArray());

            foreach (var item in selected.Cast<object>().ToList())
            {
                chaptersListBox.Items.Remove(item);
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            var selected = chaptersListBox.SelectedItems;

            foreach (var item in selected.Cast<object>().ToList())
            {
                int idx = chaptersListBox.Items.IndexOf(item);

                if (idx > 0)
                {
                    chaptersListBox.Items.Remove(item);
                    chaptersListBox.Items.Insert(idx - 1, item);

                    chaptersListBox.SelectedIndex = idx - 1;
                }
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            var selected = chaptersListBox.SelectedItems;

            foreach (var item in selected.Cast<object>().ToList())
            {
                int idx = chaptersListBox.Items.IndexOf(item);

                if (idx > -1 && idx < chaptersListBox.Items.Count - 1)
                {
                    chaptersListBox.Items.Remove(item);
                    chaptersListBox.Items.Insert(idx + 1, item);

                    chaptersListBox.SelectedIndex = idx + 1;
                }
            }

        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            DialogResult saveResult = saveFileDialog.ShowDialog();

            if (saveResult == DialogResult.OK && !convertBackgroundWorker.IsBusy)
            {
                outputTextBox.ResetText();
                progressBar.Visible = true;
                convertButton.Enabled = false;

                string savePath = saveFileDialog.FileName;

                convertBackgroundWorker.RunWorkerAsync(savePath);
            }
        }

        private async void convertBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string tmpFile = Path.GetTempFileName();
            string newTmpFile = tmpFile + ".html";

            File.Move(tmpFile, newTmpFile);

            using (StreamWriter writer = new StreamWriter(newTmpFile))
            {
                foreach (ChapterLink link in chaptersListBox.Items)
                {
                    WebNovelSource source = GetSource(link.Url);
                    WebNovelChapter chapter = await source.GetChapterAsync(link);

                    if (chapter == null)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            outputTextBox.AppendText(string.Format("failed to process {0}!{1}", link.Name, Environment.NewLine));
                            outputTextBox.SelectionStart = outputTextBox.Text.Length;
                            outputTextBox.ScrollToCaret();
                        });
                    }
                    else
                    {
                        writer.WriteLine(chapter.Content);

                        Invoke((MethodInvoker)delegate
                        {
                            outputTextBox.AppendText(string.Format("{0} has been processed!{1}", link.Name, Environment.NewLine));
                            outputTextBox.SelectionStart = outputTextBox.Text.Length;
                            outputTextBox.ScrollToCaret();
                        });
                    }

                    await Task.Delay(TimeSpan.FromSeconds(Settings.Default.DelayPerChapter));
                }
            }

            string ebookConvert = Path.Combine("Calibre Portable", "Calibre", "ebook-convert.exe");

            StringBuilder argsBuilder = new StringBuilder(string.Format("\"{0}\" \"{1}\" ", newTmpFile, e.Argument));

            Invoke((MethodInvoker)delegate
            {
                if (!string.IsNullOrEmpty(titleTextBox.Text))
                {
                    argsBuilder.AppendFormat("--title \"{0}\" ", titleTextBox.Text);
                }

                if (!string.IsNullOrEmpty(coverTextBox.Text))
                {
                    argsBuilder.AppendFormat("--cover \"{0}\"", coverTextBox.Text);
                }
            });

            ProcessStartInfo psi = new ProcessStartInfo(ebookConvert, argsBuilder.ToString());

            Process proc = new Process();
            proc.StartInfo = psi;

            proc.Start();
            proc.WaitForExit();

            File.Delete(newTmpFile);

            Invoke((MethodInvoker)delegate
            {
                progressBar.Visible = false;
                convertButton.Enabled = true;
            });
        }

        private async void retrieveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WebNovelSource source = GetSource(tocUrlTextBox.Text);
            ChapterLink[] links = await source.GetLinks(tocUrlTextBox.Text);

            Invoke((MethodInvoker)delegate
            {
                if (links == null)
                {
                    MessageBox.Show("Error parsing website.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    progressBar.Visible = false;
                    retrieveButton.Enabled = true;

                    return;
                }

                foreach (ChapterLink link in links)
                {
                    if (link.Unknown)
                    {
                        unknownListBox.Items.Add(link);
                    }
                    else
                    {
                        chaptersListBox.Items.Add(link);
                    }
                }

                progressBar.Visible = false;
                retrieveButton.Enabled = true;
            });
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm sf = new SettingsForm())
                sf.ShowDialog();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void manualChapAddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(manualChapUrlTextBox.Text))
            {
                MessageBox.Show("Invalid url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string chapterName = Microsoft.VisualBasic.Interaction.InputBox("Chapter Name");

            if (!string.IsNullOrEmpty(chapterName))
            {
                chaptersListBox.Items.Add(new ChapterLink
                {
                    Name = chapterName,
                    Url = manualChapUrlTextBox.Text
                });
            }
        }

        private WebNovelSource GetSource(string url)
        {
            string domain = new Uri(tocUrlTextBox.Text).GetLeftPart(UriPartial.Authority).Replace("/www.", "/").Replace("http://", "");

            WebNovelSource source;
            switch (domain.ToLower())
            {
                case "royalroadl.com":
                    source = _royalRoad;
                    break;
                default:
                    source =  _wordPress;
                    break;
            }

            return source;
        }
    }
}
