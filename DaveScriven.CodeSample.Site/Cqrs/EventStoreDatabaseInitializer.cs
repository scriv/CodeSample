using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DaveScriven.CodeSample.Site.Cqrs
{
    /// <summary>
    /// Initializes the CQRS event store.
    /// </summary>
    public static class EventStoreDatabaseInitializer
    {
        /// <summary>
        /// Creates the event store database if it does not already exist.
        /// </summary>
        public static void Initialize()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["EventStoreInitializer"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            string databaseName = "DaveScriven.CodeSample.EventStore";

            try
            {
                connection.Open();
                bool databaseExists = DatabaseExists(databaseName, connection);

                if (!databaseExists)
                {
                    CreateDatabase(databaseName, connection);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("The CQRS event store database could not be created!", exc);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Determines whether the specified database exists.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <param name="connection">The connection.</param>
        /// <returns><c>true</c> if a database with the specified name exists; otherwise <c>false</c>.</returns>
        private static bool DatabaseExists(string databaseName, SqlConnection connection)
        {
            using (var databaseExistsCommand = new SqlCommand("select * from master.dbo.sysdatabases where name = @DatabaseName", connection))
            {
                var databaseNameParameter = databaseExistsCommand.CreateParameter();
                databaseNameParameter.ParameterName = "DatabaseName";
                databaseNameParameter.SqlDbType = SqlDbType.VarChar;
                databaseNameParameter.Value = databaseName;

                databaseExistsCommand.Parameters.Add(databaseNameParameter);

                return databaseExistsCommand.ExecuteScalar() != null;
            }
        }

        /// <summary>
        /// Creates a database with the specified name.
        /// </summary>
        /// <param name="databaseName">The name of the database to create.</param>
        /// <param name="connection">The connection.</param>
        private static void CreateDatabase(string databaseName, SqlConnection connection)
        {
            using (var createDatabaseCommand = new SqlCommand(string.Format("CREATE DATABASE [{0}]", databaseName), connection))
            {
                createDatabaseCommand.ExecuteNonQuery();
            }
        }
    }
}