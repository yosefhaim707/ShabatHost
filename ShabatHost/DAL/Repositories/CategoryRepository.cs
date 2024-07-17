using ShabatHost.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabatHost.DAL.Repositories
{
    internal class CategoryRepository : IRepository<CategoryModel>
    {
        private readonly DBContex _dBContex;
        public CategoryRepository(DBContex dbContex)
        {
            _dBContex = dbContex;
        }

        // Gets all categories from the database
        public List<CategoryModel> GetAll()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            string queryString = @"SELECT ID, name
                            FROM Categories;";
            DataTable queryResult = _dBContex.ExecuteQuery(queryString, null!);
            foreach (DataRow row in queryResult.Rows)
            {
                categories.Add(new CategoryModel(row));
            }
            return categories;
        }

        // Gets a category by its ID
        public CategoryModel? GetById(int id)
        {
            string queryString = @"SELECT ID, name
                            FROM Categories
                            WHERE ID = @id;";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            DataTable queryResult = _dBContex.ExecuteQuery(queryString, parameters);
            if(queryResult.Rows.Count > 0)
            {
                return new CategoryModel(queryResult.Rows[0]);
            }
            return null;
        }

        // Inserts a new category to the database
        public bool Insert(CategoryModel category)
        {
            string queryString = @"INSERT INTO Categories (name)
                                   VALUES (@name);";
            SqlParameter[] parameters = { new SqlParameter("@name", category.Name)};
            int rowsAffected = _dBContex.ExecuteNonQuery(queryString, parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(CategoryModel category)
        {
            string queryString = @"UPDATE Categories
                                   SET name = @name
                                   WHERE ID = @id;";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", category.Id),
                new SqlParameter("@name", category.Name)
            };
            int rowsAffected = _dBContex.ExecuteNonQuery(queryString, parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            string queryString = @"DELETE FROM Categories
                                   WHERE ID = @id;";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            int rowsAffected = _dBContex.ExecuteNonQuery(queryString, parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
