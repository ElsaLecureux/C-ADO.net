using Azure.Core;
using Microsoft.Data.SqlClient;

public static class DataInitializer
{
    public static void InitDatabase(SqlConnection connection)
    {

        string query = @"
        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Library')
        BEGIN
            CREATE DATABASE Library;
            PRINT 'Database Library created successfully.';
        END
        GO

        USE Library;
        GO

        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Book')
        BEGIN
            CREATE TABLE Book (
                id INT PRIMARY KEY IDENTITY(1,1), 
                book_title VARCHAR(100) NOT NULL,  
                author VARCHAR(100) NOT NULL,      
                quantity INT,               
            );
            PRINT 'Table Book created successfully.';
        END
        GO";

        using (SqlCommand cmd = new SqlCommand(query, connection))    
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Database initiated successfully"); 
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                } 
            }
    }
}