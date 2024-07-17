using Microsoft.Extensions.Configuration;
using ShabatHost.Views;
using ShabatHost.DAL;
using ShabatHost.DAL.Repositories;

namespace ShabatHost
{
    internal class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create a new instance of a JSON configuration provider
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            // Get the connection string and default DB name from the configuration
            string? connectionString = config["ConnectionString"];
            string? dbName = config["DefaultDB"];
            // Check if the connection string or default DB name is null or empty
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(dbName))
            {
                throw new InvalidOperationException("Connection string or default DB name is not set");
            };

            // Create a new instance of the DBContex class
            DBContex dBContex = new DBContex(connectionString);
            // Check if the database is correctly set up
            dBContex.CheckDefaultDB(dbName);
            // Create a new instance of the Seed class
            Seed seed = new Seed(dBContex);
            // Check if the database tables are set up correctly
            seed.CheckTables();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new HostForm(new CategoryRepository(dBContex)));
        }
    }
}