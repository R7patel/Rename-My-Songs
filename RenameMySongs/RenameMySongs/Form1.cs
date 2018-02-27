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

        public static string rename(string s)
        {
            char[] token = { '.' };
            int i = 0;
            string[] arr1 = s.Split(token);
            string New_name = ""; 
            for(i=0;i<arr1.Length-1;i++)
            {
                New_name += Regex.Replace(arr1[i].ToString(), @"[\d-]", string.Empty);                
            }
            New_name += "."+ arr1[i];
            return New_name;
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
                    // fi.MoveTo(di.FullName + Regex.Replace(fi.Name.ToString(), @"[\d-]", string.Empty));
                    fi.MoveTo(di.FullName + rename(fi.Name.ToString()));
                }
                catch (Exception e1)
                {
                    nm += fi.Name + "\n";
                    err = e1.ToString();
                }

            }
            if(nm!="")
             MessageBox.Show("Cannot Rename::" + nm + "\nPossibly beacuse the file already exist with name of the intended destination filename."+"\n\n"+err);
        }
    }
}
