using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Batch_Renamer
{
    public partial class Form1 : Form
    {
        List<string> filenameList;
        List<string> dirnameList;
        
        public Form1()
        {
            InitializeComponent();
            filenameList = new List<string>();
            dirnameList = new List<string>();
        }

        private string replaceInvalidChars(string stringSet)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                stringSet = stringSet.Replace(c.ToString(), "");
            }
            return stringSet;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            filenameList.Clear();
            dirnameList.Clear();

            FolderBrowserDialog brws = new FolderBrowserDialog();
            DialogResult rslt = brws.ShowDialog();
            if (rslt == DialogResult.OK)
            {
                textBox1.Text = brws.SelectedPath;

                filenameList.AddRange(Directory.EnumerateFiles(textBox1.Text));
                dirnameList.AddRange(Directory.EnumerateDirectories(textBox1.Text));


                foreach (string s in filenameList)
                {
                    richTextBox1.AppendText(s);
                    richTextBox1.AppendText("\n");
                }
                foreach (string s in dirnameList)
                {
                    richTextBox1.AppendText(s);
                    richTextBox1.AppendText("\n");
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            foreach (string s in filenameList)
            {
                string oldName = Path.GetFileName(s);
                if (oldName.Contains(textBox2.Text))
                {
                    string newName = oldName.Replace(textBox2.Text, textBox3.Text);
                    newName = replaceInvalidChars(newName); 
                    File.Move(path + "\\" + oldName, path + "\\" + newName);
                }
            }

            foreach (string s in dirnameList)
            {
                string dirNameOld = new DirectoryInfo(s).Name;
                if (dirNameOld.Contains(textBox2.Text))
                {
                    string dirNameNew = dirNameOld.Replace(textBox2.Text, textBox3.Text);
                    dirNameNew = replaceInvalidChars(dirNameNew); 
                    Directory.Move(path + "\\" + dirNameOld, path + "\\" + dirNameNew);
                }
            }
            filenameList = new List<string>();
            dirnameList = new List<string>();
            MessageBox.Show("All operations executed!");
        }
    }
}
