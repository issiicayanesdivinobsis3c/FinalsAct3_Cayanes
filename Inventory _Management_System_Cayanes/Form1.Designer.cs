namespace Inventory_Management_System_Cayanes
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button categoryButton;
        private System.Windows.Forms.Button reportsButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label itemIDLabel;
        private System.Windows.Forms.TextBox itemIDTextBox;
        private System.Windows.Forms.Label itemCodeLabel;
        private System.Windows.Forms.TextBox itemCodeTextBox;
        private System.Windows.Forms.Label productNameLabel;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label unitPriceLabel;
        private System.Windows.Forms.TextBox unitPriceTextBox;
        private System.Windows.Forms.Label stockQuantityLabel;
        private System.Windows.Forms.TextBox stockQuantityTextBox;
        private System.Windows.Forms.Label reorderLevelLabel;
        private System.Windows.Forms.TextBox reorderLevelTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchIDTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DataGridView itemsDataGridView;
        private System.Windows.Forms.MenuStrip menuStrip;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.settingsButton = new System.Windows.Forms.Button();
            this.reportsButton = new System.Windows.Forms.Button();
            this.categoryButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.itemsDataGridView = new System.Windows.Forms.DataGridView();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchIDTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.reorderLevelTextBox = new System.Windows.Forms.TextBox();
            this.reorderLevelLabel = new System.Windows.Forms.Label();
            this.stockQuantityTextBox = new System.Windows.Forms.TextBox();
            this.stockQuantityLabel = new System.Windows.Forms.Label();
            this.unitPriceTextBox = new System.Windows.Forms.TextBox();
            this.unitPriceLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.productNameLabel = new System.Windows.Forms.Label();
            this.itemCodeTextBox = new System.Windows.Forms.TextBox();
            this.itemCodeLabel = new System.Windows.Forms.Label();
            this.itemIDTextBox = new System.Windows.Forms.TextBox();
            this.itemIDLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.sidebarPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDataGridView)).BeginInit();
            this.searchPanel.SuspendLayout();
            this.inputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(51)))));
            this.sidebarPanel.Controls.Add(this.settingsButton);
            this.sidebarPanel.Controls.Add(this.reportsButton);
            this.sidebarPanel.Controls.Add(this.categoryButton);
            this.sidebarPanel.Controls.Add(this.homeButton);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(129, 541);
            this.sidebarPanel.TabIndex = 0;
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.settingsButton.ForeColor = System.Drawing.Color.White;
            this.settingsButton.Location = new System.Drawing.Point(9, 139);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(111, 35);
            this.settingsButton.TabIndex = 3;
            this.settingsButton.Text = "SETTINGS";
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // reportsButton
            // 
            this.reportsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.reportsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportsButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.reportsButton.ForeColor = System.Drawing.Color.White;
            this.reportsButton.Location = new System.Drawing.Point(9, 95);
            this.reportsButton.Name = "reportsButton";
            this.reportsButton.Size = new System.Drawing.Size(111, 35);
            this.reportsButton.TabIndex = 2;
            this.reportsButton.Text = "REPORTS";
            this.reportsButton.UseVisualStyleBackColor = false;
            this.reportsButton.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // categoryButton
            // 
            this.categoryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.categoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.categoryButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.categoryButton.ForeColor = System.Drawing.Color.White;
            this.categoryButton.Location = new System.Drawing.Point(9, 52);
            this.categoryButton.Name = "categoryButton";
            this.categoryButton.Size = new System.Drawing.Size(111, 35);
            this.categoryButton.TabIndex = 1;
            this.categoryButton.Text = "CATEGORY";
            this.categoryButton.UseVisualStyleBackColor = false;
            this.categoryButton.Click += new System.EventHandler(this.categoryToolStripMenuItem_Click);
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.homeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.homeButton.ForeColor = System.Drawing.Color.White;
            this.homeButton.Location = new System.Drawing.Point(9, 9);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(111, 35);
            this.homeButton.TabIndex = 0;
            this.homeButton.Text = "HOME";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(200)))), ((int)(((byte)(192)))));
            this.mainPanel.Controls.Add(this.itemsDataGridView);
            this.mainPanel.Controls.Add(this.searchPanel);
            this.mainPanel.Controls.Add(this.inputPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(129, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(728, 541);
            this.mainPanel.TabIndex = 1;
            // 
            // itemsDataGridView
            // 
            this.itemsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.itemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsDataGridView.Location = new System.Drawing.Point(0, 251);
            this.itemsDataGridView.Name = "itemsDataGridView";
            this.itemsDataGridView.Size = new System.Drawing.Size(728, 290);
            this.itemsDataGridView.TabIndex = 2;
            this.itemsDataGridView.SelectionChanged += new System.EventHandler(this.itemsDataGridView_SelectionChanged);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(180)))), ((int)(((byte)(170)))));
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchIDTextBox);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 208);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(728, 43);
            this.searchPanel.TabIndex = 1;
            this.searchPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.searchPanel_Paint_1);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(51)))));
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(257, 10);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(69, 20);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "SEARCH";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchIDTextBox
            // 
            this.searchIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchIDTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchIDTextBox.Location = new System.Drawing.Point(77, 10);
            this.searchIDTextBox.Name = "searchIDTextBox";
            this.searchIDTextBox.Size = new System.Drawing.Size(172, 23);
            this.searchIDTextBox.TabIndex = 1;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.searchLabel.Location = new System.Drawing.Point(13, 13);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(71, 15);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "SEARCH ID:";
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(180)))), ((int)(((byte)(170)))));
            this.inputPanel.Controls.Add(this.clearButton);
            this.inputPanel.Controls.Add(this.deleteButton);
            this.inputPanel.Controls.Add(this.updateButton);
            this.inputPanel.Controls.Add(this.addButton);
            this.inputPanel.Controls.Add(this.reorderLevelTextBox);
            this.inputPanel.Controls.Add(this.reorderLevelLabel);
            this.inputPanel.Controls.Add(this.stockQuantityTextBox);
            this.inputPanel.Controls.Add(this.stockQuantityLabel);
            this.inputPanel.Controls.Add(this.unitPriceTextBox);
            this.inputPanel.Controls.Add(this.unitPriceLabel);
            this.inputPanel.Controls.Add(this.descriptionTextBox);
            this.inputPanel.Controls.Add(this.descriptionLabel);
            this.inputPanel.Controls.Add(this.categoryComboBox);
            this.inputPanel.Controls.Add(this.categoryLabel);
            this.inputPanel.Controls.Add(this.productNameTextBox);
            this.inputPanel.Controls.Add(this.productNameLabel);
            this.inputPanel.Controls.Add(this.itemCodeTextBox);
            this.inputPanel.Controls.Add(this.itemCodeLabel);
            this.inputPanel.Controls.Add(this.itemIDTextBox);
            this.inputPanel.Controls.Add(this.itemIDLabel);
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputPanel.Location = new System.Drawing.Point(0, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(728, 208);
            this.inputPanel.TabIndex = 0;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.clearButton.ForeColor = System.Drawing.Color.White;
            this.clearButton.Location = new System.Drawing.Point(587, 139);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(69, 26);
            this.clearButton.TabIndex = 19;
            this.clearButton.Text = "CLEAR";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(514, 139);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(69, 26);
            this.deleteButton.TabIndex = 18;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.updateButton.ForeColor = System.Drawing.Color.White;
            this.updateButton.Location = new System.Drawing.Point(441, 139);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(69, 26);
            this.updateButton.TabIndex = 17;
            this.updateButton.Text = "UPDATE";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(51)))));
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(369, 139);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(69, 26);
            this.addButton.TabIndex = 16;
            this.addButton.Text = "ADD";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // reorderLevelTextBox
            // 
            this.reorderLevelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reorderLevelTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reorderLevelTextBox.Location = new System.Drawing.Point(570, 78);
            this.reorderLevelTextBox.Name = "reorderLevelTextBox";
            this.reorderLevelTextBox.Size = new System.Drawing.Size(69, 23);
            this.reorderLevelTextBox.TabIndex = 15;
            this.reorderLevelTextBox.Text = "10";
            // 
            // reorderLevelLabel
            // 
            this.reorderLevelLabel.AutoSize = true;
            this.reorderLevelLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.reorderLevelLabel.Location = new System.Drawing.Point(570, 61);
            this.reorderLevelLabel.Name = "reorderLevelLabel";
            this.reorderLevelLabel.Size = new System.Drawing.Size(99, 15);
            this.reorderLevelLabel.TabIndex = 14;
            this.reorderLevelLabel.Text = "REORDER LEVEL:";
            // 
            // stockQuantityTextBox
            // 
            this.stockQuantityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stockQuantityTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.stockQuantityTextBox.Location = new System.Drawing.Point(480, 78);
            this.stockQuantityTextBox.Name = "stockQuantityTextBox";
            this.stockQuantityTextBox.Size = new System.Drawing.Size(77, 23);
            this.stockQuantityTextBox.TabIndex = 13;
            this.stockQuantityTextBox.Text = "0";
            // 
            // stockQuantityLabel
            // 
            this.stockQuantityLabel.AutoSize = true;
            this.stockQuantityLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.stockQuantityLabel.Location = new System.Drawing.Point(480, 61);
            this.stockQuantityLabel.Name = "stockQuantityLabel";
            this.stockQuantityLabel.Size = new System.Drawing.Size(111, 15);
            this.stockQuantityLabel.TabIndex = 12;
            this.stockQuantityLabel.Text = "STOCK QUANTITY:";
            // 
            // unitPriceTextBox
            // 
            this.unitPriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.unitPriceTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.unitPriceTextBox.Location = new System.Drawing.Point(369, 78);
            this.unitPriceTextBox.Name = "unitPriceTextBox";
            this.unitPriceTextBox.Size = new System.Drawing.Size(103, 23);
            this.unitPriceTextBox.TabIndex = 11;
            this.unitPriceTextBox.Text = "0.00";
            // 
            // unitPriceLabel
            // 
            this.unitPriceLabel.AutoSize = true;
            this.unitPriceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.unitPriceLabel.Location = new System.Drawing.Point(369, 61);
            this.unitPriceLabel.Name = "unitPriceLabel";
            this.unitPriceLabel.Size = new System.Drawing.Size(74, 15);
            this.unitPriceLabel.TabIndex = 10;
            this.unitPriceLabel.Text = "UNIT PRICE:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descriptionTextBox.Location = new System.Drawing.Point(13, 78);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(343, 44);
            this.descriptionTextBox.TabIndex = 9;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.descriptionLabel.Location = new System.Drawing.Point(13, 61);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(87, 15);
            this.descriptionLabel.TabIndex = 8;
            this.descriptionLabel.Text = "DESCRIPTION:";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(369, 30);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(103, 23);
            this.categoryComboBox.TabIndex = 7;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.categoryLabel.Location = new System.Drawing.Point(369, 13);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(70, 15);
            this.categoryLabel.TabIndex = 6;
            this.categoryLabel.Text = "CATEGORY:";
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productNameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.productNameTextBox.Location = new System.Drawing.Point(227, 30);
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.Size = new System.Drawing.Size(129, 23);
            this.productNameTextBox.TabIndex = 5;
            // 
            // productNameLabel
            // 
            this.productNameLabel.AutoSize = true;
            this.productNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.productNameLabel.Location = new System.Drawing.Point(227, 13);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Size = new System.Drawing.Size(103, 15);
            this.productNameLabel.TabIndex = 4;
            this.productNameLabel.Text = "PRODUCT NAME:";
            // 
            // itemCodeTextBox
            // 
            this.itemCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemCodeTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.itemCodeTextBox.Location = new System.Drawing.Point(111, 30);
            this.itemCodeTextBox.Name = "itemCodeTextBox";
            this.itemCodeTextBox.Size = new System.Drawing.Size(103, 23);
            this.itemCodeTextBox.TabIndex = 3;
            // 
            // itemCodeLabel
            // 
            this.itemCodeLabel.AutoSize = true;
            this.itemCodeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.itemCodeLabel.Location = new System.Drawing.Point(111, 13);
            this.itemCodeLabel.Name = "itemCodeLabel";
            this.itemCodeLabel.Size = new System.Drawing.Size(72, 15);
            this.itemCodeLabel.TabIndex = 2;
            this.itemCodeLabel.Text = "ITEM CODE:";
            // 
            // itemIDTextBox
            // 
            this.itemIDTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.itemIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemIDTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.itemIDTextBox.Location = new System.Drawing.Point(13, 30);
            this.itemIDTextBox.Name = "itemIDTextBox";
            this.itemIDTextBox.ReadOnly = true;
            this.itemIDTextBox.Size = new System.Drawing.Size(86, 23);
            this.itemIDTextBox.TabIndex = 1;
            // 
            // itemIDLabel
            // 
            this.itemIDLabel.AutoSize = true;
            this.itemIDLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.itemIDLabel.Location = new System.Drawing.Point(13, 13);
            this.itemIDLabel.Name = "itemIDLabel";
            this.itemIDLabel.Size = new System.Drawing.Size(54, 15);
            this.itemIDLabel.TabIndex = 0;
            this.itemIDLabel.Text = "ITEM ID:";
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(857, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            this.menuStrip.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 541);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Inventory Management System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.sidebarPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemsDataGridView)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
