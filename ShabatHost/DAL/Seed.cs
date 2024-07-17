using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ShabatHost.DAL
{
    // A class to check if the database tables are set up correctly
    internal class Seed
    {
        // Initialize the Seed class
        private DBContex _dBContex;
        public Seed(DBContex dBContex)
        {
            _dBContex = dBContex;
        }

        // Check if the database tables are set up correctly
        public void CheckTables()
        {
            string query = @"USE Shabat;
                            GO
                            DECLARE @tablecreated INT = 0;
                            BEGIN TRANSACTION;
                            BEGIN TRY
                                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories' AND type = 'U')
                                BEGIN
                                    CREATE TABLE Categories (
                                    CategoryID INT PRIMARY KEY,
                                    CategoryName VARCHAR(255)
                                    );
                                    SET @tablecreated = 1;
                                END
                            COMMIT TRANSACTION;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRANSACTION;
                            END CATCH
                            SELECT @tablecreated AS IsCreated;";
            // Execute the query
            _dBContex.ExecuteQuery(query, null!);
            // Check if the table creation was successful
            string testQuery = "SELECT COUNT(*) FROM Categories;";
            DataTable test = _dBContex.ExecuteQuery(testQuery, null!);
            if (test.Rows.Count <= 0)
            {
                throw new Exception("Table Categories was not created succesfully.");
            }
        }
    }
}
