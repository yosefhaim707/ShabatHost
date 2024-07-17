using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace ShabatHost.DAL
{
    internal class DBContex
    {
        private string _connectionString {  get; init; }
        // Initialize the connection string
        public DBContex(string connectionString)
        {
            // Check if the connection string is null or empty
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connectionString = connectionString;
        }
        
        // Checks that the connection works
        private void CheckConnection()
        {
            DataTable result = ExecuteQuery("SELECT 1+1 AS test", null!);
            if (Convert.ToInt32(result.Rows[0][0]) != 2)
            {
                throw new Exception("Unable To Connect To The Database");
            }
        }

        // Checks if the database is correctly set up
        public void CheckDefaultDB(string dbName)
        {
            CheckConnection();
            // A query to check if the database exists
            string query = $"SELECT db_id('{SqlEscape(dbName)}');";
            // Execute the query
            DataTable result = ExecuteQuery(query, null!);
            // Check if the database is not defined in different ways
            if (result == null || result.Rows.Count == 0 || result.Rows[0][0] == DBNull.Value)
                throw new Exception($"Database {dbName} is not defined.");
        }
        
        // Execute a regular query
        public DataTable ExecuteQuery(string sqlQuery, SqlParameter[] parameters)
        {
            // Create a new DataTable instance
            DataTable output = new DataTable();
            // Create a new SqlConnection instance
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Create a new SqlCommand instance
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Check if the parameters are not null
                    if (parameters != null)
                    {
                        // Add the parameters to the command
                        command.Parameters.AddRange(parameters);
                    }
                    // Try to open the connection
                    try
                    {
                        connection.Open();
                        // Create a new SqlDataAdapter instance
                        using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Fill the DataTable with the data from the query
                            adapter.Fill(output);
                        }
                    }
                    // Catch any SqlException
                    catch (SqlException ex)
                    {
                        // Log the error
                        Debug.WriteLine("An Error Happend: " + ex.Message);
                    }
                }
            }
            // Return the DataTable
            return output;
        }

        // Execute a non-query
        public int ExecuteNonQuery(string queryStr, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("An error occurred: " + ex.Message);
                        return -1;
                    }
                }
            }
        }

        // Execute a scalar query
        /// <summary>
        /// Executes a SQL command and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">SQL parameters to avoid SQL injection.</param>
        /// <returns>The value of the first column of the first row in the result set.</returns>
        public object? ExecuteScalar(string query, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to the command.
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"General Error: {ex.Message}");
                    }

                    return null;
                }
            }
        }

        // Avoids basic SQL injections
        private string SqlEscape(string query)
        {
            return query.Replace("'", "''");
        }
    }
}
