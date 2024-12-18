using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString= "Server=localhost;Database=MyDatabase;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";

        List<string> queryList = new List<string>
        {   
        "CREATE TABLE Car (id INT PRIMARY KEY IDENTITY(1,1), brand VARCHAR(50), model VARCHAR(50), yearBuild INT, price DECIMAL(10, 2));",
        "INSERT INTO Car(brand, model, yearBuild, price) VALUES('Volvo', 'EX40', 2023, 49500),('Volvo', 'XC90', 2025, 57400),('Renault', 'Clio', 2019, 17840),('Renault', 'Espace', 1995, 20899),('Renault', 'Avantime', 2002, 45000)",
        "UPDATE Car SET price='19500', model= 'Megane' WHERE id = 5",
        "UPDATE Car SET price='17890' WHERE id = 3"
        };

        string selectQuery = "SELECT model FROM Car WHERE id > 2";

        using (SqlConnection connection = new SqlConnection(connectionString))
        { 
            foreach (string query in queryList)
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} ligne(s) affectée(s)"); 
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                } 
                finally
                {
                    connection.Close();
                }
            } 
            SqlCommand select = new SqlCommand(selectQuery, connection);
            try
            {
                connection.Open();
                using(SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string model= reader["model"].ToString();
                        Console.WriteLine($"model: {model}");
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
