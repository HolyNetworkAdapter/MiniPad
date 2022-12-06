using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPad
{
    public partial class Language : Form
    {
        public Language()
        {
            InitializeComponent();
        }

        private void Language_Load(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    string conf = File.ReadAllText("./config.json");
                    string jdata = conf;
                    dynamic data = JObject.Parse(jdata);
                    if (File.Exists("./lang/" + data.lang + ".lang"))
                    {
                        this.Text = GetLine("./lang/" + data.lang + ".lang", 23);
                        label1.Text = GetLine("./lang/" + data.lang + ".lang", 18) + " " + GetLine("./lang/" + data.lang + ".lang", 3);
                        label2.Text = GetLine("./lang/" + data.lang + ".lang", 19) + " " + GetLine("./lang/" + data.lang + ".lang", 4);
                        label3.Text = GetLine("./lang/" + data.lang + ".lang", 21);
                        groupBox1.Text = GetLine("./lang/" + data.lang + ".lang", 20);
                        button1.Text = GetLine("./lang/" + data.lang + ".lang", 22);
                    }
                    else
                    {
                        MessageBox.Show("Missing language file " + data.lang + ".lang");
                        Environment.Exit(1);
                    }
                    string[] langs = Directory.GetFiles(@".\lang\");
                    foreach (string i in langs)
                    {
                        listBox1.Items.Add(i);
                    }
                } catch (JsonReaderException)
                {
                    Environment.Exit(1);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.ToString());
                Environment.Exit(1);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "config.json");
        }
    }
}
