using System;
namespace Inventory_Management_System_Cayanes.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public Category()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        public Category(int categoryID, string categoryName, string description = "")
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
            Description = description;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
