using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tommynulled
{
    public partial class Main : Form
    {

        static string name = "jigsaw"; // application name. right above the blurred text aka the secret on the licenses tab among other tabs
        static string ownerid = "ZXS8NfrGIw"; // ownerid, found in account settings. click your profile picture on top right of dashboard and then account settings.
        static string secret = "19bd3f5d851a74bba39babdf87f2f1c4e545533b9bca11e0666cd922b6070a3c"; // app secret, the blurred text on licenses tab and other tabs
        static string version = "1.0"; // leave alone unless you've changed version on website

        public static api KeyAuthApp = new api(name, ownerid, secret, version);

        public Main()
        {
            InitializeComponent();
            KeyAuthApp.init();
            // get version
            bunifuLabelVersion1.Text = Application.ProductVersion;
            // end get version
            // username in titlebar
            bunifuTitleBar1.Text = "DASHBOARD - " + Properties.Settings.Default.user;
            // end username in titlebar
        }

        private void bunifuAppBar1_IconClick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.user = "";
            Properties.Settings.Default.pass = "";
            Properties.Settings.Default.remember = "";
            Properties.Settings.Default.Save();
            if(Properties.Settings.Default.user == "")
            {
                Application.Restart();
            }
            else { iconButton1.PerformClick(); }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // start on windows
            if (Properties.Settings.Default.os == "true")
            {
                bunifuCheckBox1.Checked = true;
            }
            if (Properties.Settings.Default.os == "false")
            {
                bunifuCheckBox1.Checked = false;
            }
        }

        private void navigtionMenu1_OnItemSelected(object sender, string path, EventArgs e)
        {
            bunifuPages1.SetPage(path);
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (bunifuCheckBox1.Checked == true)
            {
                key.SetValue("tommynulled.exe", "\"" + Application.ExecutablePath + "\"");
                Properties.Settings.Default.os = "true";
                Properties.Settings.Default.Save();
            }
            if (bunifuCheckBox1 .Checked == false)
            {
                key.DeleteValue("tommynulled.exe");
                Properties.Settings.Default.os = "false";
                Properties.Settings.Default.Save();
            }
        }
    }
}
