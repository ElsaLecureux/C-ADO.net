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
                Console.WriteLine($"Database created successfully: {rowsAffected} ligne(s) affectée(s)"); 
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                } 
            }
    }

    public static void InitTables(SqlConnection connection,SqlTransaction transaction)
    {
        string createCategoriesTable=@"
            USE StoreDB;

            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
            BEGIN
                CREATE TABLE Categories (
                    pk_cat INT PRIMARY KEY identity(1,1),
                    name_cat VARCHAR(50) NOT NULL
                );
            END;";

        using (SqlCommand cmd = new SqlCommand(createCategoriesTable, connection, transaction))    
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Table Categories Created successfully"); 
            }

        string createProductTable=@"
            USE StoreDB;

            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
            BEGIN
                CREATE TABLE Products (
                    pk_pro INT PRIMARY KEY  identity(1,1),
                    name_pro VARCHAR(50) NOT NULL,
                    quantity_pro INT NOT NULL,
                    price_pro DECIMAL(10, 2) NOT NULL,
                    fk_cat INT NOT NULL,
                    FOREIGN KEY (fk_cat) REFERENCES Categories(pk_cat)
                );
            END;";

        using (SqlCommand cmd = new SqlCommand(createProductTable, connection, transaction))    
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Table Products created successfully"); 
            }

        string insertDataCategories= @"
            
            IF NOT EXISTS (SELECT 1 FROM Categories)
            BEGIN
            INSERT INTO Categories (name_cat)
            VALUES ('clothes'),
                   ('furniture'),
                   ('tech');
            END;";

        using (SqlCommand cmd = new SqlCommand(insertDataCategories, connection, transaction))    
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Table Categories initialized successfully"); 
            }

        string insertDataProducts= @"
            IF NOT EXISTS (SELECT 1 FROM Products)
            BEGIN
            INSERT INTO Products (name_pro, quantity_pro, price_pro, fk_cat)
            VALUES ('tshirt', 45, 15.99, 1),
                   ('pull', 36, 49.99, 1),
                   ('desk', 12, 129.90, 2),
                   ('chair', 23, 52.99, 2),
                   ('computer', 5, 999.99, 3),
                   ('keyboard', 15, 25, 3),
                   ('mouse', 22, 12.50, 3);
            END;";


        using (SqlCommand cmd = new SqlCommand(insertDataProducts, connection, transaction))    
            {
                try
                {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Table Products initialized successfully"); 
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
