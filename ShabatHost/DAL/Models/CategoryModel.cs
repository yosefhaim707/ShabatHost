using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabatHost.DAL.Models
{
    // A class to represent the category object model
    internal class CategoryModel
    {
       public int? Id { get; set; }
       public string Name { get; set; }
        
        // Constructor as regular intance
        public CategoryModel(int? id, string name)
        {
            Id = id;
            Name = name;
        }

        // Constructor as a DataRow instance
        public CategoryModel(DataRow row)
        {
            if (row == null || row["name"] == null)
            {
                throw new ArgumentNullException(nameof(row));
            }
            Id = (int)row["id"];
            Name = (string)row["name"];
        }
    }
}
