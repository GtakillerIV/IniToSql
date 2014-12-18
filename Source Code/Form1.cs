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

// 29/8/2014 6:21 PM
// Added the ability to choose the save directory
// Added hours/mins/secs in the elapsed time label(label6).



using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace IniToSQL
{
    public partial class Form1 : Form
    {
        Parser parser = new Parser();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void BTN_Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                parser.SetDirectory(di);

                parser.ParseDirectory(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Table name" || textBox1.TextLength == 0)
            {
                MessageBox.Show(null, "Enter in a proper table name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                textBox1.Enabled = false;

                parser.Start(this);

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            parser.Stop();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) textBox2.Enabled = false;
            else textBox2.Enabled = true;
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label.Length == 0) MessageBox.Show("Field can't be empty!");
            else parser.columns[e.Item] = e.Label;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {

                FileInfo fi = new FileInfo(saveFileDialog1.FileName);

                parser.SetOutputDirectory(new DirectoryInfo(fi.FullName));

                textBox3.Text = fi.DirectoryName + @"\" + fi.Name;

                button1.Enabled = true;
                textBox1.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                
            }
        }
    }
}
