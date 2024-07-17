using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReaLTaiizor.Forms;
using ShabatHost.DAL.Repositories;

using ShabatHost.DAL.Models;

namespace ShabatHost.Views
{
    internal partial class HostForm : MaterialForm
    {
        // Declare a CategoryRepository variable for the interaction with the database
        private CategoryRepository _categoryRepository;

        // Declare a Categories variable in the DataTable format 
        // In order to store the categories from the database
        private DataTable _categories;

        // Initialize the HostForm class
        public HostForm(CategoryRepository? categoryRepository)
        {
            InitializeComponent();
            _categoryRepository = categoryRepository;
            LoadCategories(_categoryRepository.GetAll());

        }

        // Load the categories from the database into the listView property of the form
        // Accepts a DataTable as a parameter
        private void LoadCategories(DataTable categories)
        {
            // Clear the listView property
            listView_Categories.Items.Clear();
            // Check if there are any categories
            if (categories.Rows.Count > 0)
            {
                // Loop through the categories DataTable and add each category to the listView
                foreach (DataRow row in categories.Rows)
                {
                    ListViewItem item = new ListViewItem(row["name"].ToString());
                    item.Tag = row["id"];
                    listView_Categories.Items.Add(item);
                }
            }
            else
            {
                // If there are no categories, show an error message
                MessageBox.Show("No categories found.");
            }
        }
        // Create a new overload of the LoadCategories method
        // Accepts a List<CategoryModel> as a parameter
        private void LoadCategories(List<CategoryModel> categories)
        {
            // Clear the listView property
            listView_Categories.Items.Clear();
            // Check if there are any categories
            if (categories.Count > 0)
            {
                // Loop through the categories List and add each category to the listView
                foreach (CategoryModel category in categories)
                {
                    ListViewItem item = new ListViewItem(category.Name);
                    item.Tag = category.Id;
                    listView_Categories.Items.Add(item);
                }
            }
            else
            {
                // If there are no categories, show an error message
                MessageBox.Show("No categories found.");
            }
        }

        private void button_Enter_Click(object sender, EventArgs e)
        {
            // Trim the adedd category name from whitespaces
            string categoryName = textBox_Insert.Text.Trim();
            // Check if the category name is not empty
            if (!string.IsNullOrEmpty(categoryName))
            {
                // Create a new CategoryModel instance with the category name
                CategoryModel category = new CategoryModel(null, categoryName);
                // Check if the category was successfully inserted
                bool insertSuccess = _categoryRepository.Insert(category);
                if (insertSuccess)
                {
                    // Reload the categories from the database
                    LoadCategories(_categoryRepository.GetAll());
                    // Clear the textBox_Insert
                    textBox_Insert.Clear();
                }
                else
                {
                    // Show an error message if the category was not inserted
                    MessageBox.Show("Failed to insert category.");
                }
            }
            else
            {
                // Show an error message if the category name is empty
                MessageBox.Show("Category name cannot be empty.");
            }
        }
    }
}
