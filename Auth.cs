using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kimtoo.Preloader;
using Kimtoo.ValidationProvider;

namespace tommynulled
{
    public partial class Auth : Form
    {

        static string name = "jigsaw"; // application name. right above the blurred text aka the secret on the licenses tab among other tabs
        static string ownerid = "ZXS8NfrGIw"; // ownerid, found in account settings. click your profile picture on top right of dashboard and then account settings.
        static string secret = "19bd3f5d851a74bba39babdf87f2f1c4e545533b9bca11e0666cd922b6070a3c"; // app secret, the blurred text on licenses tab and other tabs
        static string version = "1.0"; // leave alone unless you've changed version on website

        public static api KeyAuthApp = new api(name, ownerid, secret, version);

        public Auth()
        {
            InitializeComponent();
            KeyAuthApp.init();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // close form
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonGroup1_OnSelectionChange(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(sender.ToString());
            if (bunifuPages1.PageName == "tabPage2") // register
            {
                bunifuButton21.Text = "REGISTER";
            }
            if (bunifuPages1.PageName == "tabPage1") // login
            {
                bunifuButton21.Text = "LOGIN";
            }
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            if (bunifuPages1.PageName == "tabPage1") // login code
            {
                if(bunifuCheckBox1.CheckState == Bunifu.UI.WinForms.BunifuCheckBox.CheckStates.Checked)
                {
                    Properties.Settings.Default.user = bunifuLoginUser.Text;
                    Properties.Settings.Default.pass = bunifuPassUser.Text;
                    Properties.Settings.Default.remember = "true";
                    Properties.Settings.Default.Save();
                }
                else { Properties.Settings.Default.user = ""; Properties.Settings.Default.pass = ""; Properties.Settings.Default.remember = ""; Properties.Settings.Default.Save(); }
                if (KeyAuthApp.login(bunifuLoginUser.Text, bunifuPassUser.Text))
                {
                    Properties.Settings.Default.user = bunifuLoginUser.Text;
                    Properties.Settings.Default.Save();
                    Preloader.ShowLoader(this, text: "Loading");
                    Thread.Sleep(5000);
                    Preloader.HideLoader(this);
                    Main main = new Main();
                    main.Show();
                    this.Hide();
                }
                else { bunifuSnackbar1.Show(this, Properties.Settings.Default.jsonmessage, Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); }
            }
            if(bunifuPages1.PageName == "tabPage2") // register code
            {
                if (KeyAuthApp.register(bunifuUserRegister.Text, bunifuPassRegister.Text, bunifuLicenseRegister.Text))
                {
                    bunifuSnackbar1.Show(this, "Account Created", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
                }
                else
                {
                    bunifuSnackbar1.Show(this, Properties.Settings.Default.jsonmessage, Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                }
            }
        }
    }
}
