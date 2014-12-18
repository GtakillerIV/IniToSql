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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace IniToSQL
{

    public class Parser
    {

        private bool stoppedThread = false, containsSpaces;
        private int MAX_INSERTS = 270, accCount = 0;
        public List<string> columns = new List<string>();
        private DirectoryInfo dir, outputDir;
        private BackgroundWorker thread1 = new BackgroundWorker();
        private notification noti = new notification();

        public Parser()
        {
            thread1.DoWork += new DoWorkEventHandler(thread1_DoWork);
            thread1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread1_Completed);
            thread1.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// Sets current working directory for the parser.
        /// </summary>
        /// <param name="dir">The directory.</param>
        public void SetDirectory(DirectoryInfo dir)
        {
            this.dir = dir;
        }

        /// <summary>
        /// Sets current output directory for the parser.
        /// </summary>
        /// <param name="dir">The full directory inculding the file name.</param>
        public void SetOutputDirectory(DirectoryInfo dir)
        {
            this.outputDir = dir;
        }

        /// <summary>
        /// Opens a directory and updates the GUI of the program with new information.
        /// </summary>
        /// <param name="form">An instance of Form1</param>
        /// <returns></returns>
        public void ParseDirectory(Form1 form)
        {
            //Open direcotry
            //Loop through ini files
            //convert

            Reset(form);

            int columnsCount = 0, accsDone = 0;
            bool studiedAcc = false, checkedSpaces = false;
            columns.Clear();

            accCount = Directory.GetFiles(dir.FullName, "*.ini").Length;

            form.TXTBX_Directory.Text = dir.FullName;

            form.label1.Text = "Found a total of " + accCount + " Account(s)";

             //Loop through all .ini files in directory
            foreach (var fi in dir.GetFiles("*.ini"))
            {

                if (studiedAcc == false)
                {
                    var lines = File.ReadLines(fi.FullName);

                    foreach (var line in lines)
                    {
                        if (Regex.Match(line, @"^[A-Za-z0-9]").Success)
                        {
                            var field = line.Split('=');
                               
                            columnsCount++;
                            field[0] = field[0].Replace(" ", string.Empty);
                            form.listView1.Items.Add(field[0]);
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

                if (accsDone < 21)
                {
                    form.listBox1.Items.Add(fi.Name);
                }
                else
                {
                    //Why continue if there's really nothing else left to do?
                    //Waste of time + resources
                    form.listBox1.Items.Add("+" + (accCount-20) + " more");
                    accsDone = accCount;
                    form.progressBar1.Value = 100;
                    break;
                }
                accsDone++;
                float val = ((float)accsDone / (float)accCount) * 100;
                //Set progressbar value
                form.progressBar1.Value = (int)Math.Ceiling(val); ;
            }

            form.label2.Text = columnsCount + " columns Found";
            form.label5.Text = "0/" + accCount + " files done";

            //Enable the second browse button to allow the user to specify the directory he wants the output to be in
            form.button3.Enabled = true;
                
        }

        /// <summary>
        /// Starts the converter.
        /// </summary>
        /// <param name="form">An instance of Form1</param>
        /// <returns></returns>
        public void Start(Form1 form)
        {
            thread1.RunWorkerAsync(form);
        }
        
        /// <summary>
        /// Stops the current conversion of accounts.
        /// </summary>
        public void Stop()
        {
            thread1.CancelAsync();
        }

        private void thread1_DoWork(object sender, DoWorkEventArgs e)
        {
            GenerateQuery((Form1)e.Argument);
        }

        private void thread1_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (stoppedThread) noti.label2.Text = "Conversion of accounts was interrupted!";
            else
            {
                int width = Screen.PrimaryScreen.WorkingArea.Right,
                    height = Screen.PrimaryScreen.WorkingArea.Height;

                noti.label2.Text = "Conversion of accounts has finished successfully!";

                if (!noti.Visible)
                {
                    noti.Location = new Point(width - noti.Size.Width, height - noti.Size.Height);
                    noti.Show();
                }
               
            }
        }

        private void Reset(Form1 form)
        {
            //Disable/enable buttons
            //Clear sections
            //Reset progressbar's value
            //Clear labels
            form.progressBar1.Value = 0;
            form.label6.Text = "";
            form.listView1.Items.Clear();
            form.listBox1.Items.Clear();
            form.listBox2.Items.Clear();

        }

        private void GenerateQuery(Form1 form)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int count = 0;
            bool containsUsername = true, addedUsername = false, Write = false;

            string query;

            if (!form.checkBox1.Checked && form.textBox2.Text != "Username field name")
            {
                containsUsername = false;
                form.checkBox1.Enabled = false;
                columns.Insert(0, form.textBox2.Text);
            }

            if (form.checkBox3.Checked)
            {
                query = "INSERT IGNORE INTO `" + form.textBox1.Text + "` (";
            }
            else
            {
                query = "INSERT INTO `" + form.textBox1.Text + "` (";
            }

            //Add all the columns into a single string
            for (int i = 0; i < columns.Count; i++)
            {
                query += "`" + columns[i] + "`" + ",";
            }

            //Remove extra trailing comma (,)
            query = query.Remove(query.Length - 1);

            query += ") VALUES ";

            //Create a new string to store the values
            string Finalquery = query + "(";

            StreamWriter file = new StreamWriter(outputDir.ToString());

            file.Write("/*\t\t*********************************************\r\n");
            file.Write("\t\t*											*\r\n");
            file.Write("\t\t*			Generated by Ini->Sql			*\r\n");
            file.Write("\t\t*			Created by GtakillerIV			*\r\n");
            file.Write("\t\t*											*\r\n");
            file.Write("\t\t**********************************************\r\n*/\r\n");

            int dataLine = 0;
            List<string> data = new List<string>();
            string Line = null;
            float perc = 0.0f;

            int accsDone = 0;
            int s;
            float f;
            #region loop through .ini file and generate query
            foreach (var fi in dir.GetFiles("*.ini"))
            {
                if (thread1.CancellationPending)
                {
                    stoppedThread = true;
                    break;
                }

                if (fi.Length == 0)// Is the .ini file empty?
                {
                    form.Invoke((MethodInvoker)delegate{
                        form.listBox2.Items.Add("Skipping " + fi.Name + " due to it being empty.");
                    });
                    count++;
                    accsDone++;
                    continue;
                }
                //Console.WriteLine("File: " + fi.Name);
                var lines = File.ReadLines(fi.FullName);

                foreach (var line in lines)
                {
                    if (Regex.Match(line, @"^[\w]").Success)
                    {
                        if (containsSpaces)
                        {
                            Line = line.Remove(0, line.IndexOf("=") + 2);
                        }
                        else
                        {
                            Line = line.Remove(0, line.IndexOf("=") + 1);
                        }
                        Line = Line.Trim();
                        Line = Line.Replace("\"", string.Empty);

                        // Ignore .ini comments

                        if (Line.Contains(";")) Line = Line.Remove(Line.IndexOf(";"));
                        else if (Line.Contains("#")) Line = Line.Remove(Line.IndexOf("#"));


                        Line = Line.Replace("\u0001", "");//Get rid of SOH

                        //Escape the line if it's not a float.
                        if (!float.TryParse(Line, out f)) Line = Regex.Escape(Line);

                        data.Add(Line);
                        dataLine++;
                    }
                }
                if (count < MAX_INSERTS)
                {
                    string Format = "";
                    if (form.checkBox2.Checked)
                    {
                        if ((!containsUsername && columns.Count > dataLine + 1) || (containsUsername && columns.Count > dataLine))
                        {
                            form.Invoke((MethodInvoker)delegate
                            {
                                form.listBox2.Items.Add("Skipping " + fi.Name + " due to invalid column count");
                            });

                            count++;
                            accsDone++;
                            Write = true;
                            continue;//Let's get out of here!
                        }

                    }
                    for (int i = 0; i < dataLine; i++)
                    {
                        if (!int.TryParse(data[i].ToString(), out s) && !float.TryParse(data[i].ToString(), out f))
                        {
                            if (!containsUsername && !addedUsername)
                            {
                                Format = string.Format("\"{0}\"", Regex.Escape(Path.GetFileNameWithoutExtension(fi.Name)));
                                Finalquery += Format + ",";
                                addedUsername = true;
                            }
                            Format = string.Format("\"{0}\"", data[i]);
                        }
                        else
                        {
                            Format = string.Format("{0}", data[i]);
                        }
                        Finalquery += Format + ",";
                    }

                    Finalquery = Finalquery.Remove(Finalquery.Length - 1);
                    if (count >= MAX_INSERTS - 1 || accCount - accsDone <= 1)
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
                if (!Write)
                {
                    file.WriteLine(Finalquery);
                }

                dataLine = 0;
                Finalquery = "(";
                data.Clear();
                accsDone++;
                Write = false;
                addedUsername = false;
                perc = (float)accsDone / (float)accCount * 100;

                form.Invoke((MethodInvoker)delegate
                {

                    form.label5.Text = accsDone + "/" + accCount + " Files done";
                    form.progressBar1.Value = (int)Math.Ceiling(perc);

                });

            }
            #endregion
            file.Close();

            stopWatch.Stop();

            TimeSpan time = stopWatch.Elapsed;

            form.Invoke((MethodInvoker)delegate {

                form.button1.Enabled = true;
                form.button2.Enabled = false;
                form.textBox1.Enabled = true;
                form.checkBox1.Enabled = true;
                form.progressBar1.Value = 0;
                
                form.label6.Text = String.Format("Finished in {0:00}:{1:00}:{2:00}:{3:00}", time.Hours, time.Minutes, time.Seconds,
                         time.Milliseconds);
            });

        }

    }
}
