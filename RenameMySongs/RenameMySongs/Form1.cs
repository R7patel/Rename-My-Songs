using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenameMySongs
{
    public partial class Form1 : Form
    {
        DirectoryInfo di;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            di= new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            this.Text = AppDomain.CurrentDomain.FriendlyName;
           // di = new DirectoryInfo(@"D:\BVM\#NET\codes\test\");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            foreach(FileInfo fi in di.GetFiles())
            {
                if(fi.Name.Any(c => char.IsDigit(c)))
                {
                    checkedListBox1.Items.Add(fi, true);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nm="",err="";
            foreach(FileInfo fi in checkedListBox1.CheckedItems)
            {
                try
                {
                    fi.MoveTo(di.FullName + Regex.Replace(fi.Name.ToString(), @"[\d-]", string.Empty));
                }
                catch (Exception e1)
                {
                    nm += fi.Name + "\n";
                    err = e1.ToString();
                }

            }
            if(nm!="")
             MessageBox.Show("Cannot Rename::" + nm + "\nPossibly beacuse the file already exixt with name of the intended destination filename."+"\n\n"+err);
        }
    }
}
