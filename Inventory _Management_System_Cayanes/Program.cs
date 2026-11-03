using System;
using System.Windows.Forms;
namespace Inventory_Management_System_Cayanes
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form1(loginForm.LoggedInUser));
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
