namespace Inventory_Management_System_Cayanes
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.accountTabPage = new System.Windows.Forms.TabPage();
            this.generalTabPage = new System.Windows.Forms.TabPage();

            // Account Tab
            this.userInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.userIdLabel = new System.Windows.Forms.Label();
            this.userIdValueLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameValueLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailValueLabel = new System.Windows.Forms.Label();
            this.roleLabel = new System.Windows.Forms.Label();
            this.roleValueLabel = new System.Windows.Forms.Label();

            this.changePasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.currentPasswordLabel = new System.Windows.Forms.Label();
            this.currentPasswordTextBox = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.changePasswordButton = new System.Windows.Forms.Button();

            this.changeUsernameGroupBox = new System.Windows.Forms.GroupBox();
            this.newUsernameLabel = new System.Windows.Forms.Label();
            this.newUsernameTextBox = new System.Windows.Forms.TextBox();
            this.changeUsernameButton = new System.Windows.Forms.Button();

            // General Tab
            this.infoLabel = new System.Windows.Forms.Label();

            // Bottom buttons
            this.logoutButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.accountTabPage.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.userInfoGroupBox.SuspendLayout();
            this.changePasswordGroupBox.SuspendLayout();
            this.changeUsernameGroupBox.SuspendLayout();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Controls.Add(this.accountTabPage);
            this.tabControl.Controls.Add(this.generalTabPage);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(360, 380);
            this.tabControl.TabIndex = 0;

            // accountTabPage
            this.accountTabPage.Controls.Add(this.userInfoGroupBox);
            this.accountTabPage.Controls.Add(this.changeUsernameGroupBox);
            this.accountTabPage.Controls.Add(this.changePasswordGroupBox);
            this.accountTabPage.Location = new System.Drawing.Point(4, 22);
            this.accountTabPage.Name = "accountTabPage";
            this.accountTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.accountTabPage.Size = new System.Drawing.Size(352, 354);
            this.accountTabPage.TabIndex = 0;
            this.accountTabPage.Text = "Account";
            this.accountTabPage.UseVisualStyleBackColor = true;

            // userInfoGroupBox
            this.userInfoGroupBox.Controls.Add(this.roleValueLabel);
            this.userInfoGroupBox.Controls.Add(this.roleLabel);
            this.userInfoGroupBox.Controls.Add(this.emailValueLabel);
            this.userInfoGroupBox.Controls.Add(this.emailLabel);
            this.userInfoGroupBox.Controls.Add(this.usernameValueLabel);
            this.userInfoGroupBox.Controls.Add(this.usernameLabel);
            this.userInfoGroupBox.Controls.Add(this.userIdValueLabel);
            this.userInfoGroupBox.Controls.Add(this.userIdLabel);
            this.userInfoGroupBox.Location = new System.Drawing.Point(6, 6);
            this.userInfoGroupBox.Name = "userInfoGroupBox";
            this.userInfoGroupBox.Size = new System.Drawing.Size(340, 100);
            this.userInfoGroupBox.TabIndex = 0;
            this.userInfoGroupBox.TabStop = false;
            this.userInfoGroupBox.Text = "Account Information";

            // Labels for user info
            this.userIdLabel.AutoSize = true;
            this.userIdLabel.Location = new System.Drawing.Point(6, 20);
            this.userIdLabel.Name = "userIdLabel";
            this.userIdLabel.Size = new System.Drawing.Size(50, 13);
            this.userIdLabel.TabIndex = 0;
            this.userIdLabel.Text = "User ID:";

            this.userIdValueLabel.AutoSize = true;
            this.userIdValueLabel.Location = new System.Drawing.Point(110, 20);
            this.userIdValueLabel.Name = "userIdValueLabel";
            this.userIdValueLabel.Size = new System.Drawing.Size(0, 13);
            this.userIdValueLabel.TabIndex = 1;

            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(6, 37);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Username:";

            this.usernameValueLabel.AutoSize = true;
            this.usernameValueLabel.Location = new System.Drawing.Point(110, 37);
            this.usernameValueLabel.Name = "usernameValueLabel";
            this.usernameValueLabel.Size = new System.Drawing.Size(0, 13);
            this.usernameValueLabel.TabIndex = 3;

            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(6, 54);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(35, 13);
            this.emailLabel.TabIndex = 4;
            this.emailLabel.Text = "Email:";

            this.emailValueLabel.AutoSize = true;
            this.emailValueLabel.Location = new System.Drawing.Point(110, 54);
            this.emailValueLabel.Name = "emailValueLabel";
            this.emailValueLabel.Size = new System.Drawing.Size(0, 13);
            this.emailValueLabel.TabIndex = 5;

            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(6, 71);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(32, 13);
            this.roleLabel.TabIndex = 6;
            this.roleLabel.Text = "Role:";

            this.roleValueLabel.AutoSize = true;
            this.roleValueLabel.Location = new System.Drawing.Point(110, 71);
            this.roleValueLabel.Name = "roleValueLabel";
            this.roleValueLabel.Size = new System.Drawing.Size(0, 13);
            this.roleValueLabel.TabIndex = 7;

            // changeUsernameGroupBox
            this.changeUsernameGroupBox.Controls.Add(this.changeUsernameButton);
            this.changeUsernameGroupBox.Controls.Add(this.newUsernameTextBox);
            this.changeUsernameGroupBox.Controls.Add(this.newUsernameLabel);
            this.changeUsernameGroupBox.Location = new System.Drawing.Point(6, 112);
            this.changeUsernameGroupBox.Name = "changeUsernameGroupBox";
            this.changeUsernameGroupBox.Size = new System.Drawing.Size(340, 65);
            this.changeUsernameGroupBox.TabIndex = 1;
            this.changeUsernameGroupBox.TabStop = false;
            this.changeUsernameGroupBox.Text = "Change Username";

            this.newUsernameLabel.AutoSize = true;
            this.newUsernameLabel.Location = new System.Drawing.Point(6, 20);
            this.newUsernameLabel.Name = "newUsernameLabel";
            this.newUsernameLabel.Size = new System.Drawing.Size(82, 13);
            this.newUsernameLabel.TabIndex = 0;
            this.newUsernameLabel.Text = "New Username:";

            this.newUsernameTextBox.Location = new System.Drawing.Point(110, 17);
            this.newUsernameTextBox.Name = "newUsernameTextBox";
            this.newUsernameTextBox.Size = new System.Drawing.Size(220, 20);
            this.newUsernameTextBox.TabIndex = 1;
            this.newUsernameTextBox.UseSystemPasswordChar = false;

            this.changeUsernameButton.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.changeUsernameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeUsernameButton.ForeColor = System.Drawing.Color.White;
            this.changeUsernameButton.Location = new System.Drawing.Point(255, 40);
            this.changeUsernameButton.Name = "changeUsernameButton";
            this.changeUsernameButton.Size = new System.Drawing.Size(75, 23);
            this.changeUsernameButton.TabIndex = 2;
            this.changeUsernameButton.Text = "Change";
            this.changeUsernameButton.UseVisualStyleBackColor = false;
            this.changeUsernameButton.Click += new System.EventHandler(this.changeUsernameButton_Click);

            // changePasswordGroupBox
            this.changePasswordGroupBox.Controls.Add(this.changePasswordButton);
            this.changePasswordGroupBox.Controls.Add(this.confirmPasswordTextBox);
            this.changePasswordGroupBox.Controls.Add(this.confirmPasswordLabel);
            this.changePasswordGroupBox.Controls.Add(this.newPasswordTextBox);
            this.changePasswordGroupBox.Controls.Add(this.newPasswordLabel);
            this.changePasswordGroupBox.Controls.Add(this.currentPasswordTextBox);
            this.changePasswordGroupBox.Controls.Add(this.currentPasswordLabel);
            this.changePasswordGroupBox.Location = new System.Drawing.Point(6, 183);
            this.changePasswordGroupBox.Name = "changePasswordGroupBox";
            this.changePasswordGroupBox.Size = new System.Drawing.Size(340, 158);
            this.changePasswordGroupBox.TabIndex = 2;
            this.changePasswordGroupBox.TabStop = false;
            this.changePasswordGroupBox.Text = "Change Password";

            this.currentPasswordLabel.AutoSize = true;
            this.currentPasswordLabel.Location = new System.Drawing.Point(6, 20);
            this.currentPasswordLabel.Name = "currentPasswordLabel";
            this.currentPasswordLabel.Size = new System.Drawing.Size(94, 13);
            this.currentPasswordLabel.TabIndex = 0;
            this.currentPasswordLabel.Text = "Current Password:";

            this.currentPasswordTextBox.Location = new System.Drawing.Point(110, 17);
            this.currentPasswordTextBox.Name = "currentPasswordTextBox";
            this.currentPasswordTextBox.Size = new System.Drawing.Size(220, 20);
            this.currentPasswordTextBox.TabIndex = 1;
            this.currentPasswordTextBox.UseSystemPasswordChar = true;

            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.Location = new System.Drawing.Point(6, 46);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(81, 13);
            this.newPasswordLabel.TabIndex = 2;
            this.newPasswordLabel.Text = "New Password:";

            this.newPasswordTextBox.Location = new System.Drawing.Point(110, 43);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.Size = new System.Drawing.Size(220, 20);
            this.newPasswordTextBox.TabIndex = 3;
            this.newPasswordTextBox.UseSystemPasswordChar = true;

            this.confirmPasswordLabel.AutoSize = true;
            this.confirmPasswordLabel.Location = new System.Drawing.Point(6, 72);
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(94, 13);
            this.confirmPasswordLabel.TabIndex = 4;
            this.confirmPasswordLabel.Text = "Confirm Password:";

            this.confirmPasswordTextBox.Location = new System.Drawing.Point(110, 69);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(220, 20);
            this.confirmPasswordTextBox.TabIndex = 5;
            this.confirmPasswordTextBox.UseSystemPasswordChar = true;

            this.changePasswordButton.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.changePasswordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changePasswordButton.ForeColor = System.Drawing.Color.White;
            this.changePasswordButton.Location = new System.Drawing.Point(255, 97);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(75, 23);
            this.changePasswordButton.TabIndex = 6;
            this.changePasswordButton.Text = "Change";
            this.changePasswordButton.UseVisualStyleBackColor = false;
            this.changePasswordButton.Click += new System.EventHandler(this.changePasswordButton_Click);

            // generalTabPage
            this.generalTabPage.Controls.Add(this.infoLabel);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalTabPage.Size = new System.Drawing.Size(352, 354);
            this.generalTabPage.TabIndex = 1;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;

            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(10, 20);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(300, 65);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Inventory Management System v1.0.0\r\n\r\nAdministration Panel\r\n\r\nThis is the settings panel where you can manage your account settings.";

            // logoutButton
            this.logoutButton.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.logoutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutButton.ForeColor = System.Drawing.Color.White;
            this.logoutButton.Location = new System.Drawing.Point(12, 410);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 1;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);

            // closeButton
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(297, 410);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);

            // SettingsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 445);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.accountTabPage.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.userInfoGroupBox.ResumeLayout(false);
            this.userInfoGroupBox.PerformLayout();
            this.changePasswordGroupBox.ResumeLayout(false);
            this.changePasswordGroupBox.PerformLayout();
            this.changeUsernameGroupBox.ResumeLayout(false);
            this.changeUsernameGroupBox.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage accountTabPage;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.GroupBox userInfoGroupBox;
        private System.Windows.Forms.Label userIdLabel;
        private System.Windows.Forms.Label userIdValueLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label usernameValueLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label emailValueLabel;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.Label roleValueLabel;
        private System.Windows.Forms.GroupBox changePasswordGroupBox;
        private System.Windows.Forms.Label currentPasswordLabel;
        private System.Windows.Forms.TextBox currentPasswordTextBox;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.Label confirmPasswordLabel;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.GroupBox changeUsernameGroupBox;
        private System.Windows.Forms.Label newUsernameLabel;
        private System.Windows.Forms.TextBox newUsernameTextBox;
        private System.Windows.Forms.Button changeUsernameButton;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button closeButton;
    }
}
