using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tommynulled
{
    static class Program
    {

        static string name = "jigsaw"; // application name. right above the blurred text aka the secret on the licenses tab among other tabs
        static string ownerid = "ZXS8NfrGIw"; // ownerid, found in account settings. click your profile picture on top right of dashboard and then account settings.
        static string secret = "19bd3f5d851a74bba39babdf87f2f1c4e545533b9bca11e0666cd922b6070a3c"; // app secret, the blurred text on licenses tab and other tabs
        static string version = "1.0"; // leave alone unless you've changed version on website

        public static api KeyAuthApp = new api(name, ownerid, secret, version);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            KeyAuthApp.init();
            if (Properties.Settings.Default.remember == "true")
            {
                if (KeyAuthApp.login(Properties.Settings.Default.user, Properties.Settings.Default.pass))
                {
                    Application.Run(new Main());
                }
                else { Properties.Settings.Default.user = ""; Properties.Settings.Default.pass = ""; Properties.Settings.Default.remember = ""; Properties.Settings.Default.Save(); Application.Run(new Auth()); }

            }
            else
            {
                Application.Run(new Auth());
            }
        }
    }
}
