/*
                        ██╗███╗   ██╗██╗     ██╗  ███████╗ ██████╗ ██╗     
                        ██║████╗  ██║██║     ╚██╗ ██╔════╝██╔═══██╗██║     
                        ██║██╔██╗ ██║██║█████╗╚██╗███████╗██║   ██║██║     
                        ██║██║╚██╗██║██║╚════╝██╔╝╚════██║██║▄▄ ██║██║     
                        ██║██║ ╚████║██║     ██╔╝ ███████║╚██████╔╝███████╗
                        ╚═╝╚═╝  ╚═══╝╚═╝     ╚═╝  ╚══════╝ ╚══▀▀═╝ ╚══════╝
                                 
                    The MIT License (MIT)

                    Copyright (c) 2014 GtakillerIV

                    Permission is hereby granted, free of charge, to any person obtaining a copy
                    of this software and associated documentation files (the "Software"), to deal
                    in the Software without restriction, including without limitation the rights
                    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
                    copies of the Software, and to permit persons to whom the Software is
                    furnished to do so, subject to the following conditions:

                    The above copyright notice and this permission notice shall be included in all
                    copies or substantial portions of the Software.

                    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
                    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
                    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
                    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
                    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
                    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
                    SOFTWARE.
                                                   
*/


using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace IniToSQL
{
    public partial class Form1 : Form
    {
        string query;
        public const int MAX_INSERTS = 270;
        int count = 0;
        bool containsSpaces = false;

        ArrayList columns = new ArrayList();
        int accCount;


        DirectoryInfo di;

        public Form1()
        {
            InitializeComponent();
        }

        private void BTN_Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                button1.Enabled = false;
                string dir = folderBrowserDialog1.SelectedPath;
                TXTBX_Directory.Text = dir;
                this.Invoke(new MethodInvoker(delegate { progressBar1.Value = 0; }));
                dir = folderBrowserDialog1.SelectedPath;
                di = new DirectoryInfo(@dir);
                accCount = Directory.GetFiles(dir, "*.ini").Length;
                label5.Text = "0/" + accCount + " Files done";
                label1.Text = "Found a total of " + Directory.GetFiles(dir, "*.ini").Length.ToString() + " Account(s)";
                listBox1.Items.Clear();
                listBox3.Items.Clear();
                columns.Clear();
                int accsDone = 0;
                float perc = 0.0f ;
                int columnsCount = 0;

                bool checkedSpaces = false, studiedAcc = false;
               
                foreach (var fi in di.GetFiles("*.ini"))
                {
                    if (studiedAcc == false)
                    {
                        var lines = File.ReadLines(fi.FullName);

                        foreach (var line in lines)
                        {
                            Match match = Regex.Match(line, @"^[A-Za-z0-9]");

                            if (match.Success)
                            {
                                var field = line.Split('=');
                               
                                columnsCount++;
                                field[0] = field[0].Replace(" ", string.Empty);
                                listBox3.Items.Add(field[0]);
                                columns.Add(field[0]);

                                
                                if (checkedSpaces == false)
                                {
                                    if (line[line.IndexOf("=") + 1] == ' ') containsSpaces = true;
                                    checkedSpaces = true;
                                }
                            }

                        }
                        studiedAcc = true;
                    }
                    perc = (float)accsDone/(float)accCount * 100;
                    this.Invoke(new MethodInvoker(delegate { progressBar1.Value = (int)Math.Ceiling(perc); }));
                   
                    listBox1.Items.Add(fi.Name);
                    accsDone++;                    
                }
                label2.Text = columnsCount + " columns Found";
                button1.Enabled = true;
                textBox1.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GenerateQuery(columns);
        }
        
        private void GenerateQuery(ArrayList columns)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool containsUsername = true, addedUsername = false, dontWrite = false;
            this.Invoke(new MethodInvoker(delegate
            {
                progressBar1.Value = 0;
                listBox2.Items.Clear();

                if (!checkBox1.Checked && textBox2.Text != "Username field name")
                {
                    containsUsername = false;
                    checkBox1.Enabled = false;
                    columns.Insert(0, textBox2.Text);
                }

                if (checkBox3.Checked)
                {
                    query = "INSERT IGNORE INTO `" + textBox1.Text + "` (";
                }
                else
                {
                    query = "INSERT INTO `" + textBox1.Text + "` (";
                }
            }));

            for (int i = 0; i < columns.Count; i++)
            {
                query += "`" + columns[i] + "`" + ",";
            }

            query = query.Remove(query.Length - 1);

            query += ") VALUES ";

            string Finalquery = query + "(";           

            StreamWriter file = new StreamWriter("Query.sql");

            file.Write("/*\t\t*********************************************\r\n");
			file.Write("\t\t*											*\r\n");
			file.Write("\t\t*			Generated by Ini->Sql			*\r\n");
            file.Write("\t\t*			Created by GtakillerIV			*\r\n");
			file.Write("\t\t*											*\r\n");
            file.Write("\t\t**********************************************\r\n*/\r\n");

            int dataLine = 0;
            Match match;
            ArrayList data = new ArrayList();
            string Line = null;
            float perc = 0.0f;

            int accsDone = 0;
            int s;
            foreach (var fi in di.GetFiles("*.ini"))
            {
                if (backgroundWorker1.CancellationPending) break;
                if (fi.Length == 0)// Is the .ini file empty?
                {
                    this.Invoke(new MethodInvoker(delegate { listBox2.Items.Add("Skipping" + fi.Name + " due to it being empty."); }));
                    count++;
                    accsDone++;
                    continue; 
                }
                //Console.WriteLine("File: " + fi.Name);
                var lines = File.ReadLines(fi.FullName);

                foreach (var line in lines)
                {
                    match = Regex.Match(line, @"^[\w]");
                    if (match.Success)
                    {
                        if (containsSpaces)
                        {
                            Line = line.Remove(0, line.IndexOf("=") + 2);
                        }
                        else
                        {
                            Line = line.Remove(0, line.IndexOf("=") + 1);
                        }
                        
                        data.Add(Line);
                        dataLine++;
                    }
                }
                if (count < MAX_INSERTS)
                {
                    string Format = "";
                    if (checkBox2.Checked)
                    {
                        if (!containsUsername && columns.Count > dataLine+1)
                        {
                            this.Invoke(new MethodInvoker(delegate { listBox2.Items.Add("Skipping " + fi.Name + " due to invalid column count"); }));
                            count++;
                            accsDone++;
                            dontWrite = true;
                            continue;//Let's get out of here!
                        }
                        else if(containsUsername && columns.Count > dataLine)
                        {
                            this.Invoke(new MethodInvoker(delegate { listBox2.Items.Add("Skipping " + fi.Name + " due to invalid column count"); }));
                            count++;
                            accsDone++;
                            dontWrite = true;
                            continue;
                        }
                        
                    }
                    for (int i = 0; i < dataLine; i++)
                    {
                        data[i] = data[i].ToString().Replace("\u0001", "");//Get rid of SOH

                        if(!int.TryParse(data[i].ToString(), out s ))
                        {
                            if (!containsUsername && !addedUsername)
                            {
                                Format = string.Format("'{0}'", Path.GetFileNameWithoutExtension(fi.Name));
                                Finalquery += Format + ",";
                                addedUsername = true;
                            }
                            Format = string.Format("'{0}'", data[i]);
                        }
                        else
                        {
                            Format = string.Format("{0}", data[i]);
                        }
                        Finalquery += Format + ",";
                    }

                    Finalquery = Finalquery.Remove(Finalquery.Length - 1);
                    if (count >= MAX_INSERTS-1 || accCount-accsDone <= 1)
                    {
                        Finalquery += ");";
                    }
                    else
                    {
                        Finalquery += "),";
                    }
                    count++;
                }
                else
                {
                    count = 0;
                    Finalquery = query;
                }
                if (!dontWrite)
                {
                    file.WriteLine(Finalquery);
                }
                dataLine = 0;
                Finalquery = "(";
                data.Clear();
                accsDone++;
                dontWrite = false;
                addedUsername = false;
                this.Invoke(new MethodInvoker(delegate { label5.Text = accsDone + "/" + accCount + " Files done"; }));
                perc = (float)accsDone / (float)accCount * 100;
                this.Invoke(new MethodInvoker(delegate { progressBar1.Value = (int)Math.Ceiling(perc); }));
            }
            file.Close();

            stopWatch.Stop();

            this.Invoke(new MethodInvoker(delegate
                {

                    notification noti = new notification();
                    int width = Screen.PrimaryScreen.WorkingArea.Right;
                    int height = Screen.PrimaryScreen.WorkingArea.Height;

                    if (backgroundWorker1.CancellationPending) noti.label2.Text = "Conversion of accounts was interrupted!";
                    else noti.label2.Text = "Conversion of accounts has finished successfully!";

                    noti.Location = new Point(width - noti.Size.Width, height - noti.Size.Height);
                    noti.Show();

                    button1.Enabled = true;
                    button2.Enabled = false;
                    textBox1.Enabled = true;
                    checkBox1.Enabled = true;
                    progressBar1.Value = 0;
                    label6.Text = "Finished in " + stopWatch.ElapsedMilliseconds + " ms";

                }));
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Table name")
            {
                MessageBox.Show(null, "Enter in a table name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                textBox1.Enabled = false;

                backgroundWorker1.RunWorkerAsync();

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Logo_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) textBox2.Enabled = false;
            else textBox2.Enabled = true;
        }
    }
}
