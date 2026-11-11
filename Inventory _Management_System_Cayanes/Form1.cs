using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Inventory_Management_System_Cayanes.Models;
using Inventory_Management_System_Cayanes.Services;

namespace Inventory_Management_System_Cayanes
{
    public partial class Form1 : Form
    {
        private User _currentUser;
        private ItemService _itemService;
        private CategoryService _categoryService;
        private TransactionLogger _logger;
        private Item _selectedItem;
        private bool _gridConfigured = false;

        public Form1(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _itemService = new ItemService();
            _categoryService = new CategoryService();
            _logger = new TransactionLogger();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = $"Inventory Management System | User: {_currentUser.Username} ({_currentUser.Role})";
            LoadCategories();
            LoadItems();
            SetupUIPermissions();
            RefreshUI();
        }

        private void SetupUIPermissions()
        {
            bool isAdmin = _currentUser.IsAdmin();
            settingsButton.Enabled = isAdmin;
            categoryButton.Enabled = isAdmin;
            deleteButton.Enabled = isAdmin;
            addButton.Enabled = PermissionManager.CanAddItem(_currentUser);
            updateButton.Enabled = PermissionManager.CanEditItem(_currentUser);
            if (!isAdmin)
            {
                MessageBox.Show(
                    "You have limited permissions.\n" + PermissionManager.GetPermissionDescription(_currentUser),
                    "User Permissions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void LoadCategories()
        {
            try
            {
                List<Category> categories = _categoryService.GetAllCategories();
                categoryComboBox.DataSource = categories;
                categoryComboBox.DisplayMember = "CategoryName";
                categoryComboBox.ValueMember = "CategoryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadItems()
        {
            try
            {
                List<Item> items = _itemService.GetAllItems();
                itemsDataGridView.DataSource = items;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            // Only set up columns once so we do not stack event handlers
            if (_gridConfigured)
            {
                return;
            }

            itemsDataGridView.AutoGenerateColumns = false;
            itemsDataGridView.Columns.Clear();

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemID",
                HeaderText = "ITEM ID",
                Width = 80,
                Visible = false
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemCode",
                HeaderText = "ITEM CODE",
                Width = 100
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "PRODUCT NAME",
                Width = 150
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CategoryName",
                HeaderText = "CATEGORY",
                Width = 120
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "DESCRIPTION",
                Width = 150
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockQuantity",
                HeaderText = "STOCK QTY",
                Width = 100
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "UNIT PRICE",
                Width = 100
            });
            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReorderLevel",
                HeaderText = "REORDER LEVEL",
                Width = 100
            });

            itemsDataGridView.AllowUserToAddRows = false;
            itemsDataGridView.AllowUserToDeleteRows = false;
            itemsDataGridView.ReadOnly = true;
            itemsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            itemsDataGridView.MultiSelect = false;

            _gridConfigured = true;
        }

        private void RefreshUI()
        {
            LoadItems();
            ClearFields();
        }

        private void ClearFields()
        {
            itemIDTextBox.Text = "";
            itemCodeTextBox.Text = "";
            productNameTextBox.Text = "";

            // Safe: only select first category if list is not empty
            if (categoryComboBox.Items.Count > 0)
            {
                categoryComboBox.SelectedIndex = 0;
            }
            else
            {
                categoryComboBox.SelectedIndex = -1;
            }

            descriptionTextBox.Text = "";
            unitPriceTextBox.Text = "0.00";
            stockQuantityTextBox.Text = "0";
            reorderLevelTextBox.Text = "10";
            searchIDTextBox.Text = "";
            _selectedItem = null;
            itemsDataGridView.ClearSelection();

            foreach (DataGridViewRow row in itemsDataGridView.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    Item item = new Item
                    {
                        ItemCode = itemCodeTextBox.Text.Trim(),
                        ProductName = productNameTextBox.Text.Trim(),
                        CategoryID = Convert.ToInt32(categoryComboBox.SelectedValue),
                        Description = descriptionTextBox.Text.Trim(),
                        UnitPrice = decimal.Parse(unitPriceTextBox.Text),
                        StockQuantity = int.Parse(stockQuantityTextBox.Text),
                        ReorderLevel = int.Parse(reorderLevelTextBox.Text)
                    };

                    if (_itemService.ItemCodeExists(item.ItemCode))
                    {
                        MessageBox.Show("Item code already exists!", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int itemID = _itemService.AddItem(item, _currentUser.UserID);
                    _logger.LogItemAdded(_currentUser.UserID, itemID, item.ItemCode, item.ProductName);
                    MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.LogFailedOperation(_currentUser.UserID, "ADD", "Items", ex.Message);
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Please select an item from the list to update.\n\n" +
                    "Step 1: Click on any item row in the table below\n" +
                    "Step 2: The item details will appear in the form\n" +
                    "Step 3: Modify the fields as needed\n" +
                    "Step 4: Click UPDATE to save your changes",
                    "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateInputs())
            {
                string newItemCode = itemCodeTextBox.Text.Trim();
                if (_itemService.ItemCodeExists(newItemCode, _selectedItem.ItemID))
                {
                    MessageBox.Show("Item code already exists!", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    itemCodeTextBox.Focus();
                    return;
                }

                try
                {
                    string oldValues = $"ItemCode: {_selectedItem.ItemCode}, ProductName: {_selectedItem.ProductName}, StockQuantity: {_selectedItem.StockQuantity}";
                    _selectedItem.ItemCode = newItemCode;
                    _selectedItem.ProductName = productNameTextBox.Text.Trim();
                    _selectedItem.CategoryID = Convert.ToInt32(categoryComboBox.SelectedValue);
                    _selectedItem.Description = descriptionTextBox.Text.Trim();
                    _selectedItem.UnitPrice = decimal.Parse(unitPriceTextBox.Text);
                    _selectedItem.StockQuantity = int.Parse(stockQuantityTextBox.Text);
                    _selectedItem.ReorderLevel = int.Parse(reorderLevelTextBox.Text);
                    string newValues = $"ProductName: {_selectedItem.ProductName}, StockQuantity: {_selectedItem.StockQuantity}";

                    bool success = _itemService.UpdateItem(_selectedItem, _currentUser.UserID);
                    if (success)
                    {
                        _logger.LogItemUpdated(_currentUser.UserID, _selectedItem.ItemID, oldValues, newValues);
                        MessageBox.Show($"Item '{_selectedItem.ProductName}' updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshUI();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.LogFailedOperation(_currentUser.UserID, "UPDATE", "Items", ex.Message);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanDeleteItem(_currentUser))
            {
                MessageBox.Show("You do not have permission to delete items. Please contact an administrator.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedItem == null)
            {
                MessageBox.Show("Please select an item to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete '{_selectedItem.ProductName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _itemService.DeleteItem(_selectedItem.ItemID);
                    if (success)
                    {
                        _logger.LogItemDeleted(_currentUser.UserID, _selectedItem.ItemID, _selectedItem.ItemCode, _selectedItem.ProductName);
                        MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshUI();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.LogFailedOperation(_currentUser.UserID, "DELETE", "Items", ex.Message);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchIDTextBox.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadItems();
                return;
            }

            try
            {
                List<Item> items = _itemService.SearchItems(searchTerm);
                itemsDataGridView.DataSource = items;
                _logger.LogItemSearch(_currentUser.UserID, searchTerm);

                if (items.Count == 0)
                {
                    MessageBox.Show("No items found matching your search.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Quiet selection - no MessageBox popup
        private void itemsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (itemsDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = itemsDataGridView.SelectedRows[0];
                Item item = row.DataBoundItem as Item;
                if (item != null)
                {
                    // Clear previous highlight
                    foreach (DataGridViewRow r in itemsDataGridView.Rows)
                    {
                        r.DefaultCellStyle.BackColor = Color.White;
                    }

                    _selectedItem = item;
                    PopulateFormFields(item);
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void PopulateFormFields(Item item)
        {
            itemIDTextBox.Text = item.ItemID.ToString();
            itemCodeTextBox.Text = item.ItemCode;
            productNameTextBox.Text = item.ProductName;
            categoryComboBox.SelectedValue = item.CategoryID;
            descriptionTextBox.Text = item.Description;
            unitPriceTextBox.Text = item.UnitPrice.ToString("F2");
            stockQuantityTextBox.Text = item.StockQuantity.ToString();
            reorderLevelTextBox.Text = item.ReorderLevel.ToString();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(itemCodeTextBox.Text))
            {
                MessageBox.Show("Item code is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itemCodeTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(productNameTextBox.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productNameTextBox.Focus();
                return false;
            }

            if (categoryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                categoryComboBox.Focus();
                return false;
            }

            if (!decimal.TryParse(unitPriceTextBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Unit price must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                unitPriceTextBox.Focus();
                return false;
            }

            if (!int.TryParse(stockQuantityTextBox.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Stock quantity must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stockQuantityTextBox.Focus();
                return false;
            }

            if (!int.TryParse(reorderLevelTextBox.Text, out int reorder) || reorder < 0)
            {
                MessageBox.Show("Reorder level must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reorderLevelTextBox.Focus();
                return false;
            }

            return true;
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanManageCategories(_currentUser))
            {
                MessageBox.Show("You do not have permission to manage categories. Please contact an administrator.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Category management coming soon.", "Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanViewReports(_currentUser))
            {
                MessageBox.Show("You do not have permission to view reports.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal totalValue = _itemService.GetTotalInventoryValue();
                List<Item> lowStock = _itemService.GetLowStockItems();
                MessageBox.Show($"Total Inventory Value: ${totalValue:F2}\nLow Stock Items: {lowStock.Count}",
                    "Inventory Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_currentUser.IsAdmin())
            {
                MessageBox.Show("You do not have permission to access settings. Please contact an administrator.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SettingsForm settingsForm = new SettingsForm(_currentUser))
                {
                    DialogResult result = settingsForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.LogUserLogout(_currentUser.UserID);
            this.Close();
        }

        // Required by Designer - do not remove
        private void searchPanel_Paint(object sender, PaintEventArgs e)
        {
            // Empty - required by Designer
        }

        private void searchPanel_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
