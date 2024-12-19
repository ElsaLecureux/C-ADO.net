using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using static DataInitializer;

class Program
{
    public static bool running = true;
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        string databaseConnectionString = "Server=localhost;Database=StoreDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";

        using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
        {
            masterConnection.Open();
            string checkDatabaseQuery= "SELECT database_id from SYS.DATABASES WHERE Name = 'StoreDB'";
            using (SqlCommand cmd = new SqlCommand(checkDatabaseQuery, masterConnection))
            {
                try
                {
                    InitDatabase(masterConnection);
                }
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
                catch(Exception exp)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {exp}");
                }
            }
        }

        using (SqlConnection databaseConnection = new SqlConnection(databaseConnectionString))
        {
            databaseConnection.Open();

            while(running)
            {
            Options(databaseConnection);
            }
        }
    }
        

        public static void Options(SqlConnection connection)
        {
            Console.WriteLine("   Welcome to the console store!   ");
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("1. Add a new product.");
            Console.WriteLine("2. Update a product.");
            Console.WriteLine("3. Delete a product.");
            Console.WriteLine("4. Display all products by category.");
            Console.WriteLine("5. Display cumulated prices of all products.");
            Console.WriteLine("6. Exit console store.");
            Console.WriteLine("Please Select an option: ");
            string option = Console.ReadLine()!;
            switch(option)
            {
                case "1":
                    AddProduct(connection);
                    break;
                case "2":
                    UpdateProduct(connection);
                    break;
                case "3":
                    DeleteProduct(connection);
                    break;
                case "4":
                    DisplayProductsByCategories(connection);
                    break;
                case "5":
                    DisplayCumulatedPrices(connection);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        public static void AddProduct(SqlConnection connection)
        {
            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine()!;
            Console.WriteLine("Enter product quantity:");
            int quantity = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter product price:");
            decimal price = decimal.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter product category number:");
            string category_id = Console.ReadLine()!;
            string query= "INSERT INTO Products(name_pro, quantity_pro, price_pro, fk_cat) VALUES (@Name, @Quantity, @Price, @Category_id)";
            
            using (SqlCommand cmd = new SqlCommand(query, connection))    
            {
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Quantity", quantity));
                cmd.Parameters.Add(new SqlParameter("@Price", price));
                cmd.Parameters.Add(new SqlParameter("@Category_id", category_id));
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Product: {name} created."); 
                }
                catch(SqlException exp)
                {
                    Console.WriteLine($"Error adding the product in database SqlException: {exp}");
                }
                catch(Exception exp)
                {
                    Console.WriteLine($"Error adding the product in database Exception: {exp}");
                }
            }
        }

        public static void UpdateProduct(SqlConnection connection)
        {
            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine()!;
            Console.WriteLine("Enter product quantity:");
            int quantity = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter product price:");
            decimal price = decimal.Parse(Console.ReadLine()!, System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
            Console.WriteLine("Enter product category number:");
            string category_id = Console.ReadLine()!;
            string query= $"UPDATE Product SET name_pro = @Name, quantity_pro = @Quantity, price_pro = @Price, fk_cat= @Category WHERE name = @Name;";
            using (SqlCommand cmd = new SqlCommand(query, connection))    
            {
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Quantity", quantity));
                cmd.Parameters.Add(new SqlParameter("@Price", price));
                cmd.Parameters.Add(new SqlParameter("@Category_id", category_id));
                try
                {
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Product: {name} Updated."); 
                }
                catch(SqlException exp)
                {
                    Console.WriteLine($"Error updating the product in database: {exp}");
                }
            }
        }

        public static void DeleteProduct(SqlConnection connection)
        {
            Console.WriteLine("Type the name of the product you would like to delete: ");
            string name = Console.ReadLine()!;
            string query= "DELETE FROM Products WHERE name_pro = @Name";
            using (SqlCommand cmd = new SqlCommand(query, connection))    
            {
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Product {name} was successfully deleted");
                }
                catch(SqlException exp)
                {
                    Console.WriteLine($"Error deleting product in database: {exp}");
                }
            }
        }

        public static void DisplayProductsByCategories(SqlConnection connection)
        {
            Console.WriteLine("Choose the category you would like to display: ");
            string cat = Console.ReadLine()!;
            string query = "SELECT Products.name_pro, Products.quantity_pro, Products.price_pro FROM Products JOIN Categories ON Products.fk_cat = Categories.pk_cat WHERE Categories.name_cat = @Category";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Category", cat);
            Console.WriteLine($"List of all Products from category '{cat}':");
            try
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string product = reader["name_pro"].ToString()!;
                        string price = reader["price_pro"].ToString()!;
                        string quantity = reader["quantity_pro"].ToString()!;
                        Console.WriteLine($"Product: {product}, Price:  {price}, Quantity in stock: {quantity}.");
                    }
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine($"Error displaying products: {exp}");
            }
        }

        public static void DisplayCumulatedPrices(SqlConnection connection)
        {
            string query = "SELECT SUM(price_pro) FROM Products";
            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                decimal count= (decimal)cmd.ExecuteScalar();
                Console.WriteLine($"Here is the cumulated prices of all products in store: {count}");
            }
            catch(Exception exp)
            {
                Console.WriteLine($"Error calculating cumulated price: {exp}");
            }
        }

}


