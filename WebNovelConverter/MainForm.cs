using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epub.Net;
using Epub.Net.Extensions;
using Epub.Net.Models;
using WebNovelConverter.Properties;
using WebNovelConverter.Sources;
using WebNovelConverter.Sources.Models;

namespace WebNovelConverter
{
    public partial class MainForm : Form
    {
        private readonly NovelSourceCollection _sources = new NovelSourceCollection();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text += $" {ver.Major}.{ver.Minor}.{ver.Build}";

            _sources.Add(new WordPressSource());
            _sources.Add(new RoyalRoadLSource());
            _sources.Add(new BakaTsukiSource());
            _sources.Add(new BlogspotSource());
            _sources.Add(new NovelsNaoSource());
            _sources.Add(new LNMTLSource());

            websiteTypeComboBox.SelectedIndex = 0;
            modeComboBox.SelectedIndex = 0;
        }

        private void retrieveButton_Click(object sender, EventArgs e)
        {
            Uri uri;
            if (string.IsNullOrEmpty(modeSelectedTextBox.Text) || !Uri.TryCreate(modeSelectedTextBox.Text, UriKind.Absolute, out uri))
            {
                MessageBox.Show("Invalid url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!retrieveBackgroundWorker.IsBusy)
            {
                outputTextBox.ResetText();
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
            var items = new List<object>();
            string type = string.Empty;
            string mode = string.Empty;
            Invoke((MethodInvoker)delegate
            {
                type = ((string)websiteTypeComboBox.SelectedItem).ToLower();
                mode = ((string)modeComboBox.SelectedItem).ToLower();
                items.AddRange(chaptersListBox.Items.Cast<object>());
            });

            EBook book = new EBook
            {
                Title = titleTextBox.Text,
                CoverImage = coverTextBox.Text
            };

            foreach (object obj in items)
            {
                if (obj is ChapterLink)
                {
                    ChapterLink link = (ChapterLink)obj;

                    try
                    {
                        WebNovelSource source = GetSource(link.Url, type);
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

                    if (Settings.Default.DelayPerChapter > 0)
                        await Task.Delay(TimeSpan.FromSeconds(Settings.Default.DelayPerChapter));
                }
                else if (obj is WebNovelChapter)
                {
                    WebNovelChapter wn = (WebNovelChapter)obj;

                    book.Chapters.Add(new Chapter { Name = wn.ChapterName, Content = wn.Content });
                }
            }

            WriteText("Generating epub...");

            try
            {
                await book.GenerateEpubAsync(e.Argument.ToString());
            }
            catch (Exception ex)
            {
                WriteText("Error generating Epub", Color.Red);
                WriteText($"ERROR: {ex}", Color.Red);
            }

            WriteText("Done!", Color.Green);

            Invoke((MethodInvoker)delegate
            {
                progressBar.Visible = false;
                convertButton.Enabled = true;
            });
        }

        private async void retrieveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string type = string.Empty;
            string mode = string.Empty;
            string modeSelectedText = string.Empty;
            int amount = 0;

            Invoke((MethodInvoker)delegate
            {
                type = ((string)websiteTypeComboBox.SelectedItem).ToLower();
                mode = ((string)modeComboBox.SelectedItem).ToLower();
                modeSelectedText = modeSelectedTextBox.Text;
                amount = (int)amountNumericUpDown.Value;
            });

            if (!(modeSelectedText.StartsWith("http://") || modeSelectedText.StartsWith("https://")))
                modeSelectedText = "http://" + modeSelectedText;

            WebNovelSource source = GetSource(modeSelectedText, type);

            WebNovelInfo novelInfo = await source.GetNovelInfoAsync(modeSelectedText);

            if (mode == "table of contents")
            {
                ChapterLink[] links = (await source.GetChapterLinksAsync(modeSelectedText)).ToArray();

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

                    if (novelInfo != null)
                    {
                        if (!string.IsNullOrEmpty(novelInfo.CoverUrl))
                        {
                            try
                            {
                                string coverUrl = novelInfo.CoverUrl;
                                coverUrl = coverUrl.StartsWith("//") ? coverUrl.Substring(2) : coverUrl;
                                coverTextBox.Text = new UriBuilder(coverUrl).Uri.AbsoluteUri;
                            }
                            catch (UriFormatException) { }
                        }

                        if (!string.IsNullOrEmpty(novelInfo.Title))
                            titleTextBox.Text = novelInfo.Title;
                    }

                    progressBar.Visible = false;
                    retrieveButton.Enabled = true;
                });
            }
            else if (mode == "next chapter link")
            {
                ChapterLink firstChapter = new ChapterLink { Url = modeSelectedText };
                ChapterLink current = firstChapter;

                int ctr = 1;
                var chapters = new List<WebNovelChapter>();
                while (true)
                {
                    WebNovelChapter chapter;
                    try
                    {
                        chapter = await source.GetChapterAsync(current);
                    }
                    catch (HttpRequestException)
                    {
                        break;
                    }

                    if (chapter == null)
                        break;

                    if (string.IsNullOrEmpty(chapter.ChapterName))
                        chapter.ChapterName = current.Url;

                    chapters.Add(chapter);

                    WriteText($"Found Chapter {chapter.ChapterName}", Color.Green);

                    if (string.IsNullOrEmpty(chapter.NextChapterUrl) || chapter.Url == chapter.NextChapterUrl)
                        break;

                    current = new ChapterLink { Url = chapter.NextChapterUrl };

                    if (ctr == amount)
                        break;

                    ctr++;
                }

                Invoke((MethodInvoker)delegate
                {
                    chaptersListBox.Items.AddRange(chapters.Cast<object>().ToArray());

                    progressBar.Visible = false;
                    retrieveButton.Enabled = true;
                });
            }
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

        private WebNovelSource GetSource(string url, string fallbackType)
        {
            return _sources.Get(url) ?? _sources.GetByName(fallbackType);
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

        private void reverseButton_Click(object sender, EventArgs e)
        {
            var items = chaptersListBox.Items.Cast<object>().ToArray();
            chaptersListBox.Items.Clear();

            chaptersListBox.Items.AddRange(items.Reverse().ToArray());
        }

        private void websiteTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modeComboBox.Items.Clear();

            string type = (string)websiteTypeComboBox.SelectedItem;

            switch (type.ToLower())
            {
                case "wordpress":
                case "blogspot":
                    modeComboBox.Items.AddRange(new object[] { "Table of Contents", "Next Chapter Link" });
                    break;
                case "royalroadl":
                case "baka-tsuki":
                    modeComboBox.Items.Add("Table of Contents");
                    break;
                case "lnmtl":
                    modeComboBox.Items.Add("Next Chapter Link");
                    break;
                default:
                    modeComboBox.Items.Add("Table of Contents");
                    break;
            }

            modeComboBox.SelectedIndex = 0;
        }

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = (string)modeComboBox.SelectedItem;

            switch (type.ToLower())
            {
                case "table of contents":
                    modeSelectedLabel.Text = "TOC URL";

                    amountLabel.Visible = false;
                    amountNumericUpDown.Visible = false;
                    break;
                case "next chapter link":
                    modeSelectedLabel.Text = "Starting Chapter URL";

                    amountLabel.Visible = true;
                    amountNumericUpDown.Visible = true;
                    break;
            }

            Size textSize = TextRenderer.MeasureText(modeSelectedLabel.Text, modeSelectedLabel.Font);

            modeSelectedLabel.Location = new Point(modeSelectedTextBox.Location.X - textSize.Width - 10,
                modeSelectedTextBox.Location.Y);
        }
    }
}
