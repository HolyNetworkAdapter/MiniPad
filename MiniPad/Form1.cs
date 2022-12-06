using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiniPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = false;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = false;
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDlg.Font;
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                richTextBox1.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
            } else
            {
                richTextBox1.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MiniPad version 1.0, 6 December 2022\nThis program is licensed under MIT.\n\n(c) holynetworkadapter.fun 2022", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void languageSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language l = new Language();
            l.Show();
        }

        private string GetLine(string fn, int sn)
        {
            int lineCount = 0;

            using (StreamReader sr = new StreamReader(fn))
            {
                var line = sr.ReadLine();

                while (line != null)
                {
                    lineCount++;
                    if (lineCount == sn)
                    {
                        return line;
                    }
                    line = sr.ReadLine();
                }

                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.json"))
            {
                try
                {
                    string conf = File.ReadAllText("./config.json");
                    string jdata = conf;
                    dynamic data = JObject.Parse(jdata);
                    if (File.Exists("./lang/" + data.lang + ".lang"))
                    {
                        this.Text = GetLine("./lang/" + data.lang + ".lang", 5) + " - MiniPad";
                        fileToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 6);
                        viewToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 7);
                        programToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 8);
                        helpToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 9);
                        openToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 10);
                        saveToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 11);
                        exitToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 12);
                        fontToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 13);
                        wordWrapToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 14);
                        languageSettingsToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 16);
                        aboutToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 17);
                        readOnlyToolStripMenuItem.Text = GetLine("./lang/" + data.lang + ".lang", 24);
                    }
                    else
                    {
                        MessageBox.Show("Missing language file " + data.lang + ".lang, MiniPad will not start.");
                        Environment.Exit(1);
                    }
                } catch (JsonReaderException ex)
                {
                    MessageBox.Show("Invalid configuration, please check config.json and make sure syntax is correct.\n" + ex.ToString());
                    Environment.Exit(1);
                }
            } else
            {
                MessageBox.Show("Missing configuration file config.json, MiniPad will not start.");
                Environment.Exit(1);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.Title = "Save file";
            SaveFileDialog1.DefaultExt = "txt";
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.ShowDialog();
            try
            {
                System.IO.File.WriteAllText(SaveFileDialog1.FileName, richTextBox1.Text);
            } catch (ArgumentException)
            {
                // nothing
            }
            this.Text = saveFileDialog1.FileName + " - MiniPad";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open file";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
            try
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            } catch (ArgumentException)
            {
                // nothing
            }
            this.Text = openFileDialog1.FileName + " - MiniPad";
        }

        private void readOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (readOnlyToolStripMenuItem.Checked == true)
            {
                richTextBox1.ReadOnly = false;
                readOnlyToolStripMenuItem.Checked = false;
            }
            else
            {
                richTextBox1.ReadOnly = true;
                readOnlyToolStripMenuItem.Checked = true;
            }
        }
    }
}