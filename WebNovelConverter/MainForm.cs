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
using Epub.Net;
using Epub.Net.Extensions;
using Epub.Net.Models;
using WebNovelConverter.Properties;
using WebNovelConverter.Sources;

namespace WebNovelConverter
{
    public partial class MainForm : Form
    {
        private readonly WordPressSource _wordpress = new WordPressSource();

        private readonly NovelSourceCollection _sources = new NovelSourceCollection();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _sources.Add(new RoyalRoadL());
            _sources.Add(new BakaTsukiSource());
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

                titleTextBox.Text = string.Empty;
                coverTextBox.Text = string.Empty;

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
            if (!string.IsNullOrEmpty(titleTextBox.Text))
                saveFileDialog.FileName = $"{titleTextBox.Text.ToValidFilePath()}.epub";

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
            EBook book = new EBook
            {
                Title = titleTextBox.Text,
                CoverImage = coverTextBox.Text
            };

            foreach (ChapterLink link in chaptersListBox.Items)
            {
                try
                {
                    WebNovelSource source = GetSource(link.Url);
                    WebNovelChapter chapter = await source.GetChapterAsync(link);

                    if (chapter == null)
                    {
                        WriteText($"Failed to process {link.Name}!", Color.Red);
                    }
                    else
                    {
                        book.Chapters.Add(new Chapter { Name = link.Name, Content = chapter.Content });

                        WriteText($"{link.Name} has been processed.", Color.Green);
                    }
                }
                catch (Exception ex)
                {
                    WriteText($"Failed to process {link.Name}!", Color.Red);
                    WriteText($"ERROR: {ex}", Color.Red);
                }

                await Task.Delay(TimeSpan.FromSeconds(Settings.Default.DelayPerChapter));
            }

            WriteText("Generating epub...");

            await book.GenerateEpubAsync(e.Argument.ToString());

            WriteText("Done!", Color.Green);

            Invoke((MethodInvoker)delegate
            {
                progressBar.Visible = false;
                convertButton.Enabled = true;
            });
        }

        private async void retrieveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string tocUrl = tocUrlTextBox.Text;

            WebNovelSource source = GetSource(tocUrl);
            string coverUrl = await source.GetNovelCoverAsync(tocUrl);
            coverUrl = coverUrl.StartsWith("//") ? coverUrl.Substring(2) : coverUrl;

            ChapterLink[] links = (await source.GetChapterLinksAsync(tocUrl)).ToArray();

            Invoke((MethodInvoker)delegate
            {
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

                if (!string.IsNullOrEmpty(coverUrl))
                    coverTextBox.Text = new UriBuilder(coverUrl).Uri.AbsoluteUri;

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
            return _sources.Get(url) ?? _wordpress;
        }

        private void WriteText(string text)
        {
            Invoke((MethodInvoker)delegate
            {
                outputTextBox.AppendText(text + Environment.NewLine);
                outputTextBox.SelectionStart = outputTextBox.Text.Length;
                outputTextBox.ScrollToCaret();
            });
        }

        public void WriteText(string text, Color color)
        {
            Invoke((MethodInvoker)delegate
            {
                outputTextBox.SelectionStart = outputTextBox.TextLength;
                outputTextBox.SelectionLength = 0;

                outputTextBox.SelectionColor = color;
                outputTextBox.AppendText(text + Environment.NewLine);
                outputTextBox.SelectionColor = outputTextBox.ForeColor;

                outputTextBox.SelectionStart = outputTextBox.Text.Length;
                outputTextBox.ScrollToCaret();
            });
        }
    }
}
