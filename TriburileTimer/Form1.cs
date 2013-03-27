using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using TriburileTimer.Properties;

namespace TriburileTimer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            done = notified = false;
            notified = true;
        }

        bool done, notified;

        private DateTime end;

        private void button1_Click(object sender, EventArgs e)
        {
            
            int h = Int32.Parse(comboBox1.Text);
            int m = Int32.Parse(comboBox2.Text);
            int s = Int32.Parse(comboBox3.Text);
            if (!radioButton2.Checked)
                end = DateTime.Now.Add(new TimeSpan(h, m, s));
            else
                end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, h, m, s);

            done = notified = false;
            
        }

        private void ticker_Tick(object sender, EventArgs e)
        {
            DEBUG.Text = TimeLeft(end.Subtract(DateTime.Now));

            if (!notified)
            {
                if (done)
                {
                    notifyIcon1.ShowBalloonTip(10000, "Timer!","Timer expired!",ToolTipIcon.None);

                    SoundPlayer t = new SoundPlayer(this.GetType().Assembly.GetManifestResourceStream("Ding"));
                    t.Play();
                    done = false;
                    notified = true;
                }
            }
        }

        private string TimeLeft(TimeSpan timp)
        {
            if (timp.Ticks < 0)
            {
                if (!notified)
                    done = true;
                return "DONE!";
            }
            long h,m,s,n;
            h=m=s=n=0;
            h = timp.Hours;
            m = timp.Minutes;
            s = timp.Seconds;
            n = timp.Milliseconds;
            string str="";
            if (h<10) str+="0";str+=h+" : ";
            if (m<10) str+="0";str+=m+" : ";
            if (s<10) str+="0";str+=s+" . ";
            if (n<100) str+="0";
            if (n<10)str+="0";
            str+=n+"";

            return str;
        }


        private void ExitM(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Open(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        private bool close = false;
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            close = true;
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox1.Text = DateTime.Now.Hour + "";
            this.comboBox2.Text = DateTime.Now.Minute + "";
            this.comboBox3.Text = DateTime.Now.Second + "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox1.Text = "00";
            this.comboBox2.Text = "00";
            this.comboBox3.Text = "00";
        }

        private void Closingh(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || close==true) {return; }
            e.Cancel = true;
            this.Hide();
        }
    }
}
