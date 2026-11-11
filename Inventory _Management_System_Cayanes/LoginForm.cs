using System;
using System.Windows.Forms;
using Inventory_Management_System_Cayanes.Services;
using Inventory_Management_System_Cayanes.Models;
using Inventory_Management_System_Cayanes.Data;
namespace Inventory_Management_System_Cayanes
{
    public partial class LoginForm : Form
    {
        private UserService _userService;
        private TransactionLogger _logger;
        public User LoggedInUser { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
            _userService = new UserService();
            _logger = new TransactionLogger();
            StyleForm();
        }
        private void StyleForm()
        {
            this.Text = "Inventory Management System - Login";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 300);
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            string databasePath = DatabaseInitializer.ResolveDatabasePath("InventoryDB.accdb");
            if (!System.IO.File.Exists(databasePath))
            {
                MessageBox.Show("Database file not found at:\n" + databasePath,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernameTextBox.Focus();
                return;
            }

            try
            {
                DatabaseInitializer.InitializeDatabase(databasePath);
                var db = new DatabaseHelper(databasePath);
                using (db.GetConnection())
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to the database at:\n" + databasePath +
                    "\n\n" + ex.GetBaseException().Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            usernameTextBox.Focus();
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginUser();
        }
        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                LoginUser();
            }
        }
        private void LoginUser()
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", 
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                User user = _userService.AuthenticateUser(username, password);
                if (user != null)
                {
                    LoggedInUser = user;
                    _logger.LogUserLogin(user.UserID, true);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", 
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.LogFailedOperation(0, "LOGIN", "Users", "Invalid credentials");
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogFailedOperation(0, "LOGIN", "Users", ex.Message);
            }
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void titleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
