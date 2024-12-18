using Microsoft.Data.SqlClient;
using static DataInitializer;

class Program
{
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";

        using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
        {
            masterConnection.Open();
            string checkDatabaseQuery= "SELECT database_id from SYS.DATABASES WHERE Name = 'StoreDB'";
            using (SqlCommand cmd = new SqlCommand(checkDatabaseQuery, masterConnection))
            {
                //DeleteDatabase(masterConnection);
                try
                {
                    InitDatabase(masterConnection);
                }
                // catch(SqlException exp)
                // {
                //     Console.WriteLine("Database already exists");
                // }
                catch(Exception exp)
                {
                    Console.WriteLine($"Error: {exp}");
                }
            }

           

            using (SqlTransaction transaction = masterConnection.BeginTransaction()) 
            {
                try
                {
                    InitTables(masterConnection, transaction);
                    transaction.Commit();
                }
                // catch(SqlException exp)
                // {
                //     Console.WriteLine("Tables already exist");
                //     transaction.Rollback();
                // }
                catch(Exception exp)
                {
                    Console.WriteLine($"Error: {exp}");
                }
            }
        }
    }
}
