using System;
using System.Windows.Forms;
using Inventory_Management_System_Cayanes.Models;
using Inventory_Management_System_Cayanes.Services;

namespace Inventory_Management_System_Cayanes
{
    public partial class SettingsForm : Form
    {
        private User _currentUser;
        private UserService _userService;
        private TransactionLogger _logger;

        public SettingsForm(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userService = new UserService();
            _logger = new TransactionLogger();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Settings - {_currentUser.Username}";
            LoadSettings();
        }

        private void LoadSettings()
        {
            usernameValueLabel.Text = _currentUser.Username;
            emailValueLabel.Text = _currentUser.Email;
            roleValueLabel.Text = _currentUser.Role;
            userIdValueLabel.Text = _currentUser.UserID.ToString();

            // Clear password fields
            currentPasswordTextBox.Clear();
            newPasswordTextBox.Clear();
            confirmPasswordTextBox.Clear();
            newUsernameTextBox.Clear();
        }

        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            if (!ValidatePasswordFields())
                return;

            try
            {
                string currentPassword = currentPasswordTextBox.Text;
                string newPassword = newPasswordTextBox.Text;
                string confirmPassword = confirmPasswordTextBox.Text;

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    newPasswordTextBox.Focus();
                    return;
                }

                bool success = _userService.ChangePassword(_currentUser.UserID, currentPassword, newPassword);

                if (success)
                {
                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _logger.LogTransaction(_currentUser.UserID, "CHANGE_PASSWORD", "Users", _currentUser.UserID, 
                        "Password changed", "");
                    currentPasswordTextBox.Clear();
                    newPasswordTextBox.Clear();
                    confirmPasswordTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentPasswordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogFailedOperation(_currentUser.UserID, "CHANGE_PASSWORD", "Users", ex.Message);
            }
        }

        private void changeUsernameButton_Click(object sender, EventArgs e)
        {
            if (!ValidateUsernameFields())
                return;

            try
            {
                string newUsername = newUsernameTextBox.Text.Trim();

                if (newUsername == _currentUser.Username)
                {
                    MessageBox.Show("New username must be different from current username.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    newUsernameTextBox.Focus();
                    return;
                }

                bool success = _userService.ChangeUsername(_currentUser.UserID, newUsername);

                if (success)
                {
                    MessageBox.Show("Username changed successfully! You will need to login again with the new username.", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _logger.LogTransaction(_currentUser.UserID, "CHANGE_USERNAME", "Users", _currentUser.UserID, 
                        $"Username: {_currentUser.Username}", $"Username: {newUsername}");
                    _currentUser.Username = newUsername;
                    usernameValueLabel.Text = newUsername;
                    newUsernameTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    newUsernameTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing username: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogFailedOperation(_currentUser.UserID, "CHANGE_USERNAME", "Users", ex.Message);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _logger.LogUserLogout(_currentUser.UserID);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidatePasswordFields()
        {
            if (string.IsNullOrWhiteSpace(currentPasswordTextBox.Text))
            {
                MessageBox.Show("Current password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currentPasswordTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPasswordTextBox.Text))
            {
                MessageBox.Show("New password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newPasswordTextBox.Focus();
                return false;
            }

            if (newPasswordTextBox.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newPasswordTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Please confirm your new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                confirmPasswordTextBox.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateUsernameFields()
        {
            if (string.IsNullOrWhiteSpace(newUsernameTextBox.Text))
            {
                MessageBox.Show("New username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newUsernameTextBox.Focus();
                return false;
            }

            if (newUsernameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newUsernameTextBox.Focus();
                return false;
            }

            return true;
        }
    }
}
