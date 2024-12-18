using Azure.Core;
using Microsoft.Data.SqlClient;

public static class DataInitializer
{
    public static void InitDatabase(SqlConnection connection)
    {
        string query=@"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StoreDB')
                    BEGIN
                        CREATE DATABASE StoreDB;
                    END";

        using (SqlCommand cmd = new SqlCommand(query, connection))    
            {
                try
                {
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Database initialized successfully: {rowsAffected} ligne(s) affectée(s)"); 
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                } 
            }
    }

    public static void InitTables(SqlConnection connection,SqlTransaction transaction)
    {
        string query=@"
            USE StoreDB;

            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
            BEGIN
                CREATE TABLE Categories (
                    pk_cat INT PRIMARY KEY identity(1,1),
                    name_cat VARCHAR(50) NOT NULL
                );
            END;

            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
            BEGIN
                CREATE TABLE Products (
                    pk_pro INT PRIMARY KEY  identity(1,1),
                    name_pro VARCHAR(50) NOT NULL,
                    quantity_pro INT NOT NULL,
                    price_pro FLOAT NOT NULL,
                    fk_cat INT NOT NULL,
                    FOREIGN KEY (fk_cat) REFERENCES Categories(pk_cat)
                );
            END;";


        using (SqlCommand cmd = new SqlCommand(query, connection, transaction))    
            {
                try
                {
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Tables initialized successfully: {rowsAffected} ligne(s) affectée(s)"); 
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                } 
            }
    }

    public static void DeleteDatabase(SqlConnection connection){
         string query=@"
                    BEGIN
                        DROP DATABASE StoreDB;
                    END";

        using (SqlCommand cmd = new SqlCommand(query, connection))    
            {
                try
                {
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Database deleted: {rowsAffected} ligne(s) affectée(s)"); 
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                } 
            }
    }
}
